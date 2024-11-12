namespace CSHM.Presentation.Blog;

public class BlogPublisherViewModel
{
    public int ID { get; set; }

    public int BlogID { get; set; }

    public string BlogTitle { get; set; }

    public int PublisherID { get; set; }

    public string PublisherTitle { get; set; }

    public bool IsActive { get; set; }
}
