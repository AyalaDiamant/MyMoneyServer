using System;
using System.Collections.Generic;

namespace Logic;

public partial class Task
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int UserId { get; set; }

    public int StatusId { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? DoDate { get; set; }

    public int? UrgencyId { get; set; }

    public string? Comment { get; set; }

    public virtual Status Status { get; set; } = null!;

    public virtual UrgencyDebt? Urgency { get; set; }

    public virtual User User { get; set; } = null!;
}
