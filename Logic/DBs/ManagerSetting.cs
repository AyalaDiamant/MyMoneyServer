using System;
using System.Collections.Generic;

namespace Logic;

public partial class ManagerSetting
{
    public int Id { get; set; }

    public int ManagerId { get; set; }

    public string? OrganizationName { get; set; }

    public string? Phon { get; set; }

    public string? Email { get; set; }

    public string? Adress { get; set; }

    public string? ContactMen { get; set; }

    public int? OrganizationType { get; set; }

    public virtual User Manager { get; set; } = null!;
}
