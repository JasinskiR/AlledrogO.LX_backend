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
        
        var descriptionConverter = new ValueConverter<PostDescription, string>(
            description => description.Value,
            description => new PostDescription(description));
        builder.Property(typeof(PostDescription), "_description")
            .HasConversion(descriptionConverter)
            .HasColumnName("Description");

        builder.HasMany(typeof(PostImage), "_images");

        builder.HasMany(typeof(Tag), "_tags");
        
        var statusConverter = new ValueConverter<PostStatus, string>(
            status => status.ToString(),
            status => Enum.Parse<PostStatus>(status));
        builder.Property(typeof(PostStatus) , "_status")
            .HasConversion(statusConverter)
            .HasColumnName("Status");

        builder.HasOne(typeof(Author), "_author");

        var authorDetailsConverter = new ValueConverter<AuthorDetails, string>(
            details => details.ToString(),
            details => AuthorDetails.Create(details));
        builder.Property(typeof(AuthorDetails), "_sharedAuthorDetails")
            .HasConversion(authorDetailsConverter)
            .HasColumnName("AuthorDetails");
        
        builder.ToTable("Posts");
    }

    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.AuthorDetails)
            .HasConversion(
                details => details.ToString(),
                details => AuthorDetails.Create(details));
        
        builder.HasMany(typeof(Domain.Entities.Post), "_posts");
        
        builder.ToTable("Authors");
    }

    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Name)
            .HasConversion(
                name => name.Value,
                name => new TagName(name));
        
        builder.HasMany(typeof(Domain.Entities.Post), "_posts");
        
        builder.ToTable("Tags");
    }

    public void Configure(EntityTypeBuilder<PostImage> builder)
    {
        builder.Property<Guid>("Id");
        builder.Property(i => i.Url);
        
        builder.ToTable("PostImages");
    }
}