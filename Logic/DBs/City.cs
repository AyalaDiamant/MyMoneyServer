using System;
using System.Collections.Generic;

namespace Logic;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? ManagerId { get; set; }

    public virtual User? Manager { get; set; }
}
