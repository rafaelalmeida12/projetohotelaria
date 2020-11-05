using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projetohotelaria.Infraestrutura.Interface;
using projetohotelaria.Models;
using projetohotelaria.Models.Enum;

namespace projetohotelaria.Infraestrutura.Repositorio
{
    public class RepositoryAcomodacao : RepositoryBase<Acomodacao>, InterfaceAcomodacao
    {
        private readonly DbContextOptions<Contexto> _OptionsBuilder;

        public RepositoryAcomodacao()
        {
            _OptionsBuilder = new DbContextOptions<Contexto>();
        }
        public Task<List<Acomodacao>> Disponivel()
        {
            using (var data = new Contexto(_OptionsBuilder))
            {
                return data.Acomodacao
                .Where(c => c.TipoStatusAcomodacao == TipoStatusAcomodacao.Disponivel)
                .ToListAsync();

            }
        }
    }
}