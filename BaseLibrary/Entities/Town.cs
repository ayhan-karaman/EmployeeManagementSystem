namespace BaseLibrary.Entities;

public class Town : BaseEntity
{
    //Relationship: One to many with Employee
    public ICollection<Employee>? Employees { get; set; }
    
    // Many to one relationship with City
    public int CityId { get; set; }
    public City? City { get; set; }
}
