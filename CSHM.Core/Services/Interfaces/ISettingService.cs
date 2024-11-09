using CSHM.Presentations.General;
using CSHM.Core.Repositories;
using CSHM.Domain;

namespace CSHM.Core.Services.Interfaces;

public interface ISettingService : IRepository<Setting, SettingViewModel>
{
    List<SettingViewModel> SelectAllByGroup(string groupCode);
}