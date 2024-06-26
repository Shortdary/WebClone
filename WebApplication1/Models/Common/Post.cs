﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public partial class Post
{
    [Range(0, int.MaxValue)]
    public int Id { get; set; }

    public int BoardId { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string? Subject { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string? Detail { get; set; }

    public int CommentCount { get; set; }

    public int ViewCount { get; set; }

    public int LikeCount { get; set; }

    public DateTime CreatedTime { get; set; }

    public int CreatedUid { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime UpdatedTime { get; set; }

    public int UpdatedUid { get; set; }
}

public partial class PostDetailWithUser : Post
{
    public string? Nickname { get; set; }

    public string? BoardName { get; set; }

    public string? BoardNameEng { get; set; }

    public CommentPartialViewModel CommentPartialViewModel { get; set; } = null!;

    public BoardControllerCommonParameter? Parameters { get; set; }
}

public partial class PostInsert
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "게시판을 선택해주세요")]
    public int BoardId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "제목을 입력해주세요")]
    public string? Subject { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "내용을 입력해주세요")]
    public string? Detail { get; set; }

    [Required]
    public int CreatedUid { get; set; }
}

public partial class PostDelete
{
    [Required]
    [Range(1, int.MaxValue)]
    public int PostId { get; set; }
}

public partial class PostEdit
{
    [Required]
    [Range(1, int.MaxValue)]
    public int PostId { get; set; }

    public string? BoardNameEng { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "제목을 입력해주세요")]
    public string? Subject { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "내용을 입력해주세요")]
    public string? Detail { get; set; }

    [Required]
    public int UpdatedUid { get; set; }
}