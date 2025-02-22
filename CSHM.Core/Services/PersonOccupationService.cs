using AutoMapper;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.People;
using CSHM.Presentation.Resources;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using Microsoft.AspNetCore.Hosting;
using System.Linq.Expressions;
using System.Reflection;


namespace CSHM.Core.Services
{
    public class PersonOccupationService : Repository<PersonOccupation, PersonOccupationViewModel>, IPersonOccupationService
    {
        private readonly ILogWidget _log;
        private readonly IMapper _mapper;
        private readonly IExcelWidget _excel;
        private readonly DatabaseContext _context;


        public PersonOccupationService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel, IHostingEnvironment hostingEnvironment) : base(context, log, mapper)
        {
            _log = log;
            _mapper = mapper;
            _excel = excel;
            _context = context;
        }

        public override ResultViewModel<PersonOccupationViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
        {
            var result = new ResultViewModel<PersonOccupationViewModel>();
            try
            {
                IQueryable<PersonOccupation> items;
                Expression<Func<PersonOccupation, bool>> condition = x => string.IsNullOrWhiteSpace(filter);
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    items = GetAll(activate, condition, pageNumber, pageSize);
                }
                else
                {
                    items = GetAll(activate, null, pageNumber, pageSize);
                }
                result.List = MapToViewModel(items);

                result.TotalCount = Count(activate, condition);

                result.Message = result.TotalCount > 0
                    ? new MessageViewModel { Status = Statuses.Success }
                    : new MessageViewModel { Status = Statuses.Warning, Message = Messages.NotFoundAnyRecords };
                return result;
            }
            catch (Exception ex)
            {
                _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName());
                result.Message = new MessageViewModel { Status = Statuses.Error, Message = _log.GetExceptionMessage(ex) };
                return result;
            }
        }

        private List<ErrorViewModel> ValidationForm(PersonOccupation entity)
        {
            var result = new List<ErrorViewModel>();

            //Required
            //if (string.IsNullOrEmpty(entity.Title) || string.IsNullOrWhiteSpace(entity.Title))
            //{
            //    result.Add(new ErrorViewModel()
            //    {
            //        ErrorCode = Errors.Error930,
            //        ErrorMessage = string.Format(Messages.FieldIsRequired, "عنوان")
            //    });
            //}

            if (entity.PersonID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه شخص")
                });
            }

            if (entity.OccupationID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه موضوع")
                });
            }


            //Max Length
            //if (!string.IsNullOrEmpty(entity.Title) && entity.Title.Length > 150)
            //{
            //    result.Add(new ErrorViewModel()
            //    {
            //        ErrorCode = Errors.Error931,
            //        ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "عنوان", 150)
            //    });
            //}

            return result;
        }
    }
}
