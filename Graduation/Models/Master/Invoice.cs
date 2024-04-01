using System;
using System.Collections.Generic;

namespace Graduation.Models.Master;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public DateOnly InvoiceCompilationDate { get; set; }

    public int DepartmentId { get; set; }

    public int DepartmentReceiverId { get; set; }

    public int WorkOrderId { get; set; }

    public DateOnly WorkOrderCompilationDate { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Department DepartmentReceiver { get; set; } = null!;

    public virtual InvoicePau? InvoicePau { get; set; }

    public virtual WorkOrder WorkOrder { get; set; } = null!;
}
