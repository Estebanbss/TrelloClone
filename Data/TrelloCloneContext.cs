using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TrelloClone.Data.TrelloModels;

namespace TrelloClone.Data;

public partial class TrelloCloneContext : DbContext
{
    public TrelloCloneContext()
    {
    }

    public TrelloCloneContext(DbContextOptions<TrelloCloneContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Board> Boards { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ACCOUNT__3214EC27BBA1010C");

            entity.ToTable("ACCOUNT");

            entity.HasIndex(e => e.Username, "UQ__ACCOUNT__536C85E49876E6CB").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__ACCOUNT__A9D10534BE47ADD9").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Atype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AType");
            entity.Property(e => e.BoardId).HasColumnName("BOARD_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Photo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Pwd)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RegDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Board).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.BoardId)
                .HasConstraintName("FK__ACCOUNT__BOARD_I__6754599E");
        });

        modelBuilder.Entity<Board>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BOARD__3214EC273DF08CAD");

            entity.ToTable("BOARD");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountId).HasColumnName("ACCOUNT_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Ph)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Boards)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BOARD__ACCOUNT_I__6383C8BA");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CARD__3214EC276B3138B2");

            entity.ToTable("CARD");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Comment)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Cover)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Labels)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ListId).HasColumnName("LIST_ID");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.List).WithMany(p => p.Cards)
                .HasForeignKey(d => d.ListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CARD__LIST_ID__68487DD7");
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LIST__3214EC27258FA375");

            entity.ToTable("LIST");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BoardId).HasColumnName("BOARD_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");

            entity.HasOne(d => d.Board).WithMany(p => p.Lists)
                .HasForeignKey(d => d.BoardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LIST__BOARD_ID__66603565");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
