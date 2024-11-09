using CSHM.Widget.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CSHM.Presentation.Base;

namespace CSHM.Widget.Ticketum
{
    public interface ITicketumWidget
    {

        ResultViewModel<PriorityTypeViewModel> PriorityTypeSelectAll();
        ResultViewModel<TicketTypeViewModel> TicketTypeBySection(int sectionID);
        ResultViewModel<SectionViewModel> UserSelectSectionsByCurrentUser();

        ResultViewModel<ResponseTicketViewModel> MangeTicketSubmit(RequestTicketViewModel entity);
        ResultViewModel<TicketViewModel> MangeTicketList(string username, int pageNumber, int pageSize);
        ResultViewModel<TicketViewModel> MangeTicketList(FilterViewModel filter, string username, int pageNumber, int pageSize);
        ResultViewModel<TicketDetailViewModel> MangeTicketHistory(int ticketID, string username);
        ResultViewModel<FollowUpViewModel> MangeTicketFollowUp(string ticketCode);

        MessageViewModel UserGenerate(TicketUserViewModel entity);
        MessageViewModel MangeTicketResponse(RequestTicketViewModel entity);
        MessageViewModel ManageTicketCloseWithComment(CommentViewModel entity);
        FileContentResult ManageTicketDownload(int documentFileID, string username);

    }
}
