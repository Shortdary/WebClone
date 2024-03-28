using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class CopycatContext : DbContext
{
    private readonly IConfiguration? _config;

    public CopycatContext()
    {
    }

    public CopycatContext(IConfiguration config)
    {
        _config = config;
    }

    public CopycatContext(DbContextOptions<CopycatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public virtual DbSet<Board> Boards { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-AEPN1UJ;Database=Copycat;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83F5955E3F4");

            entity.ToTable("ApplicationUser");

            entity.HasIndex(e => e.Nickname, "UQ__User__5CF1C59B4F66B7DB").IsUnique();

            entity.HasIndex(e => e.LoginId, "UQ__User__C2C971DAB52FC2FB").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LoginId)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("login_id");
            entity.Property(e => e.Nickname)
                .HasMaxLength(20)
                .HasColumnName("nickname");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("password");
        });

        modelBuilder.Entity<Board>(entity =>
        {
            entity.ToTable("Board");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BoardName)
                .HasMaxLength(20)
                .HasColumnName("board_name");
            entity.Property(e => e.BoardNameEng)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("board_name_eng");
            entity.Property(e => e.Description)
                .HasMaxLength(30)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3213E83FC097FD9B");

            entity.ToTable("Comment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment1)
                .HasMaxLength(50)
                .HasColumnName("comment");
            entity.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasColumnName("created_time");
            entity.Property(e => e.CreatedUid).HasColumnName("created_uid");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.LikeCount).HasColumnName("like_count");
            entity.Property(e => e.ParentCommentId).HasColumnName("parent_comment_id");
            entity.Property(e => e.PostId).HasColumnName("post_id");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Post__3213E83F775B3FC3");

            entity.ToTable("Post");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BoardId).HasColumnName("board_id");
            entity.Property(e => e.CommentCount).HasColumnName("comment_count");
            entity.Property(e => e.CreatedTime).HasColumnName("created_time");
            entity.Property(e => e.CreatedUid).HasColumnName("created_uid");
            entity.Property(e => e.Detail).HasColumnName("detail");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_deleted");
            entity.Property(e => e.LikeCount).HasColumnName("like_count");
            entity.Property(e => e.Subject)
                .HasMaxLength(30)
                .HasColumnName("subject");
            entity.Property(e => e.UpdatedTime).HasColumnName("updated_time");
            entity.Property(e => e.UpdatedUid).HasColumnName("updated_uid");
            entity.Property(e => e.ViewCount).HasColumnName("view_count");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
