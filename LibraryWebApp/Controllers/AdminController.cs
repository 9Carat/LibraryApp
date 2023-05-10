using LibraryWebApp.Models;
using LibraryWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILibraryAdminService _libraryAdminService;
        public AdminController(UserManager<IdentityUser> userManager, ILibraryAdminService libraryAdminService)
        {
            _userManager = userManager;
            _libraryAdminService = libraryAdminService;
        }
        public async Task<IActionResult> Index()
        {
            var users = (await _userManager.GetUsersInRoleAsync("User"))
                .ToArray();
            var books = (await _libraryAdminService.GetBooksAsync())
                .ToArray();
            var viewModel = new AdminViewModel() { Books = books, Users = users };
            return View(viewModel);
        }
        public async Task<IActionResult> ViewLoanedBooks()
        {
            var books = await _libraryAdminService.GetBooksAsync();
            var viewModel = new LibraryViewModel() { Books = books };
            return View(viewModel);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsReturned(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }
            var success = await _libraryAdminService.MarkAsReturnedAsync(id);
            if (!success)
            {
                return BadRequest("Error! Book could not be marked as returned.");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SearchUser()
        {
            return View();
        }
        public async Task<IActionResult> SearchUserResult(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            IdentityUser[] users = new IdentityUser[1] { user};
            var viewModel = new AdminViewModel() { Users = users};
            return View(viewModel);
        }
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
			await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var viewModel = new UserViewModel() 
            { 
                Id = id,
                UserName = user.UserName,
                Email = user.Email
            };
            return View(viewModel);
        }
        public async Task<IActionResult> EditUserResult(string id, string username, string email)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.Email = email;
            user.UserName = username;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }

    }
}
