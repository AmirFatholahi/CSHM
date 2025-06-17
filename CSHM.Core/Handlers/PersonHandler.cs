using CSHM.Core.Handlers.Interfaces;
using CSHM.Core.Services;
using CSHM.Core.Services.Interfaces;
using CSHM.Domain;
using CSHM.Presentation.Base;
using CSHM.Presentation.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Core.Handlers
{
    public class PersonHandler : IPersonHandler
    {

        private readonly IPersonService _personService;
        private readonly IPersonOccupationService _personOccupationService;
        public PersonHandler(IPersonService personService,IPersonOccupationService personOccupationService)
        {
            _personService = personService;
            _personOccupationService = personOccupationService;
            
        }

        public ResultViewModel<PersonViewModel> SelectAll(bool? activate, int? pageNumber = null, int pageSize = 20)
        {
            ResultViewModel<PersonViewModel> result = new ResultViewModel<PersonViewModel>();
            result.List = new List<PersonViewModel>();
            var errors = new List<ErrorViewModel>();

            var person = _personService.GetAll(true,null,pageNumber,pageSize).ToList();

            result.List = _personService.MapToViewModel(person);

            foreach (var item in result.List)
            {
                var list = _personOccupationService.GetAll(true, x => x.PersonID == item.ID).ToList();
                item.PersonOccupations = _personOccupationService.MapToViewModel(list);
            }

            return result;

        }

        public ResultViewModel<PersonViewModel> SelectAllPin(bool? activate, int? pageNumber = null, int pageSize = 20)
        {
            ResultViewModel<PersonViewModel> result = new ResultViewModel<PersonViewModel>();
            result.List = new List<PersonViewModel>();
            var errors = new List<ErrorViewModel>();

            var person = _personService.GetAll(true, x => x.IsPin == true, pageNumber, pageSize).ToList();

            result.List = _personService.MapToViewModel(person);

            foreach (var item in result.List)
            {
                var list = _personOccupationService.GetAll(true, x => x.PersonID == item.ID).ToList();
                item.PersonOccupations = _personOccupationService.MapToViewModel(list);
            }

            return result;

        }



        public ResultViewModel<PersonViewModel> SelectAllByOccupation(int occupationID)
        {
            ResultViewModel<PersonViewModel> result = new ResultViewModel<PersonViewModel>();
            result.List = new List<PersonViewModel>();
            var errors = new List<ErrorViewModel>();

            var personOccupatin = _personOccupationService.GetAll(true,x => x.OccupationID == occupationID).ToList();
            

            foreach (var item in personOccupatin) 
            {
                var person = item.Person;

                result.List.Add(_personService.MapToViewModel(person));
            }

            return result;

        }
    }
}
