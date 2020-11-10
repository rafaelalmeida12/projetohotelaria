
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projetohotelaria.Infraestrutura.Interface;
using projetohotelaria.Models;

namespace projetohotelaria.Infraestrutura.Repositorio
{
    public class RepositoryReserva : RepositoryBase<Reserva>, InterfaceReserva
    {

        private readonly DbContextOptions<Contexto> _OptionsBuilder;

        public RepositoryReserva()
        {
            _OptionsBuilder = new DbContextOptions<Contexto>();
        }

        public Task<List<Reserva>> BuscarEmAberto()
        {
            throw new NotImplementedException();
        }

        public async Task<Reserva> BuscarReservaPorId(int id)
        {
            using (var data = new Contexto(_OptionsBuilder))
            {
                return await data.Reserva
                    .Include(c => c.Hospede)
                    .Include(d => d.Acomodacao)
                    .Include(e => e.Consumo)
                    .FirstAsync(c => c.Id == id);
            }
        }

        public async Task<List<Reserva>> BuscarReservas()
        {
            IList<Consumo> listaConsumo = new List<Consumo>();

            using (var data = new Contexto(_OptionsBuilder))
            {
                var reservas = await data.Reserva
                .Include(c => c.Hospede)
                .Include(d => d.Acomodacao)
                .Include(e => e.Consumo)
                .ToListAsync();

                return reservas;
            }
        }

    }
}