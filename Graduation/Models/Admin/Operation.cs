using System;
using System.Collections.Generic;

namespace Graduation.Models.Admin;

public partial class Operation
{
    public int OperationId { get; set; }

    public string OperationName { get; set; } = null!;

    public virtual ICollection<WorkOrderArea> WorkOrderAreas { get; set; } = new List<WorkOrderArea>();
}
