using System;
using System.Collections.Generic;

namespace Graduation.Models.Master;

public partial class Class
{
    public int ClassId { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
