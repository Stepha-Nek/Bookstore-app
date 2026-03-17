using Microsoft.AspNetCore.Identity;

namespace FirstAPI.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
