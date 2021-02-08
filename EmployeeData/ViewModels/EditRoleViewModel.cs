using System.Collections.Generic;

namespace EmployeeData.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }
        public string EditRoleName { get; set; }
        public List<string> Users { get; set; }
    }
}