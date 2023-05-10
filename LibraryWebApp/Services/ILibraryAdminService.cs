using LibraryWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryWebApp.Services
{
    public interface ILibraryAdminService
    {
        Task<Book[]> GetBooksAsync();
        Task<bool> MarkAsReturnedAsync(Guid id);
    }
}
