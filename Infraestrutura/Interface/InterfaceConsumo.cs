using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using projetohotelaria.Interface;
using projetohotelaria.Models;

namespace projetohotelaria.Infraestrutura.Interface
{
    public interface InterfaceConsumo : InterfaceBase<Consumo>
    {
        Task<int> AddRetornaId(Consumo consumo);
        Task<List<Consumo>> BuscarConsumoReserva(int cod);
    }
}