using LibraryWebApp.Data;
using LibraryWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Services
{
    public class LibraryAdminService : ILibraryAdminService
    {
        private readonly ApplicationDbContext _context;
        public LibraryAdminService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book[]> GetBooksAsync()
        {
            var books = await _context.Books
                .Where(b => b.IsReturned == false)
                .ToArrayAsync();
            return books;
        }

        public async Task<bool> MarkAsReturnedAsync(Guid id)
        {
            var book = await _context.Books
                .Where (b => b.BookId == id)
                .FirstOrDefaultAsync();
            if (book == null)
            {
				return false;
			}
            book.IsReturned = true;
            var result = await _context.SaveChangesAsync();
            return result == 1;
        }
    }
}
