using System;
using System.Collections.Generic;

namespace Logic;

public partial class AmountSetting
{
    public int ProductId { get; set; }

    public int? UserId { get; set; }

    public int Day { get; set; }

    public string Product { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public int ProductValue { get; set; }

    public int ProductQuantity { get; set; }

    public virtual User? User { get; set; }
}
