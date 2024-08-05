using System;
using System.Collections.Generic;

namespace Logic;

public partial class Presence
{
    public int Id { get; set; }

    public DateTime Start { get; set; }

    public DateTime Finish { get; set; }

    public string? Note { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
