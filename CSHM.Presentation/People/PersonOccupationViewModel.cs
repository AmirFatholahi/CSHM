namespace CSHM.Presentation.People;

public class PersonOccupationViewModel
{
    public int ID { get; set; }

    public int PersonID { get; set; }

    public string PersonFullName { get; set; }

    public int OccupationID { get; set; }

    public string OccupationTitle { get; set; }

    public string Title { get; set; }

    public bool IsActive { get; set; }
}
