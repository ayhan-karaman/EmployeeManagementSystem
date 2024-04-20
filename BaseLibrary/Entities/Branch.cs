﻿namespace BaseLibrary.Entities;

public class Branch : BaseEntity
{
    // Many to one relationship wit Department
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    // Relationship: One to Many with Employee
    public ICollection<Employee>? Employees { get; set; }
}
