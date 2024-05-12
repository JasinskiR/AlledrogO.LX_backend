using AlledrogO.Post.Domain.Consts;
using AlledrogO.Post.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlledrogO.Post.Infrastructure.EF.Config;

internal sealed class ReadConfiguration : 
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
        
        builder.HasMany(p => p.Tags)
            .WithMany(t => t.Posts)
            .UsingEntity(j => j.ToTable("PostTag"));
        
        
        builder.ToTable("Posts");
    }

    public void Configure(EntityTypeBuilder<AuthorDbModel> builder)
    {
        builder.Property(p => p.Version)
            .IsRowVersion();

        builder.ToTable("Authors");
    }

    public void Configure(EntityTypeBuilder<PostImageDbModel> builder)
    {
        builder.ToTable("PostImages");
    }

    public void Configure(EntityTypeBuilder<TagDbModel> builder)
    {
        builder.Property(p => p.Version)
            .IsRowVersion();
        builder.ToTable("Tags");
    }
}