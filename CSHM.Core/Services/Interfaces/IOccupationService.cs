using CSHM.Core.Repositories;
using CSHM.Domain;
using CSHM.Presentation.Occupation;

namespace CSHM.Core.Services.Interfaces;

public interface IOccupationService : IRepository<Occupation,OccupationViewModel>
{
}
