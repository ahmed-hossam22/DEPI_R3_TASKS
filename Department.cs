using System;
using System.Collections.Generic;

namespace EFDay1.Models;

public partial class Department
{
    public int Dnum { get; set; }

    public string? Dname { get; set; }

    public string? Locations { get; set; }

    public int? Ssn { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual Employee? SsnNavigation { get; set; }
}
