using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public partial class TvChannelContext : DbContext
{
    public TvChannelContext()
    {
    }

    public TvChannelContext(DbContextOptions<TvChannelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SUSSY-BAKA\\SQLEXPRESS;Initial Catalog=TV_Channel;Integrated Security=True;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fullName");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("position");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.GenreId).HasColumnName("genreId");
            entity.Property(e => e.Length)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("length");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.Genre).WithMany(p => p.Programs)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Programs_Genres");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
