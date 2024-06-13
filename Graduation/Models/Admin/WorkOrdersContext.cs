using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Models.Admin;

public partial class WorkOrdersContext : DbContext
{
    public WorkOrdersContext()
    {
    }

    public WorkOrdersContext(DbContextOptions<WorkOrdersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<DeleteMark> DeleteMarks { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoicePau> InvoicePaus { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<Pau> Paus { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<StoragePlace> StoragePlaces { get; set; }

    public virtual DbSet<WorkOrder> WorkOrders { get; set; }

    public virtual DbSet<WorkOrderArea> WorkOrderAreas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=192.168.1.112;Database=WorkOrders;Username=admin;Password=admin1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.AreaId).HasName("area_pkey");

            entity.ToTable("area");

            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");

            entity.HasOne(d => d.Department).WithMany(p => p.Areas)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("area_department_id_fkey");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("class_pkey");

            entity.ToTable("class");

            entity.Property(e => e.ClassId).HasColumnName("class_id");
        });

        modelBuilder.Entity<DeleteMark>(entity =>
        {
            entity.HasKey(e => new { e.DeleteMarkId, e.DeleteMarkDate }).HasName("delete_mark_pkey");

            entity.ToTable("delete_mark");

            entity.Property(e => e.DeleteMarkId)
                .ValueGeneratedOnAdd()
                .HasColumnName("delete_mark_id");
            entity.Property(e => e.DeleteMarkDate).HasColumnName("delete_mark_date");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.IsDeleted)
                .HasMaxLength(20)
                .HasColumnName("is_deleted");

            entity.HasOne(d => d.Employee).WithMany(p => p.DeleteMarks)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("delete_mark_employee_id_fkey");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("department_pkey");

            entity.ToTable("department");

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(20)
                .HasColumnName("employee_name");
            entity.Property(e => e.EmployeePatronymic)
                .HasMaxLength(20)
                .HasColumnName("employee_patronymic");
            entity.Property(e => e.EmployeeSurname)
                .HasMaxLength(20)
                .HasColumnName("employee_surname");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.PositionId).HasColumnName("position_id");

            entity.HasOne(d => d.Area).WithMany(p => p.Employees)
                .HasForeignKey(d => d.AreaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_area_id_fkey");

            entity.HasOne(d => d.Class).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_class_id_fkey");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_position_id_fkey");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => new { e.InvoiceId, e.InvoiceCompilationDate }).HasName("invoice_pkey");

            entity.ToTable("invoice");

            entity.Property(e => e.InvoiceId)
                .ValueGeneratedOnAdd()
                .HasColumnName("invoice_id");
            entity.Property(e => e.InvoiceCompilationDate).HasColumnName("invoice_compilation_date");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.DepartmentReceiverId).HasColumnName("department_receiver_id");
            entity.Property(e => e.WorkOrderCompilationDate).HasColumnName("work_order_compilation_date");
            entity.Property(e => e.WorkOrderId).HasColumnName("work_order_id");

            entity.HasOne(d => d.Department).WithMany(p => p.InvoiceDepartments)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_department_id_fkey");

            entity.HasOne(d => d.DepartmentReceiver).WithMany(p => p.InvoiceDepartmentReceivers)
                .HasForeignKey(d => d.DepartmentReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_department_receiver_id_fkey");

            entity.HasOne(d => d.WorkOrder).WithMany(p => p.Invoices)
                .HasForeignKey(d => new { d.WorkOrderId, d.WorkOrderCompilationDate })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_work_order_id_work_order_compilation_date_fkey");
        });

        modelBuilder.Entity<InvoicePau>(entity =>
        {
            entity.HasKey(e => new { e.InvoiceId, e.InvoiceCompilationDate }).HasName("invoice_pau_pkey");

            entity.ToTable("invoice_pau");

            entity.Property(e => e.InvoiceId)
                .ValueGeneratedOnAdd()
                .HasColumnName("invoice_id");
            entity.Property(e => e.InvoiceCompilationDate).HasColumnName("invoice_compilation_date");
            entity.Property(e => e.FactCount).HasColumnName("fact_count");
            entity.Property(e => e.PauId).HasColumnName("pau_id");

            entity.HasOne(d => d.Pau).WithMany(p => p.InvoicePaus)
                .HasForeignKey(d => d.PauId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_pau_pau_id_fkey");

            entity.HasOne(d => d.Invoice).WithOne(p => p.InvoicePau)
                .HasForeignKey<InvoicePau>(d => new { d.InvoiceId, d.InvoiceCompilationDate })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_pau_invoice_id_invoice_compilation_date_fkey");
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(e => e.OperationId).HasName("operation_pkey");

            entity.ToTable("operation");

            entity.Property(e => e.OperationId).HasColumnName("operation_id");
            entity.Property(e => e.OperationName)
                .HasMaxLength(100)
                .HasColumnName("operation_name");
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

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => new { e.ReservationId, e.ReservationCompilationDate }).HasName("reservation_pkey");

            entity.ToTable("reservation");

            entity.Property(e => e.ReservationId)
                .ValueGeneratedOnAdd()
                .HasColumnName("reservation_id");
            entity.Property(e => e.ReservationCompilationDate).HasColumnName("reservation_compilation_date");
            entity.Property(e => e.ReservationCount).HasColumnName("reservation_count");
            entity.Property(e => e.ReservationEndDate).HasColumnName("reservation_end_date");
            entity.Property(e => e.ReservationStartDate).HasColumnName("reservation_start_date");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("shift_pkey");

            entity.ToTable("shift");

            entity.Property(e => e.ShiftId).HasColumnName("shift_id");
            entity.Property(e => e.BrigadeNumber).HasColumnName("brigade_number");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

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
        });

        modelBuilder.Entity<WorkOrder>(entity =>
        {
            entity.HasKey(e => new { e.WorkOrderId, e.WorkOrderCompilationDate }).HasName("work_order_pkey");

            entity.ToTable("work_order");

            entity.Property(e => e.WorkOrderId)
                .ValueGeneratedOnAdd()
                .HasColumnName("work_order_id");
            entity.Property(e => e.WorkOrderCompilationDate).HasColumnName("work_order_compilation_date");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.PauId).HasColumnName("pau_id");
            entity.Property(e => e.ReservationCompilationDate).HasColumnName("reservation_compilation_date");
            entity.Property(e => e.ReservationId).HasColumnName("reservation_id");
            entity.Property(e => e.WorkOrderCloseDate).HasColumnName("work_order_close_date");
            entity.Property(e => e.WorkOrderCompleteDate).HasColumnName("work_order_complete_date");

            entity.HasOne(d => d.Employee).WithMany(p => p.WorkOrders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_order_employee_id_fkey");

            entity.HasOne(d => d.Pau).WithMany(p => p.WorkOrders)
                .HasForeignKey(d => d.PauId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_order_pau_id_fkey");

            entity.HasOne(d => d.Reservation).WithMany(p => p.WorkOrders)
                .HasForeignKey(d => new { d.ReservationId, d.ReservationCompilationDate })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_order_reservation_id_reservation_compilation_date_fkey");
        });

        modelBuilder.Entity<WorkOrderArea>(entity =>
        {
            entity.HasKey(e => new { e.WorkOrderId, e.WorkOrderCompilationDate }).HasName("work_order_area_pkey");

            entity.ToTable("work_order_area");

            entity.Property(e => e.WorkOrderId)
                .ValueGeneratedOnAdd()
                .HasColumnName("work_order_id");
            entity.Property(e => e.WorkOrderCompilationDate).HasColumnName("work_order_compilation_date");
            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.OperationEndDate).HasColumnName("operation_end_date");
            entity.Property(e => e.OperationEndTime).HasColumnName("operation_end_time");
            entity.Property(e => e.OperationId).HasColumnName("operation_id");
            entity.Property(e => e.OperationStartDate).HasColumnName("operation_start_date");
            entity.Property(e => e.OperationStartTime).HasColumnName("operation_start_time");

            entity.HasOne(d => d.Area).WithMany(p => p.WorkOrderAreas)
                .HasForeignKey(d => d.AreaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_order_area_area_id_fkey");

            entity.HasOne(d => d.Operation).WithMany(p => p.WorkOrderAreas)
                .HasForeignKey(d => d.OperationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_order_area_operation_id_fkey");

            entity.HasOne(d => d.WorkOrder).WithOne(p => p.WorkOrderArea)
                .HasForeignKey<WorkOrderArea>(d => new { d.WorkOrderId, d.WorkOrderCompilationDate })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_order_area_work_order_id_work_order_compilation_date_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
