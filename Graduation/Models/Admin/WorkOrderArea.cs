using System;
using System.Collections.Generic;

namespace Graduation.Models.Admin;

public partial class WorkOrderArea
{
    public int WorkOrderId { get; set; }

    public DateOnly WorkOrderCompilationDate { get; set; }

    public int AreaId { get; set; }

    public int OperationId { get; set; }

    public DateOnly OperationStartDate { get; set; }

    public TimeOnly OperationStartTime { get; set; }

    public DateOnly? OperationEndDate { get; set; }

    public TimeOnly? OperationEndTime { get; set; }

    public virtual Area Area { get; set; } = null!;

    public virtual Operation Operation { get; set; } = null!;

    public virtual WorkOrder WorkOrder { get; set; } = null!;
}
