using System;
using System.Collections.Generic;

namespace Graduation.Models.Master;

public partial class DeleteMark
{
    public int DeleteMarkId { get; set; }

    public DateOnly DeleteMarkDate { get; set; }

    public int EmployeeId { get; set; }

    public string DeleteMark1 { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
