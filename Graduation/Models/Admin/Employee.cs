using System;
using System.Collections.Generic;

namespace Graduation.Models.Admin;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeSurname { get; set; } = null!;

    public string EmployeeName { get; set; } = null!;

    public string? EmployeePatronymic { get; set; }

    public int AreaId { get; set; }

    public int PositionId { get; set; }

    public int ClassId { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual Area Area { get; set; } = null!;

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<DeleteMark> DeleteMarks { get; set; } = new List<DeleteMark>();

    public virtual Position Position { get; set; } = null!;

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();

    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
