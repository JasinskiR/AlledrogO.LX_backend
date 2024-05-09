using AlledrogO.Post.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlledrogO.Post.Infrastructure.EF.Config;

public class WriteConfiguration:
    IEntityTypeConfiguration<Domain.Entities.Post>,
    IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Post> builder)
    {
        throw new NotImplementedException();
    }

    public void Configure(EntityTypeBuilder<Author> builder)
    {
        throw new NotImplementedException();
    }
}