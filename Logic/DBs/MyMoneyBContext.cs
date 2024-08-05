using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Logic;

public partial class MyMoneyBContext : DbContext
{
    public MyMoneyBContext()
    {
    }

    public MyMoneyBContext(DbContextOptions<MyMoneyBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AmountSetting> AmountSettings { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Debt> Debts { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<ManagerDesign> ManagerDesigns { get; set; }

    public virtual DbSet<Moving> Movings { get; set; }

    public virtual DbSet<PayOption> PayOptions { get; set; }

    public virtual DbSet<Presence> Presences { get; set; }

    public virtual DbSet<PresenceSetting> PresenceSettings { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<UrgencyDebt> UrgencyDebts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<User2Area> User2Areas { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
         //=> optionsBuilder.UseSqlServer("Server=DESKTOP-OP3HLHL;Database=MyMoneyB;Trusted_Connection=True;TrustServerCertificate=true;");
         => optionsBuilder.UseSqlServer("Server=.;Database=MyMoneyB;user id=kollel;password=1234qwe!;TrustServerCertificate=true");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AmountSetting>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__ProductS__B40CC6CDDD9F23C1");

            entity.Property(e => e.Day).HasColumnName("day");

            entity.HasOne(d => d.User).WithMany(p => p.AmountSettings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ProductSe__UserI__68487DD7");
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Subject");

            entity.ToTable("Area");

            entity.HasOne(d => d.Manager).WithMany(p => p.Areas)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Area__ManagerId__73BA3083");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cities__3214EC07AB44D064");

            entity.HasOne(d => d.Manager).WithMany(p => p.Cities)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Cities__ManagerI__72C60C4A");
        });

        modelBuilder.Entity<Debt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Debts__3214EC074C897754");

            entity.HasOne(d => d.Area).WithMany(p => p.Debts)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("fk_Debts_User2Area");

            entity.HasOne(d => d.Urgency).WithMany(p => p.Debts)
                .HasForeignKey(d => d.UrgencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Debts__UrgencyId__3F466844");

            entity.HasOne(d => d.User).WithMany(p => p.Debts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Debts__UserId__75A278F5");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC076D60C1AA");

            entity.Property(e => e.Content).HasColumnType("image");

            entity.HasOne(d => d.User).WithMany(p => p.Documents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documents__UserI__160F4887");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__History__4D7B4ABD0DD16D06");

            entity.ToTable("History");

            entity.Property(e => e.DateofChange).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Histories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__History__Id__6B24EA82");
        });

        modelBuilder.Entity<ManagerDesign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ManagerD__3214EC07240F01BC");

            entity.ToTable("ManagerDesign");

            entity.Property(e => e.ImageContent).HasColumnType("image");

            entity.HasOne(d => d.Manager).WithMany(p => p.ManagerDesigns)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ManagerId");
        });

        modelBuilder.Entity<Moving>(entity =>
        {
            entity.ToTable("Moving");

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.PayOption).WithMany(p => p.Movings)
                .HasForeignKey(d => d.PayOptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Moving_PayOption");

            entity.HasOne(d => d.User2Area).WithMany(p => p.Movings)
                .HasForeignKey(d => d.User2AreaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Moving_User2Area");
        });

        modelBuilder.Entity<PayOption>(entity =>
        {
            entity.ToTable("PayOption");

            entity.HasOne(d => d.Manager).WithMany(p => p.PayOptions)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PayOption_User");
        });

        modelBuilder.Entity<Presence>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Presence__3214EC07D7E4FABB");

            entity.ToTable("Presence");

            entity.Property(e => e.Finish).HasColumnType("datetime");
            entity.Property(e => e.Note).IsUnicode(false);
            entity.Property(e => e.Start).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Presences)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Presence__UserId__71D1E811");
        });

        modelBuilder.Entity<PresenceSetting>(entity =>
        {
            entity.HasKey(e => e.PresenceId).HasName("PK__Presence__4980E863681EB51C");

            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.Hours).HasColumnName("hours");

            entity.HasOne(d => d.User).WithMany(p => p.PresenceSettings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__PresenceS__UserI__656C112C");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Status__3214EC075300C18E");

            entity.ToTable("Status");

            entity.HasOne(d => d.Manager).WithMany(p => p.Statuses)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Status__ManagerI__74AE54BC");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tasks__3214EC07A4173A94");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DoDate).HasColumnType("datetime");

            entity.HasOne(d => d.Status).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__StatusId__2CF2ADDF");

            entity.HasOne(d => d.Urgency).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.UrgencyId)
                .HasConstraintName("FK__Tasks__UrgencyId__2DE6D218");

            entity.HasOne(d => d.User).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__UserId__2BFE89A6");
        });

        modelBuilder.Entity<UrgencyDebt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UrgencyD__3214EC07EEB2094F");

            entity.ToTable("UrgencyDebt");

            entity.HasOne(d => d.Manager).WithMany(p => p.UrgencyDebts)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__UrgencyDe__Manag__75A278F5");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.PayDate).HasColumnType("datetime");
            entity.Property(e => e.RegisterDate).HasColumnType("datetime");

            entity.HasOne(d => d.Lender).WithMany(p => p.InverseLender)
                .HasForeignKey(d => d.LenderId)
                .HasConstraintName("FK_User_User");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__User__ManagerId__17036CC0");

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_UserType");
        });

        modelBuilder.Entity<User2Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User2Are__3214EC074E3F07E1");

            entity.ToTable("User2Area");

            entity.Property(e => e.IsActive).HasDefaultValue(false);
            entity.Property(e => e.IsMaaser).HasDefaultValue(false);
            entity.Property(e => e.Type).HasDefaultValue(1);

            entity.HasOne(d => d.Debt).WithMany(p => p.User2Areas)
                .HasForeignKey(d => d.DebtId)
                .HasConstraintName("FK__User2Subj__DebtI__02FC7413");

            entity.HasOne(d => d.User).WithMany(p => p.User2Areas)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User2Area_User");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.ToTable("UserType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
