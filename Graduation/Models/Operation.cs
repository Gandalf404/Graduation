using System;
using System.Collections.Generic;

namespace Graduation.Models;

public partial class Operation
{
    public int OperationId { get; set; }

    public int PauId { get; set; }

    public string OperationName { get; set; } = null!;

    public virtual Pau Pau { get; set; } = null!;
}
