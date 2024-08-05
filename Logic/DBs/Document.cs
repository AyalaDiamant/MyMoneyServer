using System;
using System.Collections.Generic;

namespace Logic;

public partial class Document
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public byte[] Content { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
