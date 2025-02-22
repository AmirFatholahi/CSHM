using AutoMapper;
using CSHM.Core.Handlers.Interfaces;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.Product;
using CSHM.Presentation.Resources;
using CSHM.Widget.Captcha;
using CSHM.Widget.Dapper;
using CSHM.Widget.Email;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using CSHM.Widget.Redis;
using CSHM.Widget.Rest;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Core.Handlers
{
    public class ProductHandler : IProductHandler
    {

        private readonly ILogWidget _log;
        private readonly IProductService _productService;
        private readonly IProductOccupationService _productOccupationService;
            
        public ProductHandler(ILogWidget log, IDapperWidget dapper, IExcelWidget excel, IMapper mapper,IProductService productService,IProductOccupationService productOccupationService)
        {
            _log = log;
            _productService = productService;
            _productOccupationService = productOccupationService;
        }


        public ResultViewModel<ProductViewModel> SelectAllByPublisher(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20)
        {
            ResultViewModel<ProductViewModel> result = new ResultViewModel<ProductViewModel>();
            try
            {
                IQueryable<Product> items;
                Expression<Func<Product, bool>> condition = x => x.PublisherID == publisherID;
                items =_productService.GetAll(activate, condition, pageNumber, pageSize);

                result.List =_productService. MapToViewModel(items);


                foreach (var item in result.List)
                {
                    var list = _productOccupationService.GetAll(true, x => x.ProductID == item.ID).ToList();
                    item.ProductOccupations = _productOccupationService.MapToViewModel(list);
                }

                result.TotalCount =_productService.Count(activate, condition);

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

    }
}
