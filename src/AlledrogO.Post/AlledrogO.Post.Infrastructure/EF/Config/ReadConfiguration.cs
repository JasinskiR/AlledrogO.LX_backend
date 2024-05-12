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
        builder.ToTable("Posts");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Version)
            .IsRowVersion();
        
        builder.HasMany(p => p.Images)
            .WithOne(p => p.Post)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(p => p.Tags)
            .WithMany(t => t.Posts);
        
        builder.Property(p => p.Status)
            .HasConversion(
                status => status.ToString(),
                status => Enum.Parse<PostStatus>(status));
        
        builder.HasOne(p => p.Author)
            .WithMany(a => a.Posts);

        builder.Property(p => p.AuthorDetails)
            .HasConversion(
                details => details.ToString(),
                details => AuthorDetailsReadDbModel.Create(details));
    }

    public void Configure(EntityTypeBuilder<AuthorDbModel> builder)
    {
        builder.ToTable("Authors");
        
        builder.HasKey(a => a.Id);
        
        builder.HasMany(a => a.Posts)
            .WithOne(p => p.Author);
        
        builder.Property(a => a.Details)
            .HasConversion(
                details => details.ToString(),
                details => AuthorDetailsReadDbModel.Create(details));
    }

    public void Configure(EntityTypeBuilder<PostImageDbModel> builder)
    {
        builder.ToTable("PostImages");
        
        builder.HasKey(i => i.Id);
        
        builder.HasOne(i => i.Post)
            .WithMany(p => p.Images);
    }

    public void Configure(EntityTypeBuilder<TagDbModel> builder)
    {
        builder.ToTable("Tags");
        
        builder.HasKey(t => t.Id);
        
        builder.HasMany(t => t.Posts)
            .WithMany(p => p.Tags);
    }
}
