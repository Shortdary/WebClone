using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public partial class Post
{
    [Range(0, int.MaxValue)]
    public int Id { get; set; }

    public int BoardId { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Subject { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Detail { get; set; } = null!;

    public int CommentCount { get; set; }

    public int ViewCount { get; set; }

    public int LikeCount { get; set; }

    public DateTime CreatedTime { get; set; }

    public int CreatedUid { get; set; }
}

public partial class PostWithUser : Post
{
    public string Nickname { get; set; } = null!;

    public string BoardName { get; set; } = null!;

    public string BoardNameEng { get; set; } = null!;

    public List<Comment> Comments { get; set; } = null!;
}

public partial class PostInsert
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "게시판을 선택해주세요")]
    public int BoardId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "제목을 입력해주세요")]
    public string Subject { get; set; } = null!;

    [Required(AllowEmptyStrings = false, ErrorMessage = "내용을 입력해주세요")]
    public string Detail { get; set; } = null!;

    [Required]
    public int CreatedUid { get; set; }

    public int CommentCount { get; set; } = 0;

    public int ViewCount { get; set; } = 0;

    public int LikeCount { get; set; } = 0;

    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
}

public partial class PostDelete
{
    public int PostId { get; set; }
}

public partial class PostEdit
{
    public int PostId { get; set; }
}