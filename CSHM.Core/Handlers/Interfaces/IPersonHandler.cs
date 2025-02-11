using CSHM.Presentation.Base;
using CSHM.Presentation.People;


namespace CSHM.Core.Handlers.Interfaces
{
    public interface IPersonHandler
    {
        public ResultViewModel<PersonViewModel> SelectAllByOccupation(int occupationID);
    }
}
