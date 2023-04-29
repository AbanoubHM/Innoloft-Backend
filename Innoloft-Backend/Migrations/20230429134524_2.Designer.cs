﻿// <auto-generated />
using System;
using Innoloft_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Innoloft_Backend.Migrations
{
    [DbContext(typeof(EventsContext))]
    [Migration("20230429134524_2")]
    partial class _2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("EventUser", b =>
                {
                    b.Property<int>("ParticipatingEventsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ParticipatingUsersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ParticipatingEventsId", "ParticipatingUsersId");

                    b.HasIndex("ParticipatingUsersId");

                    b.ToTable("EventUser");
                });

            modelBuilder.Entity("EventUser1", b =>
                {
                    b.Property<int>("InvitedEventsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InvitedUsersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("InvitedEventsId", "InvitedUsersId");

                    b.HasIndex("InvitedUsersId");

                    b.ToTable("EventUser1");
                });

            modelBuilder.Entity("Innoloft_Backend.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Innoloft_Backend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EventUser", b =>
                {
                    b.HasOne("Innoloft_Backend.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("ParticipatingEventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Innoloft_Backend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("ParticipatingUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventUser1", b =>
                {
                    b.HasOne("Innoloft_Backend.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("InvitedEventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Innoloft_Backend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("InvitedUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Innoloft_Backend.Models.Event", b =>
                {
                    b.HasOne("Innoloft_Backend.Models.User", "User")
                        .WithMany("Events")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Innoloft_Backend.Models.Address", "Address", b1 =>
                        {
                            b1.Property<int>("EventId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Suite")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Zipcode")
                                .HasColumnType("TEXT");

                            b1.HasKey("EventId");

                            b1.ToTable("Events");

                            b1.WithOwner()
                                .HasForeignKey("EventId");

                            b1.OwnsOne("Innoloft_Backend.Models.GeoLocation", "Geo", b2 =>
                                {
                                    b2.Property<int>("AddressEventId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<string>("Lat")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.Property<string>("Lng")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.HasKey("AddressEventId");

                                    b2.ToTable("Events");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressEventId");
                                });

                            b1.Navigation("Geo");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Innoloft_Backend.Models.User", b =>
                {
                    b.OwnsOne("Innoloft_Backend.Models.Company", "Company", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Bs")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("CatchPhrase")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Innoloft_Backend.Models.Address", "Address", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Suite")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Zipcode")
                                .HasColumnType("TEXT");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.OwnsOne("Innoloft_Backend.Models.GeoLocation", "Geo", b2 =>
                                {
                                    b2.Property<int>("AddressUserId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<string>("Lat")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.Property<string>("Lng")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.HasKey("AddressUserId");

                                    b2.ToTable("Users");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressUserId");
                                });

                            b1.Navigation("Geo");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Company")
                        .IsRequired();
                });

            modelBuilder.Entity("Innoloft_Backend.Models.User", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
