using System.ComponentModel.DataAnnotations;

namespace DewiSimulationMPA201.ViewModels.TeamMemberViewModels
{
    public class TeamMemberUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public IFormFile? ImagePath { get; set; }
        [Required]
        public int PositionId { get; set; }
    }
}
