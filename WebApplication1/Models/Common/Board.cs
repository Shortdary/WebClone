namespace WebApplication1.Models
{
    public class Board
    {
        public int Id { get; set; }

        public string BoardName { get; set; } = null!;

        public string BoardNameEng { get; set; } = null!;

        public string Description { get; set; } = null!;

    }

    public class BoardInfoWithPostList: Board
    {
        public List<PostWithUser> PostList { get; set; } = null!;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 1;

        public int TotalRowNum { get; set; } = 1;
    }
}
