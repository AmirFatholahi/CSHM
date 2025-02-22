using AutoMapper;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.Product;
using CSHM.Presentation.Resources;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using System.Linq.Expressions;
using System.Reflection;


namespace CSHM.Core.Services
{
    public class GenreTypeService : Repository<GenreType, GenreTypeViewModel>, IGenreTypeService
    {
        private readonly ILogWidget _log;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;


        public GenreTypeService(DatabaseContext context, ILogWidget log, IMapper mapper) : base(context, log, mapper)
        {
            _log = log;
            _mapper = mapper;
        }

        public override ResultViewModel<GenreTypeViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
        {
            var result = new ResultViewModel<GenreTypeViewModel>();
            try
            {
                IQueryable<GenreType> items;
                Expression<Func<GenreType, bool>> condition = x => string.IsNullOrWhiteSpace(filter);
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

        private List<ErrorViewModel> ValidationForm(GenreType entity)
        {
            var result = new List<ErrorViewModel>();

            //Required
            if (string.IsNullOrEmpty(entity.Title) || string.IsNullOrWhiteSpace(entity.Title))
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "عنوان")
                });
            }

            //Max Length
            if (!string.IsNullOrEmpty(entity.Title) && entity.Title.Length > 200)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "عنوان", 200)
                });
            }
            return result;
        }
    }
}
