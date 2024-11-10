using AutoMapper;
using CSHM.Core.Repositories;
using CSHM.Core.Services.Interfaces;
using CSHM.Data.Context;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.Language;
using CSHM.Presentation.Resources;
using CSHM.Widget.Log;


namespace CSHM.Core.Services
{
    public class LanguageService : Repository<Language, LanguageViewModel>, ILanguageService
    {
        private readonly ILogWidget _log;
        private readonly IMapper _mapper;

        public LanguageService(DatabaseContext context, ILogWidget log, IMapper mapper) : base(context, log, mapper)
        {
            _log = log;
            _mapper = mapper;
        }



        public List<ErrorViewModel> ValidationForm(LanguageViewModel entity)
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
            if (!string.IsNullOrEmpty(entity.Title) && entity.Title.Length > 100)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "عنوان", 100)
                });
            }

            if (!string.IsNullOrEmpty(entity.Abbreviation) && entity.Abbreviation.Length > 5)
            {
                result.Add(new ErrorViewModel()
                {
                    ErrorCode = Errors.Error931,
                    ErrorMessage = string.Format(Messages.FieldMaxLengthExceeded, "اختصار", 5)
                });
            }



            return result;
        }
    }
}
