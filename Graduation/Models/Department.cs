using System;
using System.Collections.Generic;

namespace Graduation.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public int DepartmentNumber { get; set; }

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();
}
