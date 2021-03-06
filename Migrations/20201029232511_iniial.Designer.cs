﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using projetohotelaria.Infraestrutura.Repositorio;

namespace ProjetoHotelaria.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20201029232511_iniial")]
    partial class iniial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("projetohotelaria.Models.Acomodacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Capacidade")
                        .HasColumnType("int");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Observacao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<int>("TipoStatusAcomodacao")
                        .HasColumnType("int");

                    b.Property<double>("ValorDiaria")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Acomodacao");
                });

            modelBuilder.Entity("projetohotelaria.Models.Consumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CodigoReserva")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("ReservaId")
                        .HasColumnType("int");

                    b.Property<double>("Valor")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("ReservaId");

                    b.ToTable("Consumo");
                });

            modelBuilder.Entity("projetohotelaria.Models.Hospede", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Telefone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Hospede");
                });

            modelBuilder.Entity("projetohotelaria.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("QuantidadeEstoque")
                        .HasColumnType("int");

                    b.Property<double>("Valor")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("projetohotelaria.Models.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AcomodacaoId")
                        .HasColumnType("int");

                    b.Property<double?>("Antecipacao")
                        .HasColumnType("double");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataConfirmacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("EntradaPrevista")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("HospedeId")
                        .HasColumnType("int");

                    b.Property<int>("QuantidadeHospede")
                        .HasColumnType("int");

                    b.Property<DateTime>("SaidaPrevista")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TipoStatus")
                        .HasColumnType("int");

                    b.Property<double>("ValorConsumo")
                        .HasColumnType("double");

                    b.Property<double>("ValorReserva")
                        .HasColumnType("double");

                    b.Property<double>("ValorTotal")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("AcomodacaoId");

                    b.HasIndex("HospedeId");

                    b.ToTable("Reserva");
                });

            modelBuilder.Entity("projetohotelaria.Models.Consumo", b =>
                {
                    b.HasOne("projetohotelaria.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("projetohotelaria.Models.Reserva", null)
                        .WithMany("Consumo")
                        .HasForeignKey("ReservaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("projetohotelaria.Models.Reserva", b =>
                {
                    b.HasOne("projetohotelaria.Models.Acomodacao", "Acomodacao")
                        .WithMany("Reservas")
                        .HasForeignKey("AcomodacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("projetohotelaria.Models.Hospede", "Hospede")
                        .WithMany()
                        .HasForeignKey("HospedeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
