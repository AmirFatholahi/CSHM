using System.Linq.Expressions;
using System.Reflection;
using CSHM.Data.Context;
using CSHM.Data.CRUD;
using CSHM.Domain.Interfaces;
using CSHM.Widget.Config;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CSHM.Presentation.Base;
using CSHM.Presentation.Resources;

namespace CSHM.Core.Repositories;

public abstract class Repository<T, V> : CRUD<T>, IRepository<T, V> where V : class where T : class, IEntity
{
    private readonly DatabaseContext _context;
    private readonly ILogWidget _log;
    private readonly IMapper _mapper;
    private readonly bool _entityLogEnabled;

    protected Repository(DatabaseContext context, ILogWidget log, IMapper mapper) : base(context)
    {
        _context = context;
        _log = log;
        _mapper = mapper;
        _entityLogEnabled = ConfigWidget.GetConfigValue<bool>("Setting:EntityLogEnabled");
    }

    public virtual MessageViewModel Add(T entity, int creatorID)
    {
        MessageViewModel result;
        List<ErrorViewModel> errors = new List<ErrorViewModel>();
        try
        {
            try
            {
                Create(entity, creatorID);
                Save();
                result = new MessageViewModel()
                {
                    ID = entity.ID,
                    Status = Statuses.Success,
                    Title = Titles.Save,
                    Message = string.Format(Messages.SaveSuccessedWithID, entity.ID),
                    Errors = null,
                    Value = ""
                };
                return result;
            }
            catch (Exception ex)
            {
                _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName(), creatorID);
                errors.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error605,
                    ErrorMessage = Messages.ExceptionInRepositoryError605
                });
                errors.Add(new ErrorViewModel()
                {
                    ErrorCode = ex.HResult.ToString(),
                    ErrorMessage = _log.GetExceptionMessage(ex)
                });
                result = new MessageViewModel()
                {
                    ID = -1,
                    Status = Statuses.Error,
                    Title = Titles.Error,
                    Message = Messages.SaveFailed,
                    Errors = errors,
                    Value = ""
                };
                return result;
            }
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName(), creatorID);

            result = new MessageViewModel()
            {
                ID = -1,
                Status = Statuses.Error,
                Title = Titles.Exception,
                Message = Messages.UnknownException,
                Value = _log.GetExceptionMessage(ex)
            };

            return result;
        }
    }

    public virtual MessageViewModel Edit(T entity, int modifierID)
    {
        MessageViewModel result;
        List<ErrorViewModel> errors = new List<ErrorViewModel>();
        try
        {
            var exist = Exist(entity.ID);
            if (exist)
            {
                try
                {

                    Update(entity, modifierID);
                    Save();
                    result = new MessageViewModel()
                    {
                        ID = entity.ID,
                        Status = Statuses.Success,
                        Title = Titles.Update,
                        Message = Messages.UpdateSuccessed,
                        Errors = null,
                        Value = ""
                    };
                    return result;
                }
                catch (Exception ex)
                {
                    _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName(), modifierID);
                    errors.Add(new ErrorViewModel()
                    {
                        ErrorCode = Errors.Error605,
                        ErrorMessage = Messages.ExceptionInRepositoryError605
                    });
                    errors.Add(new ErrorViewModel()
                    {
                        ErrorCode = ex.HResult.ToString(),
                        ErrorMessage = _log.GetExceptionMessage(ex)
                    });
                    result = new MessageViewModel()
                    {
                        ID = -1,
                        Status = Statuses.Error,
                        Title = Titles.Error,
                        Message = Messages.UpdateFailed,
                        Errors = errors,
                        Value = ""
                    };
                    return result;
                }
            }
            else
            {
                errors.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error610,
                    ErrorMessage = Messages.RecordNotFoundError610
                });
                result = new MessageViewModel()
                {
                    ID = -1,
                    Status = Statuses.Error,
                    Title = Titles.Error,
                    Message = Messages.UpdateFailed,
                    Errors = errors,
                    Value = ""
                };
                return result;
            }
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName(), modifierID);
            errors.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error605,
                ErrorMessage = Messages.ExceptionInRepositoryError605
            });
            errors.Add(new ErrorViewModel()
            {
                ErrorCode = ex.HResult.ToString(),
                ErrorMessage = _log.GetExceptionMessage(ex)
            });
            result = new MessageViewModel()
            {
                ID = -1,
                Status = Statuses.Error,
                Title = Titles.Exception,
                Message = Messages.UnknownException,
                Errors = errors,
                Value = ""
            };
            return result;
        }
    }

    public virtual MessageViewModel Remove(int id, int modifierID, bool hardDelete = false)
    {
        MessageViewModel result;
        List<ErrorViewModel> errors = new List<ErrorViewModel>();
        try
        {
            var exist = Exist(id);
            if (exist)
            {
                try
                {
                    Delete(id, modifierID, hardDelete);
                    Save();
                    result = new MessageViewModel()
                    {
                        ID = id,
                        Status = Statuses.Success,
                        Title = Titles.Remove,
                        Message = Messages.RemoveSuccessed,
                        Value = ""
                    };
                    return result;
                }
                catch (Exception ex)
                {
                    errors.Add(new ErrorViewModel()
                    {
                        ErrorCode = Errors.Error605,
                        ErrorMessage = Messages.ExceptionInRepositoryError605
                    });
                    errors.Add(new ErrorViewModel()
                    {
                        ErrorCode = ex.HResult.ToString(),
                        ErrorMessage = _log.GetExceptionMessage(ex)
                    });
                    result = new MessageViewModel()
                    {
                        ID = id,
                        Status = Statuses.Error,
                        Title = Titles.Error,
                        Message = Messages.RemoveFailed,
                        Errors = errors,
                        Value = ""
                    };
                    return result;
                }
            }
            else
            {
                errors.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error610,
                    ErrorMessage = Messages.RecordNotFoundError610
                });
                result = new MessageViewModel()
                {
                    ID = -1,
                    Status = Statuses.Error,
                    Title = Titles.Error,
                    Message = Messages.RemoveFailed,
                    Errors = errors,
                    Value = ""
                };
                return result;
            }
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName(), modifierID);

            errors.Add(new ErrorViewModel()
            {
                ErrorCode = Errors.Error605,
                ErrorMessage = Messages.ExceptionInRepositoryError605
            });
            errors.Add(new ErrorViewModel()
            {
                ErrorCode = ex.HResult.ToString(),
                ErrorMessage = _log.GetExceptionMessage(ex)
            });
            result = new MessageViewModel()
            {
                ID = -1,
                Status = Statuses.Error,
                Title = Titles.Exception,
                Message = Messages.UnknownException,
                Errors = errors,
                Value = ""
            };

            return result;
        }
    }

    public int Count(bool? activate, Expression<Func<T, bool>> where = null)
    {
        var query = GetAll(activate, @where,null,1000000,null,false,true);
        return query.Count();
    }

    public abstract ResultViewModel<V> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20);

    public virtual List<V> MapToViewModel(IQueryable<T> items)
    {
        return _mapper.Map<List<V>>(items);
    }
    public virtual List<V> MapToViewModel(IEnumerable<T> items)
    {
        return _mapper.Map<List<V>>(items);
    }
    public virtual V MapToViewModel(T item)
    {
        return _mapper.Map<V>(item);
    }
    public virtual V? SelectByID(int id)
    {
        V result;
        try
        {
            var item = GetByID(id);
            result = _mapper.Map<V>(item);
        }
        catch (Exception ex)
        {
            _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
            return null;
        }
        return result;
    }


    public override void Save()
    {
        if (_entityLogEnabled)
        {
            var modifiedEntities = _context.ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted)
                .ToList();

            foreach (var entity in modifiedEntities)
            {
                var entityName = entity.Metadata.DisplayName();
                var state = entity.State.ToString();
                var changedFields = "";

                var primaryKeyValue = entity.Properties
                    .Where(x => x.Metadata.IsPrimaryKey())
                    .Select(x => x.CurrentValue)
                    .FirstOrDefault();

                var creatorID = entity.Properties
                    .Where(x => x.Metadata.Name == "CreatorID")
                    .FirstOrDefault()?
                    .CurrentValue;

                if (entity.State == EntityState.Modified || entity.State == EntityState.Added)
                {
                    var properties = entity.Properties
                        .Where(x => ((entity.State == EntityState.Modified && x.IsModified) || entity.State == EntityState.Added) &&
                                    (x.OriginalValue != null || x.CurrentValue != null))
                        .Select(x => string.Format("{0}: {1}", x.Metadata.Name, x.OriginalValue == null ? "null" : x.OriginalValue));

                    if (entity.State == EntityState.Modified && properties.Any(x => x.Contains("IsDeleted")))
                        state = "Logically Deleted";

                    changedFields = "{" + string.Join(", ", properties) + "}";
                }

                _log.EntityLog(entityName, (int)primaryKeyValue, state, (int)creatorID, changedFields);
            }
        }

        base.Save();
    }

}