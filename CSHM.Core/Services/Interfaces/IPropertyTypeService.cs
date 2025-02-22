using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Product;


namespace CSHM.Core.Services.Interfaces
{
    public interface IPropertyTypeService : IRepository<PropertyType, PropertyTypeViewModel>
    {
    }
}
