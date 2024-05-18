using System.Net.Mime;
using AlledrogO.Post.Domain.Consts;
using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AlledrogO.Post.Infrastructure.EF.Config;

public class WriteConfiguration:
    IEntityTypeConfiguration<Domain.Entities.Post>,
    IEntityTypeConfiguration<Author>,
    IEntityTypeConfiguration<Tag>,
    IEntityTypeConfiguration<PostImage>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Post> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Version)
            .IsRowVersion();
        
        builder.Property(p => p.Title)
            .HasConversion(
                title => title.Value,
                title => new PostTitle(title))
            .HasColumnName("Title");
        

        builder.Property(p => p.Description)
            .HasConversion(
                description => description.Value,
                description => new PostDescription(description))
            .HasColumnName("Description");
        
        builder.Property(p => p.Status)
            .HasConversion(
                status => status.ToString(),
                status => Enum.Parse<PostStatus>(status))
            .HasColumnName("Status");
        
        builder.Property(p => p.SharedAuthorDetails)
            .HasConversion(
                details => details.ToString(),
                details => AuthorDetails.Create(details))
            .HasColumnName("SharedAuthorDetails");

        builder.HasMany(p => p.Images);

        builder.HasMany(p => p.Tags)
            .WithMany(t => t.Posts);

        builder.HasOne(p => p.Author)
            .WithMany(a => a.Posts);
        
        builder.ToTable("Posts");
    }

    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);
        
        builder.Property(p => p.Version)
            .IsRowVersion();
        
        builder.Property(a => a.AuthorDetails)
            .HasConversion(
                details => details.ToString(),
                details => AuthorDetails.Create(details))
            .HasColumnName("AuthorDetails");

        builder.HasMany(a => a.Posts)
            .WithOne(p => p.Author);
        
        builder.ToTable("Authors");
    }

    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.Property(p => p.Version)
            .IsRowVersion();
        
        builder.Property(t => t.Name)
            .HasConversion(
                name => name.Value,
                name => new TagName(name));

        builder.Property(t => t.PostCount);
        
        builder.HasMany(t => t.Posts)
            .WithMany(p => p.Tags);
        
        builder.ToTable("Tags");
    }

    public void Configure(EntityTypeBuilder<PostImage> builder)
    {
        builder.Property(i => i.Id)
            .ValueGeneratedNever();
        
        builder.Property(i => i.Url);
        builder.Property(i => i.IsMain);
        builder.ToTable("PostImages");
    }
}