using LibraryWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryWebApp.Services
{
    public interface ILibraryUserService
    {
        Task<Book[]> GetBooksAsync(IdentityUser user);
        Task<bool> BorrowBookAsync(Book book,IdentityUser user);
    }
}
