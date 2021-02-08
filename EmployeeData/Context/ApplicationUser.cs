using Microsoft.AspNetCore.Identity;

namespace EmployeeData.Context
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
        public string Gender { get; set; }
    }
}