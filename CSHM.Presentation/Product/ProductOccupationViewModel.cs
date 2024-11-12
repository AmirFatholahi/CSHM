namespace CSHM.Presentation.Product;

public class ProductOccupationViewModel
{
    public int ID { get; set; }

    public int ProductID { get; set; }

    public string ProductTitle{ get; set; }

    public int PersonOccupationID { get; set; }

    public string PersonOccupationTitle { get; set; }

    public bool IsActive { get; set; }
}
