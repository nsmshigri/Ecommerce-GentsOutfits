using Microsoft.AspNetCore.Identity;

namespace GentsOutfits_Authentication_.Models
{
    public class MyUsers: IdentityUser
    {
        public string FullName { get; set; }
    }
}
