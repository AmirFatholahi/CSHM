namespace CSHM.Presentations.Login;

public class MenuViewModel
{
    public string title { get; set; }
    public string path { get; set; }
    public string icon { get; set; }
    public string type { get; set; }
    public bool active { get; set; }
    public List<MenuViewModel> children { get; set; }

    public bool Menusub { get; set; }
}