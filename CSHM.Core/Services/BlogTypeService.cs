using AutoMapper;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.Blog;
using CSHM.Presentation.Resources;
using CSHM.Widget.Log;
using Nest;

namespace CSHM.Core.Services
{
    public class BlogTypeService :Repository<BlogType,BlogTypeViewModel>, IBlogTypeService
    {
        private readonly ILogWidget _log;
        private readonly IMapper _mapper;

        public BlogTypeService(DatabaseContext context, ILogWidget log, IMapper mapper) : base(context, log, mapper)
        {
            _log = log;
            _mapper = mapper;
        }

        public override ResultViewModel<BlogTypeViewModel> SelectAll(bool? activate, string filter = null, int? pageNumber = null, int pageSize = 20)
        {
            throw new NotImplementedException();
        }

        public List<ErrorViewModel> ValidationForm(BlogTypeViewModel entity)
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
            if (!string.IsNullOrEmpty(entity.Title) && entity.Title.Length > 250)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "عنوان", 250)
                });
            }

            if (!string.IsNullOrEmpty(entity.TitleEN) && entity.TitleEN.Length > 250)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "عنوان لاتین", 250)
                });
            }

            return result;
        }
    }
}
