using System;
using System.Collections.Generic;

namespace Graduation.Models.Master;

public partial class Reservation
{
    public int ReservationId { get; set; }

    public DateOnly ReservationCompilationDate { get; set; }

    public int ReservationCount { get; set; }

    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
