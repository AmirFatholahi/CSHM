using CSHM.Presentation.Base;
using CSHM.Presentation.People;


namespace CSHM.Core.Handlers.Interfaces
{
    public interface IPersonHandler
    {
        public ResultViewModel<PersonViewModel> SelectAllByOccupation(int occupationID);

        public ResultViewModel<PersonViewModel> SelectAll(bool? activate, int? pageNumber = null, int pageSize = 20);

        public ResultViewModel<PersonViewModel> SelectAllPin(bool? activate, int? pageNumber = null, int pageSize = 20);
    }
}
