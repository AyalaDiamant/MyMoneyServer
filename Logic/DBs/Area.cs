using System;
using System.Collections.Generic;

namespace Logic;

public partial class Area
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int Type { get; set; }

    public int? ManagerId { get; set; }

    public virtual User? Manager { get; set; }
}
