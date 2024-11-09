using CSHM.Presentations.Calender;
using CSHM.Core.Repositories;
using CSHM.Domain;

namespace CSHM.Core.Services.Interfaces;

public interface ICalenderDimensionService : IRepository<CalenderDimension, CalenderDimensionViewModel>
{
    CalenderDimensionViewModel FirstDateOfMonth(string jalaliDate);

    CalenderDimensionViewModel FirstDateOfMonth(DateTime gregDate);

    CalenderDimensionViewModel LastDateOfMonth(string jalaliDate);

    CalenderDimensionViewModel LastDateOfMonth(DateTime gregDate);
}