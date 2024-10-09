using Microsoft.AspNetCore.Identity;

namespace Store.Data.Entities.IDentityEntities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address address { get; set; }
    }
}
