using System.ComponentModel.DataAnnotations;

namespace DewiSimulationMPA201.ViewModels.UserViewModels
{
    public class RegisterVM
    {
        [Required, MinLength(3)]
        public string FullName { get; set; }
        [Required, MinLength(3)]
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
