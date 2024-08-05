using System;
using System.Collections.Generic;

namespace Logic;

public partial class User2Area
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public bool? IsMaaser { get; set; }

    public int? Sum { get; set; }

    public bool? IsActive { get; set; }

    public int? DebtId { get; set; }

    public string Description { get; set; } = null!;

    public int Type { get; set; }

    public virtual Debt? Debt { get; set; }

    public virtual ICollection<Debt> Debts { get; set; } = new List<Debt>();

    public virtual ICollection<Moving> Movings { get; set; } = new List<Moving>();

    public virtual User User { get; set; } = null!;
}
