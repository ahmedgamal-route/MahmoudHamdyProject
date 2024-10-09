using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IDentityEntities;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task seedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Mahmoud",
                    Email = "Mahmoud01@gmail.com",
                    UserName = "ma7moud",
                    address = new Address 
                    { 
                        FisrtName = "mahmoud",
                        LastName = "hamdi",
                        city = "octoper",
                        state = "Giza",
                        street = "3",
                        Postalcode = "12345"

                    }

                };
                await userManager.CreateAsync(user, "password12343");
            }
        }
    }
}
