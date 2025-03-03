using AutoMapper;
using CSHM.Core.Handlers.Interfaces;
using CSHM.Core.Services.Interfaces;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.Lable;
using CSHM.Presentation.Product;
using CSHM.Presentation.Resources;
using CSHM.Widget.Dapper;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using System.Linq.Expressions;
using System.Reflection;


namespace CSHM.Core.Handlers
{
    public class ProductHandler : IProductHandler
    {

        private readonly ILogWidget _log;
        private readonly IProductService _productService;
        private readonly IProductOccupationService _productOccupationService;
        private readonly IProductLableService _productLableService;
        private readonly ILableService _lableSrvice;

        private readonly IProductCategoryTypeService _productCategoryTypeService;
        private readonly ICategoryTypeService _categoryTypeSrvice;

        public ProductHandler(ILogWidget log, IDapperWidget dapper, IExcelWidget excel, IMapper mapper,IProductService productService,
            IProductOccupationService productOccupationService,IProductLableService productLableService,ILableService lableService,
            IProductCategoryTypeService productCategoryTypeService, ICategoryTypeService categoryTypeService)
        {
            _log = log;
            _productService = productService;
            _productOccupationService = productOccupationService;
            _productLableService = productLableService;
            _lableSrvice = lableService;
            _productCategoryTypeService = productCategoryTypeService;
            _categoryTypeSrvice = categoryTypeService;
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

        public ResultViewModel<ProductViewModel> SelectAllNewByPublisher(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20)
        {
            ResultViewModel<ProductViewModel> result = new ResultViewModel<ProductViewModel>();
            try
            {
                IQueryable<Product> items;
                Expression<Func<Product, bool>> condition = x => x.PublisherID == publisherID && x.IsNew == true;
                items = _productService.GetAll(activate, condition, pageNumber, pageSize);

                result.List = _productService.MapToViewModel(items);


                foreach (var item in result.List)
                {
                    var list = _productOccupationService.GetAll(true, x => x.ProductID == item.ID).ToList();
                    item.ProductOccupations = _productOccupationService.MapToViewModel(list);
                }

                result.TotalCount = _productService.Count(activate, condition);

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

        public ResultViewModel<ProductViewModel> SelectAllRecommendedByPublisher(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20)
        {
            ResultViewModel<ProductViewModel> result = new ResultViewModel<ProductViewModel>();
            try
            {
                IQueryable<Product> items;
                Expression<Func<Product, bool>> condition = x => x.PublisherID == publisherID && x.IsRecommended == true;
                items = _productService.GetAll(activate, condition, pageNumber, pageSize);

                result.List = _productService.MapToViewModel(items);


                foreach (var item in result.List)
                {
                    var list = _productOccupationService.GetAll(true, x => x.ProductID == item.ID).ToList();
                    item.ProductOccupations = _productOccupationService.MapToViewModel(list);
                }

                result.TotalCount = _productService.Count(activate, condition);

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

        public ResultViewModel<ProductViewModel> SelectAllSelectedByPublisher(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20)
        {
            ResultViewModel<ProductViewModel> result = new ResultViewModel<ProductViewModel>();
            try
            {
                IQueryable<Product> items;
                Expression<Func<Product, bool>> condition = x => x.PublisherID == publisherID && x.IsSelected == true;
                items = _productService.GetAll(activate, condition, pageNumber, pageSize);

                result.List = _productService.MapToViewModel(items);


                foreach (var item in result.List)
                {
                    var list = _productOccupationService.GetAll(true, x => x.ProductID == item.ID).ToList();
                    item.ProductOccupations = _productOccupationService.MapToViewModel(list);
                }

                result.TotalCount = _productService.Count(activate, condition);

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

        public ResultViewModel<ProductViewModel> SelectAllSoonByPublisher(bool? activate, int publisherID, int? pageNumber = null, int pageSize = 20)
        {
            ResultViewModel<ProductViewModel> result = new ResultViewModel<ProductViewModel>();
            try
            {
                IQueryable<Product> items;
                Expression<Func<Product, bool>> condition = x => x.PublisherID == publisherID && x.IsSoon == true;
                items = _productService.GetAll(activate, condition, pageNumber, pageSize);

                result.List = _productService.MapToViewModel(items);


                foreach (var item in result.List)
                {
                    var list = _productOccupationService.GetAll(true, x => x.ProductID == item.ID).ToList();
                    item.ProductOccupations = _productOccupationService.MapToViewModel(list);
                }

                result.TotalCount = _productService.Count(activate, condition);

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

        public ResultViewModel<LableViewModel> SelectAllLableByProductID(bool? activate, int productID, int? pageNumber = null, int pageSize = 20)
        {
            ResultViewModel<LableViewModel> result = new ResultViewModel<LableViewModel>();
            result.List = new List<LableViewModel>();
            var errors = new List<ErrorViewModel>();

            var productLables = _productLableService.GetAll(true, x => x.ProductID == productID).ToList();

            foreach (var item in productLables)
            {
                var lable = item.Lable;

                result.List.Add(_lableSrvice.MapToViewModel(lable));
            }

            return result;
        }


        public ResultViewModel<ProductViewModel> SelectAllByCategoryType(bool? activate, int categoryTypeID, int? pageNumber = null, int pageSize = 20) 
        {
            ResultViewModel<ProductViewModel> result = new ResultViewModel<ProductViewModel>();
            result.List = new List<ProductViewModel>();
            var errors = new List<ErrorViewModel>();

            var productCategoryTypes = _productCategoryTypeService.GetAll(true, x => x.CategoryTypeID == categoryTypeID).ToList();

            foreach (var item in productCategoryTypes)
            {
                var product = item.Product;

                result.List.Add(_productService.MapToViewModel(product));
            }

            return result;

        }
    }
}
