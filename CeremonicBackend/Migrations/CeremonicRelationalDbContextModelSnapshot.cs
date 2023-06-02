﻿// <auto-generated />
using System;
using CeremonicBackend.DB.Relational;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CeremonicBackend.Migrations
{
    [DbContext(typeof(CeremonicRelationalDbContext))]
    partial class CeremonicRelationalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("CeremonicBackend.DB.Relational.AgreementEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConfirmStatus")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProviderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Service")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Agreements");
                });

            modelBuilder.Entity("CeremonicBackend.DB.Relational.ScalarReturn<int>", b =>
                {
                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");
                });

            modelBuilder.Entity("CeremonicBackend.DB.Relational.ServiceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("UnitOfService")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Банкетна зала",
                            UnitOfService = "*за годину аренди"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Місце проведення церемонії",
                            UnitOfService = "*за годину аренди"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Цермоніймейстр",
                            UnitOfService = "*за церемонію"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Ведучий",
                            UnitOfService = "*за весілля"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Їжа/кайтеринг",
                            UnitOfService = "*середній чек на одну людину"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Флористика та декор",
                            UnitOfService = "*на весілля"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Декор та освіщення",
                            UnitOfService = "*за стандартну композицію"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Поліграфія",
                            UnitOfService = "*усі паперові вироби для весілля"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Кондитер",
                            UnitOfService = "*за кг весільного торту"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Фотозйомка",
                            UnitOfService = "*за весілля"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Відеозйомка",
                            UnitOfService = "*за весілля"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Музика",
                            UnitOfService = "*за годину роботи"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Технічна підтримка",
                            UnitOfService = "*за годину роботи"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Візажист",
                            UnitOfService = "*за весільний макіяж"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Перукар",
                            UnitOfService = "*за весільну укладку"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Автомобіль наречених",
                            UnitOfService = "*за годину аренди"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Транспорт для гостей",
                            UnitOfService = "*за кожні 10 км"
                        },
                        new
                        {
                            Id = 18,
                            Name = "Букет нареченої",
                            UnitOfService = "*за букет"
                        });
                });

            modelBuilder.Entity("CeremonicBackend.DB.Relational.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CeremonicBackend.DB.Relational.UserLoginInfoEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserLoginInfos");
                });

            modelBuilder.Entity("CeremonicBackend.DB.Relational.UserEntity", b =>
                {
                    b.HasOne("CeremonicBackend.DB.Relational.UserLoginInfoEntity", "LoginInfo")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoginInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
