using System;

namespace projetohotelaria.Models.Enum
{
    public enum  TipoStatusAcomodacao
    {
        Disponivel,//disponivel para reserva
        Ocupados,//com uma reserva
        Vencido,//data de saida já passou
        Saida//Saida hoje
    }
}