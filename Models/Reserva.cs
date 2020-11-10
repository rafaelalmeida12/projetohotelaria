using System;
using System.Collections.Generic;
using System.Linq;
using projetohotelaria.Models.Enum;

namespace projetohotelaria.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public int QuantidadeHospede { get; set; }
        public DateTime EntradaPrevista { get; set; }
        public DateTime SaidaPrevista { get; set; }
        public DateTime DataConfirmacao { get; set; }
        //Valores
        public double? Antecipacao { get; set; }

        public double ValorConsumo { get; set; }

        public double ValorReserva { get; set; }

        public double ValorTotal { get; set; }


        #region Relacionamentos
        public int HospedeId { get; set; }
        public Hospede Hospede { get; set; }
        public int AcomodacaoId { get; set; }
        public Acomodacao Acomodacao { get; set; }
        public IList<Consumo> Consumo { get; set; }
        public TipoStatus TipoStatus { get; set; }
        //somente leitura
        public double SomaQuantidade
        {
            get { return Consumo == null ? 0 : Consumo.Sum(d => d.Quantidade); }
        }

        public double SomaTotal
        {
            get { return Consumo == null ? 0 : Consumo.Sum(d => d.Valor); }
        }

        #endregion
        //Metodos da classe

    }
}

//ao cadastrar uma reserva ele fica com o status #RESERVADO
// ao colocar a data de confirmacao muda para status #CONFIRMADO
//apos confirma entrada e saida efetiva muda para status #OCUPADO
//fechar pagamento 
//liberar acomodacao zera tudo
