using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using projetohotelaria.Interface;
using projetohotelaria.Models;

namespace projetohotelaria.Infraestrutura.Interface
{
    public interface InterfaceAcomodacao : InterfaceBase<Acomodacao>
    {
        Task<List<Acomodacao>> Disponivel();
    }
}