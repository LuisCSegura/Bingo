﻿// <auto-generated />
using System;
using Bingo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Bingo.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20201028203742_AddNameAndTimeToGames")]
    partial class AddNameAndTimeToGames
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Bingo.Models.Game", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int[]>("GettedNumbers")
                        .HasColumnName("getted_numbers")
                        .HasColumnType("integer[]");

                    b.Property<string>("Link")
                        .HasColumnName("link")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<int>("PlayersNumber")
                        .HasColumnName("players_number")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartTime")
                        .HasColumnName("start_time")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id")
                        .HasName("pk_games");

                    b.ToTable("games");
                });

            modelBuilder.Entity("Bingo.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users");
                });
#pragma warning restore 612, 618
        }
    }
}
