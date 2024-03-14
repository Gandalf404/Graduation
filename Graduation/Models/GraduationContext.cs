using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Models;

public partial class GraduationContext : DbContext
{
    public GraduationContext()
    {
    }

    public GraduationContext(DbContextOptions<GraduationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcceptNote> AcceptNotes { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Experience> Experiences { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<Pau> Paus { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<StoragePlace> StoragePlaces { get; set; }

    public virtual DbSet<WorkOrder> WorkOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Database=Graduation;Username=postgres;Password=mclooter131;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcceptNote>(entity =>
        {
            entity.HasKey(e => e.AcceptNoteId).HasName("accept_note_pkey");

            entity.ToTable("accept_note");

            entity.Property(e => e.AcceptNoteId).HasColumnName("accept_note_id");
            entity.Property(e => e.AcceptNoteCreateDate).HasColumnName("accept_note_create_date");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.WorkOrderId).HasColumnName("work_order_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.AcceptNotes)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("accept_note_employee_id_fkey");

            entity.HasOne(d => d.WorkOrder).WithMany(p => p.AcceptNotes)
                .HasForeignKey(d => d.WorkOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("accept_note_work_order_id_fkey");
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.AreaId).HasName("area_pkey");

            entity.ToTable("area");

            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.AreaNumber).HasColumnName("area_number");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");

            entity.HasOne(d => d.Department).WithMany(p => p.Areas)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("area_department_id_fkey");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("department_pkey");

            entity.ToTable("department");

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.DepartmentNumber).HasColumnName("department_number");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.EmployeeLogin)
                .HasMaxLength(20)
                .HasColumnName("employee_login");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(20)
                .HasColumnName("employee_name");
            entity.Property(e => e.EmployeePassword)
                .HasMaxLength(20)
                .HasColumnName("employee_password");
            entity.Property(e => e.EmployeePatronymic)
                .HasMaxLength(20)
                .HasColumnName("employee_patronymic");
            entity.Property(e => e.EmployeeSurname)
                .HasMaxLength(20)
                .HasColumnName("employee_surname");
            entity.Property(e => e.ExperienceId).HasColumnName("experience_id");
            entity.Property(e => e.IsBrigadier)
                .HasColumnType("bit(1)")
                .HasColumnName("is_brigadier");
            entity.Property(e => e.PositionId).HasColumnName("position_id");

            entity.HasOne(d => d.Area).WithMany(p => p.Employees)
                .HasForeignKey(d => d.AreaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_area_id_fkey");

            entity.HasOne(d => d.Experience).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ExperienceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_experience_id_fkey");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_position_id_fkey");
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.HasKey(e => e.ExperienceId).HasName("experience_pkey");

            entity.ToTable("experience");

            entity.Property(e => e.ExperienceId).HasColumnName("experience_id");
            entity.Property(e => e.Experience1).HasColumnName("experience");
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(e => e.OperationId).HasName("operation_pkey");

            entity.ToTable("operation");

            entity.Property(e => e.OperationId).HasColumnName("operation_id");
            entity.Property(e => e.OperationName)
                .HasMaxLength(100)
                .HasColumnName("operation_name");
            entity.Property(e => e.PauId).HasColumnName("pau_id");

            entity.HasOne(d => d.Pau).WithMany(p => p.Operations)
                .HasForeignKey(d => d.PauId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("operation_pau_id_fkey");
        });

        modelBuilder.Entity<Pau>(entity =>
        {
            entity.HasKey(e => e.PauId).HasName("pau_pkey");

            entity.ToTable("pau");

            entity.Property(e => e.PauId).HasColumnName("pau_id");
            entity.Property(e => e.PauBlueprint).HasColumnName("pau_blueprint");
            entity.Property(e => e.PauCount).HasColumnName("pau_count");
            entity.Property(e => e.PauName)
                .HasMaxLength(20)
                .HasColumnName("pau_name");
            entity.Property(e => e.StoragePlaceId).HasColumnName("storage_place_id");

            entity.HasOne(d => d.StoragePlace).WithMany(p => p.Paus)
                .HasForeignKey(d => d.StoragePlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pau_storage_place_id_fkey");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("position_pkey");

            entity.ToTable("position");

            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.PositionName)
                .HasMaxLength(50)
                .HasColumnName("position_name");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("shift_pkey");

            entity.ToTable("shift");

            entity.Property(e => e.ShiftId).HasColumnName("shift_id");
            entity.Property(e => e.BrigadeNumber).HasColumnName("brigade_number");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.ShiftNumber).HasColumnName("shift_number");

            entity.HasOne(d => d.Employee).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shift_employee_id_fkey");
        });

        modelBuilder.Entity<StoragePlace>(entity =>
        {
            entity.HasKey(e => e.StoragePlaceId).HasName("storage_place_pkey");

            entity.ToTable("storage_place");

            entity.Property(e => e.StoragePlaceId).HasColumnName("storage_place_id");
            entity.Property(e => e.StoragePlaceNumber).HasColumnName("storage_place_number");
        });

        modelBuilder.Entity<WorkOrder>(entity =>
        {
            entity.HasKey(e => e.WorkOrderId).HasName("work_order_pkey");

            entity.ToTable("work_order");

            entity.Property(e => e.WorkOrderId).HasColumnName("work_order_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.PauCount).HasColumnName("pau_count");
            entity.Property(e => e.PauId).HasColumnName("pau_id");
            entity.Property(e => e.WorkOrderCloseDate).HasColumnName("work_order_close_date");
            entity.Property(e => e.WorkOrderCreateDate).HasColumnName("work_order_create_date");

            entity.HasOne(d => d.Employee).WithMany(p => p.WorkOrders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_order_employee_id_fkey");

            entity.HasOne(d => d.Pau).WithMany(p => p.WorkOrders)
                .HasForeignKey(d => d.PauId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_order_pau_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
