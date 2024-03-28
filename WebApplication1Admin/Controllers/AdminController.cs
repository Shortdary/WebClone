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
            AdminUserListModel adminUserListModel = _userService.PopulateUserListModel(q);
            return await Task.Run(() => View("UserList", adminUserListModel));
        }

        [HttpGet]
        [Route("Admin/UserList/{id}/Posts")]
        public async Task<IActionResult> UserDetailPost(int id)
        {
            return await Task.Run(() => View("UserDetail"));
        }

    }
}
