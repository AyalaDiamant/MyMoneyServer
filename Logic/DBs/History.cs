using Logic.DTO;
using System;
using System.Collections.Generic;

namespace Logic;

public partial class History
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime DateofChange { get; set; }

    public string OldDomain { get; set; } = null!;

    public string NewDomain { get; set; } = null!;

    public int? OldAmount { get; set; }

    public int? NewAmount { get; set; }
    public ActionOptions ActionOption { get; set; }  


    public virtual User User { get; set; } = null!;
}
