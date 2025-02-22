namespace CSHM.Presentation.Product;

public class ProductOccupationViewModel
{
    public int ID { get; set; }

    public int ProductID { get; set; }


    public string ProductTitle{ get; set; }

    public int PersonOccupationID { get; set; }

    public int OccupationID { get; set; }

    public string OccupationTitle { get; set; }

    public int PersonID { get; set; }

    public string FullName { get; set; }



    public bool IsActive { get; set; }
}
