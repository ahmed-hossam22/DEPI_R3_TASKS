using System;
using System.Collections.Generic;

namespace EFDay1.Models;

public partial class Employee
{
    public int Ssn { get; set; }

    public string FirstName { get; set; } = null!;

    public string LasstName { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public int? SuperSsn { get; set; }

    public int? Dnum { get; set; }

    public int? Salary { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual Department? DnumNavigation { get; set; }

    public virtual ICollection<EmpProject> EmpProjects { get; set; } = new List<EmpProject>();

    public virtual ICollection<Employee> InverseSuperSsnNavigation { get; set; } = new List<Employee>();

    public virtual Employee? SuperSsnNavigation { get; set; }
}
