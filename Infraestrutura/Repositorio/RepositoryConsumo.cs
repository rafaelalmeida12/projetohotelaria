using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projetohotelaria.Infraestrutura.Interface;
using projetohotelaria.Models;

namespace projetohotelaria.Infraestrutura.Repositorio
{
    public class RepositoryConsumo : RepositoryBase<Consumo>, InterfaceConsumo
    {
        private readonly DbContextOptions<Contexto> _OptionsBuilder;

        public RepositoryConsumo()
        {
            _OptionsBuilder = new DbContextOptions<Contexto>();
        }
        public async Task<int> AddRetornaId(Consumo consumo)
        {
            using (var data = new Contexto(_OptionsBuilder))
            {
                await data.Consumo.AddAsync(consumo);
                await data.SaveChangesAsync();

                int id = consumo.Id;
                return id;
            }
        }

        public async Task<List<Consumo>> BuscarConsumoReserva(int cod)
        {
            try
            {
                using (var data = new Contexto(_OptionsBuilder))
                {
                    return await data.Consumo
                    .Include(d => d.Produto)
                    .Where(c => c.CodigoReserva == cod)
                    .ToListAsync();
                }

            }
            catch (System.Exception mensagem)
            {
                var mensagems = mensagem.Message;
                throw;
            }
        }
    }
}