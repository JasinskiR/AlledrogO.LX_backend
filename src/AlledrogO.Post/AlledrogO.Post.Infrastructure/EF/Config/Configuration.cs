using AlledrogO.Post.Domain.Consts;
using AlledrogO.Post.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlledrogO.Post.Infrastructure.EF.Config;

internal sealed class Configuration : 
    IEntityTypeConfiguration<PostDbModel>,
    IEntityTypeConfiguration<AuthorDbModel>,
    IEntityTypeConfiguration<PostImageDbModel>,
    IEntityTypeConfiguration<TagDbModel>
{
    public void Configure(EntityTypeBuilder<PostDbModel> builder)
    {
        builder.Property(p => p.Version)
            .IsRowVersion();
        
        builder.HasMany(p => p.Images)
            .WithOne(i => i.Post)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(p => p.Status)
            .HasConversion(
            status => status.ToString(),
            status => Enum.Parse<PostStatus>(status));
    }

    public void Configure(EntityTypeBuilder<AuthorDbModel> builder)
    {
        builder.Property(p => p.Version)
            .IsRowVersion();
    }

    public void Configure(EntityTypeBuilder<PostImageDbModel> builder)
    {
        
    }

    public void Configure(EntityTypeBuilder<TagDbModel> builder)
    {
        builder.Property(p => p.Version)
            .IsRowVersion();
    }
}