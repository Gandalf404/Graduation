using System;
using System.Collections.Generic;

namespace Graduation.Models;

public partial class Experience
{
    public int ExperienceId { get; set; }

    public int Experience1 { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
