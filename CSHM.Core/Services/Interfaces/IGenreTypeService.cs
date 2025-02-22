using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Core.Services.Interfaces
{
    public interface IGenreTypeService : IRepository<GenreType,GenreTypeViewModel>
    {
    }
}
