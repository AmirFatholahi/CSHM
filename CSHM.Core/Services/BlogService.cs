using AutoMapper;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.Blog;
using CSHM.Presentation.Resources;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using Microsoft.AspNetCore.Hosting;


namespace CSHM.Core.Services
{
    public class BlogService : Repository<Blog, BlogViewModel>, IBlogService
    {
        private readonly ILogWidget _log;
        private readonly IMapper _mapper;
        private readonly IExcelWidget _excel;
        private readonly DatabaseContext _context;


        public BlogService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel, IHostingEnvironment hostingEnvironment) : base(context, log, mapper)
        {
            _log = log;
            _mapper = mapper;
            _excel = excel;
            _context = context;
        }



        private List<ErrorViewModel> ValidationForm(Blog entity)
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

            if (entity.BlogStatusTypeID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه وضعیت بلاگ")
                });
            }

            if (entity.BlogTypeID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه نوع بلاگ")
                });
            }


            //Max Length
            if (!string.IsNullOrEmpty(entity.Title) && entity.Title.Length > 150)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "عنوان", 150)
                });
            }

            if (!string.IsNullOrEmpty(entity.Summary) && entity.Summary.Length > 1000)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "خلاصه", 1000)
                });
            }

            if (!string.IsNullOrEmpty(entity.Content) && entity.Content.Length > 2000)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "محتوی", 2000)
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

            if (!string.IsNullOrEmpty(entity.CreationDate) && entity.CreationDate.Length > 10)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "تاریخ ایجاد", 10)
                });
            }

            if (!string.IsNullOrEmpty(entity.CreationTime) && entity.CreationTime.Length > 10)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "ساعت ایجاد", 10)
                });
            }

            return result;
        }
    }
}
