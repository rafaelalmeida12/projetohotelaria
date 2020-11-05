using System;
namespace projetohotelaria.Models
{
    public class Consumo
    {
        public int Id { get; set; }
        public int ReservaId { get; set; }
        public int CodigoReserva { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime Data { get; set; }

        private double _Valor;
        public double Valor
        {
            get { return this._Valor; }
            set { _Valor = value; }

            //get { return Consumo == null ? 0 : Consumo.Sum(d => d.ValorTotal); }

        }

        public static double CalcularValor(double Valor, double Quantidade)
        {
            return Valor * Quantidade;
        }
    }
}
//so é possivel registar um consumo se o codigo do quarto estiver ocupado