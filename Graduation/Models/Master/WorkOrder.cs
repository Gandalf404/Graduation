using System;
using System.Collections.Generic;

namespace Graduation.Models.Master;

public partial class WorkOrder
{
    public int WorkOrderId { get; set; }

    public DateOnly WorkOrderCompilationDate { get; set; }

    public int PauId { get; set; }

    public int ReservationId { get; set; }

    public DateOnly ReservationCompilationDate { get; set; }

    public int EmployeeId { get; set; }

    public int PauCount { get; set; }

    public DateOnly? WorkOrderCompleteDate { get; set; }

    public DateOnly? WorkOrderCloseDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual Pau Pau { get; set; } = null!;

    public virtual Reservation Reservation { get; set; } = null!;

    public virtual ICollection<WorkOrderArea> WorkOrderAreas { get; set; } = new List<WorkOrderArea>();
}
