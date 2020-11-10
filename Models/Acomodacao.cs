using System;
using System.Collections.Generic;
using System.Globalization;
using projetohotelaria.Models.Enum;

namespace projetohotelaria.Models
{
    public class Acomodacao
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public int Capacidade { get; set; }
        public TipoAcomodacao Tipo { get; set; }
        public TipoStatusAcomodacao TipoStatusAcomodacao { get; set; }
        public string Observacao { get; set; }
        public double ValorDiaria { get; set; }
        public IList<Reserva> Reservas { get; set; }

    }
}