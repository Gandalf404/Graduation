using System;
using System.Collections.Generic;

namespace Graduation.Models.Admin;

public partial class DeleteMark
{
    public int DeleteMarkId { get; set; }

    public DateOnly DeleteMarkDate { get; set; }

    public int EmployeeId { get; set; }

    public string IsDeleted { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
