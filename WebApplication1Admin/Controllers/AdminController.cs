using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserService _userService = new();
        private readonly PostService _postService = new();
        private readonly CommentService _commentService = new();

        [HttpGet]
        public async Task<IActionResult> UserList(AdminUserListQuery q)
        {
            if (q.Id > 0)
            {
                return RedirectToAction("UserDetailPost", new { id = q.Id });
            }

            AdminUserListModel adminUserListModel = _userService.PopulateUserListModel(q);
            return await Task.Run(() => View("~/Views/Admin/User/Index.cshtml", adminUserListModel));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUserPage(AdminUserListQuery q)
        {
            AdminUserListModel adminUserListModel = _userService.PopulateUserListModel(q);
            return await Task.Run(() => PartialView("~/Views/Admin/User/_UserList.cshtml", adminUserListModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserListTable(AdminUserListQuery q)
        {
            List<UserForAdmin> adminUserList = _userService.GetAdminUserList(q);
            return await Task.Run(() => PartialView("~/Views/Admin/User/_UserListTable.cshtml", adminUserList));
        }

        [HttpGet]
        [Route("Admin/UserList/{id}/Posts", Name = "UserDetailPost")]
        public async Task<IActionResult> UserDetailPost(AdminUserDetailQuery q)
        {
            q.DetailType = "Post";
            AdminUserDetailModel adminUserListModel = _userService.PopulateUserDetailModel(q);
            return await Task.Run(() => View("~/Views/Admin/User/UserDetailLayout.cshtml", adminUserListModel));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUserDetailPage(AdminUserDetailQuery q)
        {
            AdminUserDetailModel adminUserListModel = _userService.PopulateUserDetailModel(q);
            if (q.DetailType == "Post")
            {
                return await Task.Run(() => PartialView("~/Views/Admin/User/_UserDetailPost.cshtml", adminUserListModel));
            }
            else if (q.DetailType == "Comment")
            {
                return await Task.Run(() => PartialView("~/Views/Admin/User/_UserDetailComment.cshtml", adminUserListModel));
            }
            return await Task.Run(() => PartialView("~/Views/Admin/User/_UserDetailPost.cshtml", adminUserListModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDetailPostTable(AdminUserDetailQuery q)
        {
            List<PostDetailWithUser> adminDetailPostList = _postService.GetAdminDetailPostList(q);
            return await Task.Run(() => PartialView("~/Views/Admin/User/_UserDetailPostTable.cshtml", adminDetailPostList));
        }


        [HttpGet]
        [Route("Admin/UserList/{id}/Comments", Name = "UserDetailComment")]
        public async Task<IActionResult> UserDetailComment(AdminUserDetailQuery q)
        {
            q.DetailType = "Comment";
            AdminUserDetailModel adminUserListModel = _userService.PopulateUserDetailModel(q);
            return await Task.Run(() => View("~/Views/Admin/User/UserDetailLayout.cshtml", adminUserListModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDetailCommentTable(AdminUserDetailQuery q)
        {
            List<Comment> adminDetailCommentList = _commentService.GetAdminDetailCommentList(q);
            return await Task.Run(() => PartialView("~/Views/Admin/User/_UserDetailCommentTable.cshtml", adminDetailCommentList));
        }
    }
}
