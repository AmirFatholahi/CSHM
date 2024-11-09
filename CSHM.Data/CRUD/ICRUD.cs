using System.Linq.Expressions;
using CSHM.Domain.Interfaces;

namespace CSHM.Data.CRUD;

public interface ICRUD<T> where T : class, IEntity
{
    void Create(T entity, int creatorID);

    void Create(List<T> list, int creatorID);

    void Update(T entity, int modifierID);

    void Update(List<T> list, int modifierID);

    IQueryable<T> GetAll(bool? activate, Expression<Func<T, bool>> where = null, int? pageNumber = null, int pageSize = 20, Expression<Func<T, object>> order = null, bool desc = false, bool? allowPageSize = false);

    T GetByID(int id);

    void Delete(int id, int modifierID, bool hardDelete = false);

    void Delete(List<int> list, int modifierID, bool hardDelete = false);

    bool Exist(int id);

    void Save();
}