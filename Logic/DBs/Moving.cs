using System;
using System.Collections.Generic;

namespace Logic;

public partial class Moving
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string? Common { get; set; }

    public int Sum { get; set; }

    public int PayOptionId { get; set; }

    public int User2AreaId { get; set; }

    public virtual PayOption PayOption { get; set; } = null!;

    public virtual User2Area User2Area { get; set; } = null!;
}
