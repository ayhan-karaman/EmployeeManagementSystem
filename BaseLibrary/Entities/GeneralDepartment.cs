namespace BaseLibrary.Entities;

public class GeneralDepartment : BaseEntity
{
    // One to many relationship with Department
    public ICollection<Department>? Departments { get; set; }
}
