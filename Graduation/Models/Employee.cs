using System;
using System.Collections;
using System.Collections.Generic;

namespace Graduation.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeSurname { get; set; } = null!;

    public string EmployeeName { get; set; } = null!;

    public string? EmployeePatronymic { get; set; }

    public int AreaId { get; set; }

    public int PositionId { get; set; }

    public int ExperienceId { get; set; }

    public BitArray? IsBrigadier { get; set; }

    public string? EmployeeLogin { get; set; }

    public string? EmployeePassword { get; set; }

    public virtual ICollection<AcceptNote> AcceptNotes { get; set; } = new List<AcceptNote>();

    public virtual Area Area { get; set; } = null!;

    public virtual Experience Experience { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();

    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
