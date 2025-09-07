using System;
using System.Collections.Generic;

namespace EFDay1.Models;

public partial class EmpProject
{
    public int Ssn { get; set; }

    public int Pnum { get; set; }

    public int? WorkingHours { get; set; }

    public virtual Project PnumNavigation { get; set; } = null!;

    public virtual Employee SsnNavigation { get; set; } = null!;
}
