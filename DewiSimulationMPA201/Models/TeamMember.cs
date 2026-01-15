using DewiSimulationMPA201.Models.Common;

namespace DewiSimulationMPA201.Models;
public class TeamMember : BaseEntity
{
    public string FullName { get; set; }
    public string ImagePath { get; set; }
    public int PositionId { get; set; }
    public Position Position { get; set; }
}
