namespace CSHM.Presentation.Product;

public class ProductViewModel
{
    public int ID { get; set; }

    public int PublisherID { get; set; }

    public string PublisherTitle { get; set; }

    public int ProductTypeID { get; set; }

    public string ProductTypeTitle { get; set; }

    public string Author { get; set; }

    public int PublishTypeID { get; set; }

    public string PublishTypeitle { get; set; }

    public int LanguageID { get; set; }

    public string LanguageTitle { get; set; }

    public int SizeTypeID { get; set; }

    public string SizeTypeTitle { get; set; }

    public int CoverTypeID { get; set; }

    public string CoverTypeTitle { get; set; }

    public string ProductCode { get; set; }

    public string Title { get; set; }

    public long Price { get; set; }

    public long BeforePrice { get; set; }

    public decimal StudyTime { get; set; } 

    public string ISBN { get; set; }

    public int PublisheYear { get; set; } 

    public int PublishSeason { get; set; } 

    public int PublishTurn { get; set; } 

    public string PublishDate { get; set; } 

    public string MetaDercreption { get; set; }

    public int PageCount { get; set; }

    public int Weight { get; set; } 

    public int VisitedCount { get; set; }

    public decimal Rate { get; set; }

    public string Summary { get; set; }

    public bool IsNew { get; set; }

    public bool IsRecommended { get; set; }

    public bool IsSelected { get; set; }

    public bool IsSoon { get; set; }

    public bool IsActive { get; set; }

    public  List<ProductOccupationViewModel> ProductOccupations { get; set; }

    public List<ProductPropertyTypeViewModel> ProductPropertyTypes { get; set; }
}
