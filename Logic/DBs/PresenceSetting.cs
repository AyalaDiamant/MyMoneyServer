using System;
using System.Collections.Generic;

namespace Logic;

public partial class PresenceSetting
{
    public int PresenceId { get; set; }

    public int? UserId { get; set; }

    public int Day { get; set; }

    public int Hours { get; set; }

    public virtual User? User { get; set; }
}
