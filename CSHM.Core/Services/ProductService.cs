using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using CSHM.Presentation.Base;
using CSHM.Presentation.Product;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using CSHM.Presentation.Resources;
using System.Linq.Expressions;
using System.Reflection;


namespace CSHM.Core.Services
{
    public class ProductService : Repository<Product, ProductViewModel>, IProductService
    {
        private readonly ILogWidget _log;
        private readonly IMapper _mapper;
        private readonly IExcelWidget _excel;
        private readonly DatabaseContext _context;


        public ProductService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel, IHostingEnvironment hostingEnvironment) : base(context, log, mapper)
        {
            _log = log;
            _mapper = mapper;
            _excel = excel;
            _context = context;
        }

        public ResultViewModel<ProductViewModel> SelectAllByPublisher(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20) 
        {
            ResultViewModel<ProductViewModel> result = new ResultViewModel<ProductViewModel>();
            try
            {
                IQueryable<Product> items;
                Expression<Func<Product, bool>> condition = x => x.PublisherID == publisherID;
                items = GetAll(activate, condition, pageNumber, pageSize);
                
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

        public override ResultViewModel<ProductViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
        {
            throw new NotImplementedException();
        }

        private List<ErrorViewModel> ValidationForm(Product entity)
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

            if (entity.PublisherID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه ناشر")
                });
            }

            if (entity.ProductTypeID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه نوع محصول")
                });
            }

            if (entity.PublishTypeID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه نوع انتشار")
                });
            }

            if (entity.LanguageID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه زبان")
                });
            }

            if (entity.SizeTypeID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه نوع سایز")
                });
            }

            if (entity.CoverTypeID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه نوع کاور")
                });
            }


            //Max Length
            if (!string.IsNullOrEmpty(entity.Title) && entity.Title.Length > 300)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "عنوان", 300)
                });
            }

            if (!string.IsNullOrEmpty(entity.ISBN) && entity.ISBN.Length > 100)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "شناسنامه", 100)
                });
            }

            if (!string.IsNullOrEmpty(entity.PublishDate) && entity.PublishDate.Length > 10)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "تاریخ انتشار", 10)
                });
            }

            if (!string.IsNullOrEmpty(entity.MetaDescription) && entity.MetaDescription.Length > 4000)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "توضیحات", 4000)
                });
            }

            if (!string.IsNullOrEmpty(entity.Summary) && entity.Summary.Length > 2000)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "خلاصه", 2000)
                });
            }

            return result;
        }
    }
}
