using System.ComponentModel.DataAnnotations;

namespace EmployeeData.ViewModels
{
    public class RegisterModel
    {
        [EmailAddress]
        //[Remote(action:"IsEmailUse, controller: "accontcontroller")] this tag for remote validation
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string City { get; set; }
        public string Gender { get; set; }
    }
}