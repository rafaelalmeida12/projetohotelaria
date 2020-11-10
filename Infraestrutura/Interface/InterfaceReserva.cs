using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using projetohotelaria.Interface;
using projetohotelaria.Models;

namespace projetohotelaria.Infraestrutura.Interface
{
    public interface InterfaceReserva : InterfaceBase<Reserva>
    {
        Task<List<Reserva>> BuscarReservas();
        Task<List<Reserva>> BuscarEmAberto();
        Task<Reserva> BuscarReservaPorId(int id);
    }
}