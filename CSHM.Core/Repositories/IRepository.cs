using System.Linq.Expressions;
using CSHM.Data.CRUD;
using CSHM.Domain.Interfaces;
using CSHM.Presentation.Base;

namespace CSHM.Core.Repositories;

public interface IRepository<T, V> : ICRUD<T> where V : class where T : class, IEntity
{
    MessageViewModel Add(T entity, int creatorID);
    MessageViewModel Edit(T entity, int modifierID);
    MessageViewModel Remove(int id, int modifierID, bool hardDelete = false);
    V SelectByID(int id);

    //ResultViewModel<V> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20);
    int Count(bool? activate, Expression<Func<T, bool>> where = null);
    public List<V> MapToViewModel(IQueryable<T> items);
    public List<V> MapToViewModel(IEnumerable<T> items);
    public V MapToViewModel(T item);
}