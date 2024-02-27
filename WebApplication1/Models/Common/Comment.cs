using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string Comment1 { get; set; } = null!;

    public int LikeCount { get; set; }

    public DateTime CreatedTime { get; set; }

    public int CraetedUid { get; set; }

    public int? ParentCommentId { get; set; }
}
