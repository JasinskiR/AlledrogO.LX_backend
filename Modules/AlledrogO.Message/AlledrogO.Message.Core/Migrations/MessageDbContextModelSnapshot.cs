﻿// <auto-generated />
using System;
using AlledrogO.Message.Core.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AlledrogO.Message.Core.Migrations
{
    [DbContext(typeof(MessageDbContext))]
    partial class MessageDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("MessageSchema")
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AlledrogO.Message.Core.Entities.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdvertiserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Messages")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.HasAlternateKey("BuyerId", "AdvertiserId");

                    b.HasIndex("AdvertiserId");

                    b.ToTable("Chats", "MessageSchema");
                });

            modelBuilder.Entity("AlledrogO.Message.Core.Entities.ChatUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ChatUsers", "MessageSchema");
                });

            modelBuilder.Entity("AlledrogO.Message.Core.Entities.Chat", b =>
                {
                    b.HasOne("AlledrogO.Message.Core.Entities.ChatUser", "Advertiser")
                        .WithMany("ChatsAsAdvertiser")
                        .HasForeignKey("AdvertiserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AlledrogO.Message.Core.Entities.ChatUser", "Buyer")
                        .WithMany("ChatsAsBuyer")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Advertiser");

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("AlledrogO.Message.Core.Entities.ChatUser", b =>
                {
                    b.Navigation("ChatsAsAdvertiser");

                    b.Navigation("ChatsAsBuyer");
                });
#pragma warning restore 612, 618
        }
    }
}
