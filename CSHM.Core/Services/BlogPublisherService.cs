using AutoMapper;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain.Models;
using CSHM.Presentation.Base;
using CSHM.Presentation.Blog;
using CSHM.Presentation.Publish;
using CSHM.Presentation.Resources;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using CSHM.Widget.Method;
using Microsoft.AspNetCore.Hosting;
using System.Linq.Expressions;
using System.Reflection;


namespace CSHM.Core.Services
{
    public class BlogPublisherService : Repository<BlogPublisher, BlogPublisherViewModel>, IBlogPublisherService
    {
        private readonly ILogWidget _log;
        private readonly IMapper _mapper;
        private readonly IExcelWidget _excel;
        private readonly DatabaseContext _context;


        public BlogPublisherService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel, IHostingEnvironment hostingEnvironment) : base(context, log, mapper)
        {
            _log = log;
            _mapper = mapper;
            _excel = excel;
            _context = context;
        }

        public override ResultViewModel<BlogPublisherViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
        {
            var result = new ResultViewModel<BlogPublisherViewModel>();
            try
            {
                IQueryable<BlogPublisher> items;
                Expression<Func<BlogPublisher, bool>> condition = x => (string.IsNullOrWhiteSpace(filter) );
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

        private List<ErrorViewModel> ValidationForm(BlogPublisher entity)
        {
            var result = new List<ErrorViewModel>();

            //Required
            if (entity.BlogID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه بلاگ")
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
            return result;
        }
    }
}
