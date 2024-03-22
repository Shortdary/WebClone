using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public string Comment1 { get; set; } = null!;

    public int LikeCount { get; set; }

    public DateTime CreatedTime { get; set; }

    public int CreatedUid { get; set; }

    public int? ParentCommentId { get; set; }

    public string Nickname { get; set; } = null!;
}

public partial class CommentAdd
{
    [Required]
    [Range(0, int.MaxValue)]
    public int PostId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "내용을 입력해주세요")]
    public string Comment1 { get; set; } = null!;

    [Required]
    [Range(0, int.MaxValue)]
    public int CreatedUid { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int? ParentCommentId { get; set; }
}