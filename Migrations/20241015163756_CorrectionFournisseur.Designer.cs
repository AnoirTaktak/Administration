﻿// <auto-generated />
using System;
using Administration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Administration.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20241015163756_CorrectionFournisseur")]
    partial class CorrectionFournisseur
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Administration.Models.Client", b =>
                {
                    b.Property<int>("ID_Client")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Client"));

                    b.Property<string>("Adresse_Client")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("MF_Client")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RS_Client")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Tel_Client")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type_Client")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_Client");

                    b.HasIndex("MF_Client")
                        .IsUnique();

                    b.HasIndex("RS_Client")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Administration.Models.Document", b =>
                {
                    b.Property<int>("ID_Document")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Document"));

                    b.Property<string>("Contenu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ID_Employe")
                        .HasColumnType("int");

                    b.Property<int>("ID_TypeDocument")
                        .HasColumnType("int");

                    b.HasKey("ID_Document");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Administration.Models.Employe", b =>
                {
                    b.Property<int>("ID_Employe")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Employe"));

                    b.Property<string>("CIN_Employe")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CNSS_Employe")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateFin")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email_Employe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom_Employe")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Poste_Employe")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Salaire")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Tel_Employe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeContrat")
                        .HasColumnType("int");

                    b.HasKey("ID_Employe");

                    b.HasIndex("CIN_Employe")
                        .IsUnique();

                    b.HasIndex("CNSS_Employe");

                    b.HasIndex("Nom_Employe")
                        .IsUnique();

                    b.ToTable("Employes");
                });

            modelBuilder.Entity("Administration.Models.FactureAchat", b =>
                {
                    b.Property<int>("ID_FactureAchat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_FactureAchat"));

                    b.Property<DateTime>("DateAchat")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EtatPaiement")
                        .HasColumnType("bit");

                    b.Property<int>("ID_Fournisseur")
                        .HasColumnType("int");

                    b.Property<byte[]>("ImageFacture")
                        .HasColumnType("varbinary(max)");

                    b.Property<decimal>("Montant")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID_FactureAchat");

                    b.ToTable("FacturesAchat");
                });

            modelBuilder.Entity("Administration.Models.FactureVente", b =>
                {
                    b.Property<int>("ID_FactureVente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_FactureVente"));

                    b.Property<int>("ClientID_Client")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateFacture")
                        .HasColumnType("datetime2");

                    b.Property<string>("NumeroFacture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TimbreFiscale")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total_FactureVente")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID_FactureVente");

                    b.HasIndex("ClientID_Client");

                    b.ToTable("FacturesVente");
                });

            modelBuilder.Entity("Administration.Models.Fournisseur", b =>
                {
                    b.Property<int>("ID_Fournisseur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Fournisseur"));

                    b.Property<string>("Adresse_Fournisseur")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email_Fournisseur")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MF_Fournisseur")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RaisonSociale_Fournisseur")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Tel_Fournisseur")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type_Fournisseur")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID_Fournisseur");

                    b.HasIndex("Email_Fournisseur")
                        .IsUnique();

                    b.HasIndex("MF_Fournisseur")
                        .IsUnique();

                    b.HasIndex("RaisonSociale_Fournisseur")
                        .IsUnique();

                    b.HasIndex("Tel_Fournisseur")
                        .IsUnique();

                    b.ToTable("Fournisseurs");
                });

            modelBuilder.Entity("Administration.Models.LigneFacture", b =>
                {
                    b.Property<int>("ID_LigneFV")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_LigneFV"));

                    b.Property<int?>("FactureVenteID_FactureVente")
                        .HasColumnType("int");

                    b.Property<int?>("ID_FactureVente")
                        .HasColumnType("int");

                    b.Property<int>("ID_Service")
                        .HasColumnType("int");

                    b.Property<int>("Quantite")
                        .HasColumnType("int");

                    b.Property<decimal>("Total_LigneFV")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total_LigneHT")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID_LigneFV");

                    b.HasIndex("FactureVenteID_FactureVente");

                    b.ToTable("LignesFacture");
                });

            modelBuilder.Entity("Administration.Models.Retenue", b =>
                {
                    b.Property<int>("ID_Retenue")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Retenue"));

                    b.Property<string>("Categorie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FactureAchatID_FactureAchat")
                        .HasColumnType("int");

                    b.Property<int>("ID_FactureAchat")
                        .HasColumnType("int");

                    b.Property<decimal>("Taux")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID_Retenue");

                    b.HasIndex("FactureAchatID_FactureAchat");

                    b.ToTable("Retenues");
                });

            modelBuilder.Entity("Administration.Models.Service", b =>
                {
                    b.Property<int>("ID_Service")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Service"));

                    b.Property<string>("Designation_Service")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PrixTTC")
                        .HasColumnType("decimal(18,3)");

                    b.Property<int>("TVA")
                        .HasColumnType("int");

                    b.HasKey("ID_Service");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Administration.Models.Societe", b =>
                {
                    b.Property<int>("ID_Societe")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Societe"));

                    b.Property<string>("Adresse_Societe")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<byte[]>("CachetSignature")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("CodePostal")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MF_Societe")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("RaisonSociale_Societe")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Tel_Societe")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("ID_Societe");

                    b.ToTable("Societes");
                });

            modelBuilder.Entity("Administration.Models.TypeDocument", b =>
                {
                    b.Property<int>("ID_TypeDocument")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_TypeDocument"));

                    b.Property<string>("NomType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Template")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_TypeDocument");

                    b.ToTable("TypesDocuments");
                });

            modelBuilder.Entity("Administration.Models.Utilisateur", b =>
                {
                    b.Property<int>("ID_Utilisateur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Utilisateur"));

                    b.Property<string>("Email_Utilisateur")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MotDePasse_Utilisateur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom_Utilisateur")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Pseudo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Role_Utilisateur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_Utilisateur");

                    b.HasIndex("Email_Utilisateur")
                        .IsUnique();

                    b.HasIndex("Nom_Utilisateur")
                        .IsUnique();

                    b.HasIndex("Pseudo")
                        .IsUnique();

                    b.ToTable("Utilisateurs");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Administration.Models.FactureVente", b =>
                {
                    b.HasOne("Administration.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientID_Client")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Administration.Models.LigneFacture", b =>
                {
                    b.HasOne("Administration.Models.FactureVente", null)
                        .WithMany("LignesFacture")
                        .HasForeignKey("FactureVenteID_FactureVente");
                });

            modelBuilder.Entity("Administration.Models.Retenue", b =>
                {
                    b.HasOne("Administration.Models.FactureAchat", "FactureAchat")
                        .WithMany()
                        .HasForeignKey("FactureAchatID_FactureAchat");

                    b.Navigation("FactureAchat");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Administration.Models.FactureVente", b =>
                {
                    b.Navigation("LignesFacture");
                });
#pragma warning restore 612, 618
        }
    }
}
