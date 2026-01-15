using System.ComponentModel.DataAnnotations;

namespace DewiSimulationMPA201.ViewModels.TeamMemberViewModels;

public class TeamMemberCreateVM
{
    [Required]
    public string FullName { get; set; }
    [Required]
    public IFormFile ImagePath { get; set; }
    [Required]
    public int PositionId { get; set; }
}
