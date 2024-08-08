using System;
using System.Collections.Generic;

namespace Logic;

public partial class Debt
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public int Payments { get; set; }

    public int UrgencyId { get; set; }

    public int UserId { get; set; }

    public bool IsActive { get; set; }

    public int Sum { get; set; }

    public int? AreaId { get; set; }

    public int? PersonalExpenseId { get; set; }

    public virtual User2Area? Area { get; set; }

    public virtual UrgencyDebt Urgency { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<User2Area> User2Areas { get; set; } = new List<User2Area>();
}
