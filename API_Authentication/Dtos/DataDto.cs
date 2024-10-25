using API_Authentication.Entity;

namespace API_Authentication.Dtos
{
    public class DataDto
    {
        public static List<User> Users = new List<User>()
        {
            new User { Username = "jason_admin", Email = "jason.admin@email.com", Password = "MyPass_w0rd", GivenName = "Jason", Surname = "Bryant", Role = "Admin" },
            new User { Username = "elyse_seller", Email = "elyse.seller@email.com", Password = "MyPass_w0rd", GivenName = "Elyse", Surname = "Lambert", Role = "Customer" },
        };
    }
}
