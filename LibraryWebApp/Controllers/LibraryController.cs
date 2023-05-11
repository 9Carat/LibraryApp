using LibraryWebApp.Models;
using LibraryWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILibraryUserService _libraryUserService;
        public LibraryController(UserManager<IdentityUser> userManager, ILibraryUserService libraryUserService)
        {
            _userManager = userManager;
            _libraryUserService = libraryUserService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var books = await _libraryUserService.GetBooksAsync(user);
            var viewModel = new LibraryViewModel() { Books = books };
            return View(viewModel);
        }
        public async Task<IActionResult> BorrowBook()
        {
            return View();
        }
        public async Task<IActionResult> BorrowBookResult(string title)
        {
            var user = await _userManager.GetUserAsync (User);
            var success = await _libraryUserService.BorrowBookAsync(title, user);
            if (!success)
            {
				return BadRequest("Error! Book could not be borrowed.");
			}
            return RedirectToAction("Index");
        }
    }
}
