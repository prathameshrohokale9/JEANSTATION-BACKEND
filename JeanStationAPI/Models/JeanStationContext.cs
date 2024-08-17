using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JeanStationAPI.Models;

public partial class JeanStationContext : DbContext
{
    public JeanStationContext()
    {
    }

    public JeanStationContext(DbContextOptions<JeanStationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=JeanStation;integrated security=true;trustservercertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => new { e.CustId, e.ItemCode }).HasName("PK__Cart__9D07223B6506D53D");

            entity.ToTable("Cart");

            entity.Property(e => e.CustId).HasColumnName("custId");
            entity.Property(e => e.ItemCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("itemCode");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.Value)
                .HasComputedColumnSql("([qty]*[price])", true)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustId).HasName("PK__Customer__9725F2C6A1FC69A3");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.UserName, "UQ__Customer__66DCF95C595F3A2D").IsUnique();

            entity.Property(e => e.CustId).HasColumnName("custId");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.CustName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("custName");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("phoneNo");
            entity.Property(e => e.Pwd)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("pwd");
            entity.Property(e => e.UserName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AFB3EC0D384C8BBE");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.EmpUserName, "UQ__Employee__C7790080232A834E").IsUnique();

            entity.Property(e => e.EmpId).HasColumnName("empId");
            entity.Property(e => e.EmpEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("empEmail");
            entity.Property(e => e.EmpName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("empName");
            entity.Property(e => e.EmpPhoneNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("empPhoneNo");
            entity.Property(e => e.EmpPwd)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("empPwd");
            entity.Property(e => e.EmpUserName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("empUserName");
            entity.Property(e => e.StoreId).HasColumnName("storeId");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemCode).HasName("PK__Item__A22D0FD19AB3A1B9");

            entity.ToTable("Item");

            entity.Property(e => e.ItemCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("itemCode");
            entity.Property(e => e.ItemImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("item_image");
            entity.Property(e => e.ItemName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("itemName");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.StoreId).HasColumnName("storeId");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ItemCode }).HasName("PK__orders__022BE3A015B3B57C");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.ItemCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("itemCode");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CustId).HasColumnName("custId");
            entity.Property(e => e.ItemValue)
                .HasComputedColumnSql("([qty]*[price])", true)
                .HasColumnName("itemValue");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("orderDate");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasDefaultValue("order placed")
                .HasColumnName("orderStatus");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Qty).HasColumnName("qty");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Store__1EA71613B00203F8");

            entity.ToTable("Store");

            entity.Property(e => e.StoreId).HasColumnName("storeId");
            entity.Property(e => e.Location)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("phoneNo");
            entity.Property(e => e.StoreName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("storeName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
