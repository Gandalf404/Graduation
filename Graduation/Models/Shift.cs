using System;
using System.Collections.Generic;

namespace Graduation.Models;

public partial class Shift
{
    public int ShiftId { get; set; }

    public int ShiftNumber { get; set; }

    public int BrigadeNumber { get; set; }

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
