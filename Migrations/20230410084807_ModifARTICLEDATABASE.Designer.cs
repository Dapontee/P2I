﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace P2I.Migrations
{
    [DbContext(typeof(MvcJournalContext))]
    [Migration("20230410084807_ModifARTICLEDATABASE")]
    partial class ModifARTICLEDATABASE
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("Administrateur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Mdp")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Pseudo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Administrateurs");
                });

            modelBuilder.Entity("Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategorieId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Texte")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategorieId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Categorie", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Journal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Couverture")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateParution")
                        .HasColumnType("TEXT");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Numero")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Titre")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Journeaux");
                });

            modelBuilder.Entity("Article", b =>
                {
                    b.HasOne("Categorie", "CategorieArticle")
                        .WithMany()
                        .HasForeignKey("CategorieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategorieArticle");
                });
#pragma warning restore 612, 618
        }
    }
}
