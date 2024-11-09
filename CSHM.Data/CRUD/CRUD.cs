using System.Linq.Expressions;
using CSHM.Data.Context;
using CSHM.Domain.Interfaces;
using CSHM.Widget.Security;

namespace CSHM.Data.CRUD;

public abstract class CRUD<T> : ICRUD<T> where T : class, IEntity
{
    private readonly DatabaseContext _context;
    protected CRUD(DatabaseContext context)
    {
        _context = context;
    }

    public virtual void Create(T entity, int creatorID)
    {
        SecurityWidget.AntiXSS(entity);
        //TODO:log
        entity.IsDeleted = false;
        entity.CreatorID = creatorID;
        entity.CreationDateTime = DateTime.Now;
        _context.Set<T>().Add(entity);
    }

    public virtual void Create(List<T> list, int creatorID)
    {
        foreach (var item in list)
        {
            SecurityWidget.AntiXSS(item);
            //TODO:log
            item.IsDeleted = false;
            item.CreatorID = creatorID;
            item.CreationDateTime = DateTime.Now;
            _context.Set<T>().Add(item);
        }

    }

    public virtual void Delete(int id, int modifierID, bool hardDelete = false)
    {
        var item = GetByID(id);
        //TODO:log
        if (hardDelete == false)
        {
            item.IsDeleted = true;
            Update(item, modifierID);
        }
        else
        {
            _context.Remove(item);
        }
    }

    public virtual void Delete(List<int> list, int modifierID, bool hardDelete = false)
    {
        var items = GetAll(null, x => list.Contains(x.ID)).ToList();
        if (hardDelete == false)
        {
            foreach (var item in items)
            {
                item.IsDeleted = true;
            }
            Update(items, modifierID);
        }
        else
        {
            _context.RemoveRange(items);
        }
    }

    public virtual void Update(T entity, int modifierID)
    {
        SecurityWidget.AntiXSS(entity);
        var exist = GetByID(entity.ID);
        if (exist != null)
        {
            //TODO: log
            entity.CreatorID = exist.CreatorID;
            entity.CreationDateTime = exist.CreationDateTime;
            entity.ModifierID = modifierID;
            entity.ModificationDateTime = DateTime.Now;
            _context.Entry(exist).CurrentValues.SetValues(entity);
        }
    }

    public virtual void Update(List<T> list, int modifierID)
    {
        var items = GetAll(null, x => list.Select(l => l.ID).Contains(x.ID)).ToList();
        foreach (var item in list)
        {
            SecurityWidget.AntiXSS(item);
            var exist = items.FirstOrDefault(x => x.ID == item.ID);
            if (exist != null)
            {
                item.CreatorID = exist.CreatorID;
                item.CreationDateTime = exist.CreationDateTime;
                _context.Entry(exist).CurrentValues.SetValues(item);
            }
        }
    }

    public virtual bool Exist(int id)
    {
        var query = GetAll(null, x => x.ID == id);
        return query.Any();
    }

    public virtual IQueryable<T> GetAll(bool? activate, Expression<Func<T, bool>>? where = null,
       int? pageNumber = null, int pageSize = 20, Expression<Func<T, object>>? order = null, bool desc = false, bool? allowPageSize = false)
    {
        if (allowPageSize == false)
        {
            if (pageSize > 100)
            {
                pageSize = 100;
            }
        }

        IQueryable<T> query = _context.Set<T>();
        if (activate != null)
            query = query.Where(x => x.IsActive == activate);
        if (where != null)
            query = query.Where(where);
        if (order != null)
        {
            query = desc ? query.OrderByDescending(order) : query.OrderBy(order);
        }
        if (pageNumber != null)
        {
            query = query.Skip((int)(pageNumber - 1) * pageSize).Take(pageSize);
        }
        else
        {
            if (allowPageSize == false)
            {
                query = query.Take(100);
            }
            else
            {
                query = query.Take(1500);
            }

        }
        return query;
    }
  

    public virtual T GetByID(int id)
    {
        T entity;
        var result = GetAll(null, x => x.ID == id);
        entity = result.FirstOrDefault();
        return entity;
    }

    public virtual void Save()
    {
        _context.SaveChanges();
    }

    
}