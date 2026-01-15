using DewiSimulationMPA201.Models.Common;

namespace DewiSimulationMPA201.Models;

public class Position : BaseEntity
{
    public string Name { get; set; }
    public ICollection<TeamMember>? TeamMembers { get; set; }
}
