﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetCatalogue.Models;

#nullable disable

namespace ProjetCatalogue.Migrations.GestionFavoriMigrations
{
    [DbContext(typeof(GestionFavori))]
    [Migration("20240501140037_CreerProjetCatalogue")]
    partial class CreerProjetCatalogue
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProjetCatalogue.Models.Favori", b =>
                {
                    b.Property<int>("IdFavori")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdFavori"));

                    b.Property<int>("IdVideo")
                        .HasColumnType("int");

                    b.Property<string>("PseudoUtilisateur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdFavori");

                    b.ToTable("ListeFavoris");
                });
#pragma warning restore 612, 618
        }
    }
}
