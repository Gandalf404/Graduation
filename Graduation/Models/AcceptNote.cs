using System;
using System.Collections.Generic;

namespace Graduation.Models;

public partial class AcceptNote
{
    public int AcceptNoteId { get; set; }

    public int EmployeeId { get; set; }

    public int WorkOrderId { get; set; }

    public DateOnly AcceptNoteCreateDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual WorkOrder WorkOrder { get; set; } = null!;
}
