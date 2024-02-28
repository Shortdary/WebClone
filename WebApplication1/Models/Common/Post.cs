using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Post
{
    public int Id { get; set; }

    public int BoardId { get; set; }

    public string Subject { get; set; } = null!;

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