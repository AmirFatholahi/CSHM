namespace CSHM.Presentations.Login;

public class PageViewModel
{
    public int ID { get; set; }

    public int ModuleID { get; set; }

    public string ModuleTitle { get; set; }

    public int? ParentID { get; set; }

    public string ParentTitle { get; set; }

    public string Title { get; set; }

    public string Name { get; set; }

    public string Path { get; set; }

    public string Icon { get; set; }

    public int Priority { get; set; }

    public string RootPath { get; set; }

    public List<string> Breadcrumbs { get; set; }

    public bool IsMenu { get; set; }

    public bool IsActive { get; set; }

}