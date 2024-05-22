﻿using System;
using System.Collections.Generic;

namespace Graduation.Models.Admin;

public partial class InvoicePau
{
    public int InvoiceId { get; set; }

    public DateOnly InvoiceCompilationDate { get; set; }

    public int PauId { get; set; }

    public int FactCount { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual Pau Pau { get; set; } = null!;
}
