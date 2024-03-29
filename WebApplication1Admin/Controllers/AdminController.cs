using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1Admin.Controllers
{
    public class AdminController: Controller
    {
        private readonly UserService _userService = new();

        [HttpGet]
        public async Task<IActionResult> UserList(AdminUserListQuery q)
        {
            if (q.Id > 0)
            {
                return RedirectToAction("UserDetailPost", new { id = q.Id });
            }

            AdminUserListModel adminUserListModel = _userService.PopulateUserListModel(q);
            return await Task.Run(() => View("~/Views/Admin/User/UserList.cshtml", adminUserListModel));
        }

        [HttpGet]
        [Route("Admin/UserList/{id}/Posts",Name = "UserDetailPost")]
        public async Task<IActionResult> UserDetailPost(AdminUserDetailQuery q)
        {
            q.DetailType = "Post";
            AdminUserDetailModel adminUserListModel = _userService.PopulateUserDetailModel(q);
            return await Task.Run(() => View("~/Views/Admin/User/UserDetailLayout.cshtml", adminUserListModel));
        }

        [HttpGet]
        [Route("Admin/UserList/{id}/Comments", Name = "UserDetailComment")]
        public async Task<IActionResult> UserDetailComment(AdminUserDetailQuery q)
        {
            q.DetailType = "Comment";
            AdminUserDetailModel adminUserListModel = _userService.PopulateUserDetailModel(q);
            return await Task.Run(() => View("~/Views/Admin/User/UserDetailLayout.cshtml", adminUserListModel));
        }
    }
}
