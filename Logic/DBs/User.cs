using System;
using System.Collections.Generic;

namespace Logic;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int UserTypeId { get; set; }

    public string? Phone { get; set; }

    public bool IsActive { get; set; }

    public int? LenderId { get; set; }

    public DateTime RegisterDate { get; set; }

    public bool IsYearlyPay { get; set; }

    public DateTime? PayDate { get; set; }

    public string LastName { get; set; } = null!;

    public int? ManagerId { get; set; }

    public bool? Isbusiness { get; set; }

    public virtual ICollection<AmountSetting> AmountSettings { get; set; } = new List<AmountSetting>();

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<Debt> Debts { get; set; } = new List<Debt>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<User> InverseLender { get; set; } = new List<User>();

    public virtual ICollection<User> InverseManager { get; set; } = new List<User>();

    public virtual User? Lender { get; set; }

    public virtual User? Manager { get; set; }

    public virtual ICollection<ManagerDesign> ManagerDesigns { get; set; } = new List<ManagerDesign>();

    public virtual ICollection<ManagerSetting> ManagerSettings { get; set; } = new List<ManagerSetting>();

    public virtual ICollection<PayOption> PayOptions { get; set; } = new List<PayOption>();

    public virtual ICollection<PresenceSetting> PresenceSettings { get; set; } = new List<PresenceSetting>();

    public virtual ICollection<Presence> Presences { get; set; } = new List<Presence>();

    public virtual ICollection<ProductSettingsForDay> ProductSettingsForDays { get; set; } = new List<ProductSettingsForDay>();

    public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<UrgencyDebt> UrgencyDebts { get; set; } = new List<UrgencyDebt>();

    public virtual ICollection<User2Area> User2Areas { get; set; } = new List<User2Area>();

    public virtual UserType UserType { get; set; } = null!;
}
