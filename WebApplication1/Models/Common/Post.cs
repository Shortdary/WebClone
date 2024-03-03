using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public partial class Post
{
    [Required]
    [Range(0, int.MaxValue)]
    public int Id { get; set; }

    public int BoardId { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Subject { get; set; } = null!;

    public string Detail { get; set; } = null!;

    public int CommentCount { get; set; }

    public int ViewCount { get; set; }

    public int LikeCount { get; set; }

    public DateTime CreatedTime { get; set; }

    public int CreatedUid { get; set; }

}

public partial class PostWithUser: Post
{
    public string Nickname { get; set; } = null!;

    public string BoardName { get; set; } = null!;
}
