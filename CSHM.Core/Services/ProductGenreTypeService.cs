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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Core.Services
{
    public class ProductGenreTypeService : Repository<ProductGenreType, ProductGenreTypeViewModel>, IProductGenreTypeService
    {
        private readonly ILogWidget _log;
        private readonly IMapper _mapper;
        private readonly IExcelWidget _excel;
        private readonly DatabaseContext _context;


        public ProductGenreTypeService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel, IHostingEnvironment hostingEnvironment) : base(context, log, mapper)
        {
            _log = log;
            _mapper = mapper;
            _excel = excel;
            _context = context;
        }

        public ResultViewModel<ProductGenreTypeViewModel> SelectAllByProductID(int productID, bool? activate = true, string? filter = null, int? pageNumber = null, int pageSize = 20)
        {
            var result = new ResultViewModel<ProductGenreTypeViewModel>();
            try
            {
                IQueryable<ProductGenreType> items;
                Expression<Func<ProductGenreType, bool>> condition = x => x.ProductID == productID;
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

        public override ResultViewModel<ProductGenreTypeViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
        {
            var result = new ResultViewModel<ProductGenreTypeViewModel>();
            try
            {
                IQueryable<ProductGenreType> items;
                Expression<Func<ProductGenreType, bool>> condition = x => (string.IsNullOrWhiteSpace(filter));
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

        private List<ErrorViewModel> ValidationForm(ProductGenreType entity)
        {
            var result = new List<ErrorViewModel>();

            //Required

            if (entity.ProductID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه محصول")
                });
            }

            if (entity.GenreTypeID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه ویژگی")
                });
            }
            return result;
        }
    }
}
