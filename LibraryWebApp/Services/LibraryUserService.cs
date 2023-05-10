using LibraryWebApp.Data;
using LibraryWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Services
{
    public class LibraryUserService : ILibraryUserService
    {
        private readonly ApplicationDbContext _context;
        public LibraryUserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> BorrowBookAsync(Book book, IdentityUser user)
        {
            book.BookId = Guid.NewGuid();
            book.UserName = user.Id;
            book.DueDate = DateTime.Now.AddDays(14);
            book.IsReturned = false;
            _context.Add(book);
            var result = await _context.SaveChangesAsync();
            return result == 1;
        }

        public async Task<Book[]> GetBooksAsync(IdentityUser user)
        {
            var books = await _context.Books
                .Where(b => b.IsReturned == false && b.UserName == user.Id)
                .ToArrayAsync();
            return books;
        }
    }
}
