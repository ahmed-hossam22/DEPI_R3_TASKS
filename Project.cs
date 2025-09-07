using System;
using System.Collections.Generic;

namespace EFDay1.Models;

public partial class Project
{
    public int Pnum { get; set; }

    public string Pname { get; set; } = null!;

    public string? Locationcity { get; set; }

    public int? Dnum { get; set; }

    public virtual Department? DnumNavigation { get; set; }

    public virtual ICollection<EmpProject> EmpProjects { get; set; } = new List<EmpProject>();
}
