using System;
using System.Collections.Generic;

namespace Graduation.Models;

public partial class WorkOrder
{
    public int WorkOrderId { get; set; }

    public int EmployeeId { get; set; }

    public int PauId { get; set; }

    public int PauCount { get; set; }

    public DateOnly WorkOrderCreateDate { get; set; }

    public DateOnly? WorkOrderCloseDate { get; set; }

    public virtual ICollection<AcceptNote> AcceptNotes { get; set; } = new List<AcceptNote>();

    public virtual Employee Employee { get; set; } = null!;

    public virtual Pau Pau { get; set; } = null!;
}
