using System;
using System.Collections.Generic;

namespace Graduation.Models.Master;

public partial class Authorisation
{
    public int EmployeeId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
