using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using projetohotelaria.Models;
using Pomelo.EntityFrameworkCore.MySql;

namespace projetohotelaria.Infraestrutura.Repositorio
{
    public class Contexto : DbContext
    {

        public Contexto(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<Consumo> Consumo { get; set; }
        public DbSet<Hospede> Hospede { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<Acomodacao> Acomodacao { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql("Server=localhost;Database=db_projeto;Uid=root;Pwd=rsa123");
            base.OnConfiguring(optionsBuilder);

        }
    }
}
