using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFDay1.Models;

public partial class EFContext : DbContext
{
    public EFContext()
    {
    }

    public EFContext(DbContextOptions<EFContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<EmpProject> EmpProjects { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-R3OL02D;Database=Session3task1;Trusted_Connection=True;Trust Server Certificate = true ;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Dnum).HasName("PK__Departme__7A775085AD00C435");

            entity.ToTable("Department");

            entity.HasIndex(e => e.Locations, "UQ__Departme__2B8A0A27C97E8F9F").IsUnique();

            entity.Property(e => e.Dname).HasMaxLength(255);
            entity.Property(e => e.Locations).HasMaxLength(255);
            entity.Property(e => e.Ssn).HasColumnName("SSN");

            entity.HasOne(d => d.SsnNavigation).WithMany(p => p.Departments)
                .HasForeignKey(d => d.Ssn)
                .HasConstraintName("FK__Department__SSN__571DF1D5");
        });

        modelBuilder.Entity<EmpProject>(entity =>
        {
            entity.HasKey(e => new { e.Ssn, e.Pnum }).HasName("PK__Emp_Proj__805F73CF9B13A7B3");

            entity.ToTable("Emp_Project");

            entity.Property(e => e.Ssn).HasColumnName("SSN");
            entity.Property(e => e.WorkingHours).HasColumnName("Working_Hours");

            entity.HasOne(d => d.PnumNavigation).WithMany(p => p.EmpProjects)
                .HasForeignKey(d => d.Pnum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Emp_Projec__Pnum__60A75C0F");

            entity.HasOne(d => d.SsnNavigation).WithMany(p => p.EmpProjects)
                .HasForeignKey(d => d.Ssn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Emp_Project__SSN__5FB337D6");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Ssn).HasName("PK__Employee__CA1E8E3D27FC1950");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.BirthDate, "UQ__Employee__B720CC44147AFD24").IsUnique();

            entity.Property(e => e.Ssn).HasColumnName("SSN");
            entity.Property(e => e.BirthDate).HasColumnName("Birth_Date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .HasColumnName("firstName");
            entity.Property(e => e.LasstName).HasMaxLength(30);
            entity.Property(e => e.SuperSsn).HasColumnName("Super_SSN");

            entity.HasOne(d => d.DnumNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Dnum)
                .HasConstraintName("FK__Employee__Dnum__5812160E");

            entity.HasOne(d => d.SuperSsnNavigation).WithMany(p => p.InverseSuperSsnNavigation)
                .HasForeignKey(d => d.SuperSsn)
                .HasConstraintName("FK__Employee__Super___534D60F1");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Pnum).HasName("PK__Projects__A41FDF259EDC7710");

            entity.HasIndex(e => e.Locationcity, "UQ__Projects__D02B965B2D4E99EF").IsUnique();

            entity.Property(e => e.Locationcity)
                .HasMaxLength(50)
                .HasColumnName("locationcity");
            entity.Property(e => e.Pname).HasMaxLength(50);

            entity.HasOne(d => d.DnumNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.Dnum)
                .HasConstraintName("FK__Projects__Dnum__5BE2A6F2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
