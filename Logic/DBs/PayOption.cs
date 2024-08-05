using System;
using System.Collections.Generic;

namespace Logic;

public partial class PayOption
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int ManagerId { get; set; }

    public bool? IsActive { get; set; }

    public virtual User Manager { get; set; } = null!;

    public virtual ICollection<Moving> Movings { get; set; } = new List<Moving>();
}
