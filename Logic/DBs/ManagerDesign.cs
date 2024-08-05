using System;
using System.Collections.Generic;

namespace Logic;

public partial class ManagerDesign
{
    public int Id { get; set; }

    public byte[]? ImageContent { get; set; }

    public string? Title { get; set; }

    public string? Slogan { get; set; }

    public string? HeaderColor { get; set; }

    public int ManagerId { get; set; }

    public string? TextColor { get; set; }

    public string? FileName { get; set; }

    public virtual User Manager { get; set; } = null!;
}
