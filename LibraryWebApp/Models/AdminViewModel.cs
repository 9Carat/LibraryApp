using Microsoft.AspNetCore.Identity;

namespace LibraryWebApp.Models
{
    public class AdminViewModel
    {
        public Book[]? Books { get; set; }
        public IdentityUser[]? Users { get; set; }
        public IdentityUser? User { get; set; }
    }
}
