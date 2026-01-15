using System.ComponentModel.DataAnnotations;

namespace DewiSimulationMPA201.ViewModels.UserViewModels
{
    public class LoginVM
    {
        [Required, MinLength(3)]
        public string Email { get; set; }
        [Required, MinLength(3), DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}
