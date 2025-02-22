namespace CSHM.Presentation.Blog;

public class BlogViewModel
{
    public int ID { get; set; }

    public int BlogTypeID { get; set; }

    public string BlogTypeTitle { get; set; }

    public int BlogStatusTypeID { get; set; }

    public string BlogStatusTypeTitle { get; set; }

    public int PublisherID { get; set; }

    public string PublisherTitle { get; set; }

    public string Title { get; set; }

    public string Summary { get; set; }

    public string Content { get; set; }

    public string MetaDescription { get; set; }

    public string CreationDate { get; set; }

    public string CreationTime { get; set; }

    public int VisitedCount { get; set; }

    public bool IsActive { get; set; }
}
