using System;
using System.Collections.Generic;

namespace Graduation.Models;

public partial class Pau
{
    public int PauId { get; set; }

    public int StoragePlaceId { get; set; }

    public string PauName { get; set; } = null!;

    public int PauCount { get; set; }

    public byte[]? PauBlueprint { get; set; }

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();

    public virtual StoragePlace StoragePlace { get; set; } = null!;

    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
