
using CSHM.Domain;

namespace CSHM.Presentations.Login;

public class ControllerActionViewModel
{
    public int ID { get; set; }
    public int Code { get; set; }
    public int PageID { get; set; }
    public string ControllerName { get; set; }
    public string ActionName { get; set; }
    public string TitleFa { get; set; }
    public string TitleEn { get; set; }
    public int Priority { get; set; }
    public bool IsActive { get; set; }
    public Page Page { get; set; }

}