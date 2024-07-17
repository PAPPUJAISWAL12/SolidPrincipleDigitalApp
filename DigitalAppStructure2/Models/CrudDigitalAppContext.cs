using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DigitalAppStructure2.Models;

public partial class CrudDigitalAppContext : DbContext
{
    public CrudDigitalAppContext()
    {
    }

    public CrudDigitalAppContext(DbContextOptions<CrudDigitalAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserList> UserLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=Conn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserList>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserList__1788CC4C77813668");

            entity.ToTable("UserList");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.EmailAddress).HasMaxLength(50);
            entity.Property(e => e.UserAddress).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(40);
            entity.Property(e => e.UserRole).HasMaxLength(50);
            entity.Property(e => e.UserStatus).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
