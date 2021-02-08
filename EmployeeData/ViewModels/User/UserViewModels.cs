using System.Collections.Generic;

namespace EmployeeData.ViewModels.User
{
    public class UserViewModels
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public IList<string> Claims { get; set; }
        public IList<string> Roles { get; set; }
    }
}