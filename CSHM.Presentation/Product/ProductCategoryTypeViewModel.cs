namespace CSHM.Presentation.Product;

public class ProductCategoryTypeViewModel
{
    public int ID { get; set; }

    public int ProductID { get; set; }

    public string ProductTitle { get; set; }

    public int CategoryTypeID { get; set; }

    public string CategoryTypeTitle { get; set; }

    public bool IsActive { get; set; }
}
