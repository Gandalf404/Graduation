using System;
using System.Collections.Generic;

namespace Graduation.Models;

public partial class Area
{
    public int AreaId { get; set; }

    public int DepartmentId { get; set; }

    public int AreaNumber { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
