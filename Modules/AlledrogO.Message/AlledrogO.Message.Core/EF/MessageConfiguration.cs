using AlledrogO.Message.Core.Entities;
using Jil;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace AlledrogO.Message.Core.EF;

internal sealed class MessageConfiguration : 
    IEntityTypeConfiguration<Chat>,
    IEntityTypeConfiguration<ChatUser>
{

    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasAlternateKey(c => new { c.BuyerId, c.AdvertiserId });
        
        builder.HasOne(c => c.Buyer)
            .WithMany(u => u.ChatsAsBuyer)
            .HasForeignKey(c => c.BuyerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Advertiser)
            .WithMany(u => u.ChatsAsAdvertiser)
            .HasForeignKey(c => c.AdvertiserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(c => c.Messages)
            .HasConversion(
                v => JsonConvert.SerializeObject(v, 
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<LinkedList<Entities.Message>>(v, 
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }))
            .HasColumnType("jsonb");
    }

    public void Configure(EntityTypeBuilder<ChatUser> builder)
    {
        builder.HasKey(u => u.Id);
    }
}