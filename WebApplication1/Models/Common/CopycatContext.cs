using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class CopycatContext : DbContext
{
    public CopycatContext()
    {
    }

    public CopycatContext(DbContextOptions<CopycatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-AEPN1UJ;Database=Copycat;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Comment");

            entity.Property(e => e.Comment1)
                .HasMaxLength(50)
                .HasColumnName("comment");
            entity.Property(e => e.CraetedUid).HasColumnName("craeted_uid");
            entity.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasColumnName("created_time");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LikeCount).HasColumnName("like_count");
            entity.Property(e => e.ParentCommentId).HasColumnName("parent_comment_id");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Post");

            entity.Property(e => e.BoardId).HasColumnName("board_id");
            entity.Property(e => e.CommentCount).HasColumnName("comment_count");
            entity.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedUid).HasColumnName("created_uid");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LikeCount).HasColumnName("like_count");
            entity.Property(e => e.Subject)
                .HasMaxLength(30)
                .HasColumnName("subject");
            entity.Property(e => e.ViewCount).HasColumnName("view_count");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
