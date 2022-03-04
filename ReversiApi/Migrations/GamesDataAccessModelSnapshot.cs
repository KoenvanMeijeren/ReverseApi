﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReversiApi.DataAccess;

#nullable disable

namespace ReversiApi.Migrations;

[ExcludeFromCodeCoverage]
[DbContext(typeof(GamesDataAccess))]
partial class GamesDataAccessModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "6.0.2")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

        modelBuilder.Entity("ReversiApi.Model.Game.GameEntity", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

            b.Property<string>("Board")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.Property<int?>("CurrentPlayerId")
                .HasColumnType("int");

            b.Property<string>("Description")
                .HasColumnType("nvarchar(max)");

            b.Property<int?>("PlayerOneId")
                .HasColumnType("int");

            b.Property<int?>("PlayerTwoId")
                .HasColumnType("int");

            b.Property<int>("Status")
                .HasColumnType("int");

            b.Property<string>("Token")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.HasKey("Id");

            b.HasIndex("CurrentPlayerId");

            b.HasIndex("PlayerOneId");

            b.HasIndex("PlayerTwoId");

            b.ToTable("Games");
        });

        modelBuilder.Entity("ReversiApi.Model.Player.PlayerEntity", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

            b.Property<int>("Color")
                .HasColumnType("int");

            b.Property<string>("Token")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.HasKey("Id");

            b.ToTable("Players");
        });

        modelBuilder.Entity("ReversiApi.Model.Game.GameEntity", b =>
        {
            b.HasOne("ReversiApi.Model.Player.PlayerEntity", "CurrentPlayer")
                .WithMany()
                .HasForeignKey("CurrentPlayerId");

            b.HasOne("ReversiApi.Model.Player.PlayerEntity", "PlayerOne")
                .WithMany("GamesPlayerOne")
                .HasForeignKey("PlayerOneId");

            b.HasOne("ReversiApi.Model.Player.PlayerEntity", "PlayerTwo")
                .WithMany("GamesPlayerTwo")
                .HasForeignKey("PlayerTwoId");

            b.Navigation("CurrentPlayer");

            b.Navigation("PlayerOne");

            b.Navigation("PlayerTwo");
        });

        modelBuilder.Entity("ReversiApi.Model.Player.PlayerEntity", b =>
        {
            b.Navigation("GamesPlayerOne");

            b.Navigation("GamesPlayerTwo");
        });
#pragma warning restore 612, 618
    }
}