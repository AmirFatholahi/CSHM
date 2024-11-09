namespace CSHM.Presentations.Login;

public class NavigationViewModel
{
    public int ID { get; set; }
    public int? ParentID { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Icon { get; set; }
    public int Priority { get; set; }
    public bool IsMenu { get; set; }
    public bool IsActive { get; set; }
    public List<NavigationViewModel> Children { get; set; }
}