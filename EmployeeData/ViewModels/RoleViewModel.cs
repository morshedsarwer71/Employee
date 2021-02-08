using System.ComponentModel.DataAnnotations;

namespace EmployeeData.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}