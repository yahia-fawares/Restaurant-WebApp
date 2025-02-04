using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyRestaurant.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

   

    public virtual DbSet<UserCategory> UserCategories { get; set; }

    public virtual DbSet<UserCustomer> UserCustomers { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserProduct> UserProducts { get; set; }

    public virtual DbSet<UserProductCustomer> UserProductCustomers { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("DATA SOURCE=192.168.1.43:1521;USER ID=c##Yahia;PASSWORD=Test321;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##YAHIA")
            .UseCollation("USING_NLS_COMP");

       

        modelBuilder.Entity<UserCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008440");

            entity.ToTable("USER_CATEGORY");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
        });

        modelBuilder.Entity<UserCustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008442");

            entity.ToTable("USER_CUSTOMER");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.Fname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FNAME");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LNAME");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008447");

            entity.ToTable("USER_LOGIN");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("USER_NAME");

            entity.HasOne(d => d.Customer).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CUSTOMER_ID");

            entity.HasOne(d => d.Role).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ROLE_ID");
        });

        modelBuilder.Entity<UserProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008444");

            entity.ToTable("USER_PRODUCT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.CategoryId)
                .HasColumnType("NUMBER")
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Price)
                .HasColumnType("FLOAT")
                .HasColumnName("PRICE");
            entity.Property(e => e.Sale)
                .HasColumnType("NUMBER")
                .HasColumnName("SALE");

            entity.HasOne(d => d.Category).WithMany(p => p.UserProducts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CATEGORY_ID");
        });

        modelBuilder.Entity<UserProductCustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008454");

            entity.ToTable("USER_PRODUCT_CUSTOMER");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.DateFrom)
                .HasColumnType("DATE")
                .HasColumnName("DATE_FROM");
            entity.Property(e => e.DateTo)
                .HasColumnType("DATE")
                .HasColumnName("DATE_TO");
            entity.Property(e => e.ProductId)
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCT_ID");
            entity.Property(e => e.Quantity)
                .HasColumnType("NUMBER")
                .HasColumnName("QUANTITY");

            entity.HasOne(d => d.Customer).WithMany(p => p.UserProductCustomers)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CUSTOMER_PRODUCT_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.UserProductCustomers)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PRODUCT_CUSTOMER_ID");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008438");

            entity.ToTable("USER_ROLE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ROLE_NAME");
        });

        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
