using System;
using System.Collections.Generic;

namespace Graduation.Models.Master;

public partial class Department
{
    public int DepartmentId { get; set; }

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();

    public virtual ICollection<Invoice> InvoiceDepartmentReceivers { get; set; } = new List<Invoice>();

    public virtual ICollection<Invoice> InvoiceDepartments { get; set; } = new List<Invoice>();
}
