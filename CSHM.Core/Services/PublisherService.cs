using AutoMapper;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.Publish;
using CSHM.Presentation.Resources;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using Microsoft.AspNetCore.Hosting;


namespace CSHM.Core.Services
{
    public class PublisherService : Repository<Publisher, PublisherViewModel>, IPublisherService
    {
        private readonly ILogWidget _log;
        private readonly IMapper _mapper;
        private readonly IExcelWidget _excel;
        private readonly DatabaseContext _context;


        public PublisherService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel, IHostingEnvironment hostingEnvironment) : base(context, log, mapper)
        {
            _log = log;
            _mapper = mapper;
            _excel = excel;
            _context = context;
        }


        private List<ErrorViewModel> ValidationForm(Publisher entity)
        {
            var result = new List<ErrorViewModel>();


            //Max Length
            if (!string.IsNullOrEmpty(entity.Title) && entity.Title.Length > 250)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "عنوان", 250)
                });
            }

            if (!string.IsNullOrEmpty(entity.Phone) && entity.Phone.Length > 11)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "شماره ثابت", 11)
                });
            }

            if (!string.IsNullOrEmpty(entity.Address) && entity.Address.Length > 500)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "نشانی", 500)
                });
            }

            if (!string.IsNullOrEmpty(entity.Cellphone) && entity.Cellphone.Length > 11)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "شماره همراه", 11)
                });
            }

            if (!string.IsNullOrEmpty(entity.Website) && entity.Website.Length > 100)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "وب سایت", 100)
                });
            }

            if (!string.IsNullOrEmpty(entity.Email) && entity.Email.Length > 50)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "ایمیل", 50)
                });
            }
            return result;
        }
    }
}
