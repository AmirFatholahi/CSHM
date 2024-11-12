namespace CSHM.Presentation.Product;

public class ProductCommentViewModel
{
    public int ID { get; set; }

    public int UserID { get; set; }

    public string UserFullName { get; set; }

    public int ProductID { get; set; }

    public string ProductTitle { get; set; }

    public string Note { get; set; }

    public string NoteDate { get; set; }

    public string NoteTime { get; set; }

    public int Rate { get; set; }

    public bool IsActive { get; set; }
}
