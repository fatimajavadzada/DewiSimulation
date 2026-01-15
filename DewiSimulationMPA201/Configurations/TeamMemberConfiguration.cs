using DewiSimulationMPA201.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DewiSimulationMPA201.Configurations;
public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> builder)
    {
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(256);
        builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(1024);

        builder.HasOne(x => x.Position).WithMany(x => x.TeamMembers).HasForeignKey(x => x.PositionId).HasPrincipalKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
    }
}
