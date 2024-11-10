using AutoMapper;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.People;
using CSHM.Presentation.Resources;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using Microsoft.AspNetCore.Hosting;


namespace CSHM.Core.Services
{
    public class PersonService : Repository<Person, PersonViewModel>, IPersonService
    {
        private readonly ILogWidget _log;
        private readonly IMapper _mapper;
        private readonly IExcelWidget _excel;
        private readonly DatabaseContext _context;


        public PersonService(DatabaseContext context, ILogWidget log, IMapper mapper, IExcelWidget excel, IHostingEnvironment hostingEnvironment) : base(context, log, mapper)
        {
            _log = log;
            _mapper = mapper;
            _excel = excel;
            _context = context;
        }


        private List<ErrorViewModel> ValidationForm(Person entity)
        {
            var result = new List<ErrorViewModel>();

            //Required
            if (entity.GeoCountryID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه کشور")
                });
            }

            if (entity.GenderTypeID <= 0)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error930,
                    ErrorMessage = string.Format(Messages.FieldIsRequired, "شناسه جنسیت")
                });
            }


            //Max Length
            if (!string.IsNullOrEmpty(entity.FirstName) && entity.FirstName.Length > 70)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "نام", 70)
                });
            }

            if (!string.IsNullOrEmpty(entity.FullName) && entity.FullName.Length > 100)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "نام کامل", 100)
                });
            }

            if (!string.IsNullOrEmpty(entity.AliasName) && entity.AliasName.Length > 100)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "نام مستعار", 100)
                });
            }

            if (!string.IsNullOrEmpty(entity.BirthDate) && entity.BirthDate.Length > 10)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "تاریخ تولد", 10)
                });
            }

            if (!string.IsNullOrEmpty(entity.Biography) && entity.Biography.Length > 1000)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "درباره من", 1000)
                });
            }
            return result;
        }
    }
}
