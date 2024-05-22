using System;
using System.Collections.Generic;

namespace Graduation.Models.Master;

public partial class PauView
{
    public byte[]? Чертёж { get; set; }

    public int? НомерДсе { get; set; }

    public int? КодМестаХранения { get; set; }

    public string? НаименованиеДсе { get; set; }

    public int? КоличествоДсе { get; set; }
}
