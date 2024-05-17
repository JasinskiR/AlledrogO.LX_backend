using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlledrogO.User.Core.EF.Config;

internal sealed class UserConfiguration : IEntityTypeConfiguration<Entities.User>
{
    public void Configure(EntityTypeBuilder<Entities.User> builder)
    {
        
    }
}