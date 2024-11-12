namespace CSHM.Presentation.Blog;

public class BlogOccupationViewModel
{
    public int ID { get; set; }

    public int BlogID { get; set; }

    public string BlogTitle { get; set; }

    public int PersonOccupationID { get; set; }

    public string PersonOccupationTitle { get; set; }

    public bool IsActive { get; set; }
}
