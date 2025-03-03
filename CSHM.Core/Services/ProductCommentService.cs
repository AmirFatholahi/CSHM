using AutoMapper;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.Product;
using CSHM.Presentation.Resources;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using Microsoft.AspNetCore.Hosting;
using System.Linq.Expressions;
using System.Reflection;


namespace CSHM.Core.Services
{
    public class ProductCommentService : Repository<ProductComment, ProductCommentViewModel>, IProductCommentService
    {
        private readonly ILogWidget _log;
        private readonly IMapper _mapper;
        private readonly IExcelWidget _excel;
        private readonly DatabaseContext _context;


        public ProductCommentService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel, IHostingEnvironment hostingEnvironment) : base(context, log, mapper)
        {
            _log = log;
            _mapper = mapper;
            _excel = excel;
            _context = context;
        }

        public ResultViewModel<ProductCommentViewModel> SelectAllByProductID(bool? activate,int productID , int? pageNumber = null, int pageSize = 20)
        {
            var result = new ResultViewModel<ProductCommentViewModel>();
            try
            {
                IQueryable<ProductComment> items;
                Expression<Func<ProductComment, bool>> condition = x =>  x.ProductID == productID;

                    items = GetAll(activate, null, pageNumber, pageSize);
                
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

        public override ResultViewModel<ProductCommentViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
        {
            var result = new ResultViewModel<ProductCommentViewModel>();
            try
            {
                IQueryable<ProductComment> items;
                Expression<Func<ProductComment, bool>> condition = x => (string.IsNullOrWhiteSpace(filter));
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

        private List<ErrorViewModel> ValidationForm(ProductComment entity)
        {
            var result = new List<ErrorViewModel>();

            //Required
            if (string.IsNullOrEmpty(entity.Note) || string.IsNullOrWhiteSpace(entity.Note))
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "یادداشت")
                });
            }

            if (entity.ProductID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه محصول")
                });
            }

            if (entity.UserID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه شخص")
                });
            }

            //Max Length
            if (!string.IsNullOrEmpty(entity.Note) && entity.Note.Length > 4000)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "یادداشت", 4000)
                });
            }

            if (!string.IsNullOrEmpty(entity.NoteDate) && entity.NoteDate.Length > 10)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "تاریخ یادداشت", 10)
                });
            }

            if (!string.IsNullOrEmpty(entity.NoteTime) && entity.NoteTime.Length > 10)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "ساعت یادداشت", 10)
                });
            }


            return result;
        }
    }
}
