using System;
using System.Collections.Generic;

namespace Graduation.Models.Master;

public partial class EmployeeView
{
    public int? ТабельныйНомерСотрудника { get; set; }

    public string? Фамилия { get; set; }

    public string? Имя { get; set; }

    public string? Отчество { get; set; }

    public int? Участок { get; set; }

    public string? Должность { get; set; }

    public int? Разряд { get; set; }
}
