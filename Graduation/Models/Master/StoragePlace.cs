using System;
using System.Collections.Generic;

namespace Graduation.Models.Master;

public partial class StoragePlace
{
    public int StoragePlaceId { get; set; }

    public virtual ICollection<Pau> Paus { get; set; } = new List<Pau>();
}
