using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projetohotelaria.Infraestrutura.Interface;
using projetohotelaria.Models;
using projetohotelaria.Models.Enum;
using projetohotelaria.Models.ViewModel;
using ProjetoHotelaria.Models;

namespace ProjetoHotelaria.Controllers
{
    public class HomeController : Controller
    {
        private readonly InterfaceAcomodacao _interfaceAcomodacao;
        private readonly InterfaceReserva _interfaceReserva;
        public HomeController(InterfaceAcomodacao interfaceAcomodacao, InterfaceReserva interfaceReserva)
        {
            _interfaceAcomodacao = interfaceAcomodacao;
            _interfaceReserva = interfaceReserva;
        }


        public async Task<IActionResult> Index()
        {
            var listaAcomodacao = await _interfaceAcomodacao.List();
            IList<AcomodacaoReserva> ListaacomodacaoReserva = new List<AcomodacaoReserva>();

            foreach (var Acomo in listaAcomodacao)
            {
                AcomodacaoReserva acomodacaoReserva = new AcomodacaoReserva();
                acomodacaoReserva.Acomodacao = new Acomodacao();
                acomodacaoReserva.Reserva = new Reserva();

                if (Acomo.TipoStatusAcomodacao != TipoStatusAcomodacao.Disponivel)
                {

                    acomodacaoReserva.Reserva = await _interfaceReserva.GetEntityById(Acomo.Id);
                    acomodacaoReserva.Acomodacao = Acomo;
                    ListaacomodacaoReserva.Add(acomodacaoReserva);
                }
                else
                {
                    acomodacaoReserva.Acomodacao.Descricao = Acomo.Descricao;
                    acomodacaoReserva.Acomodacao.Id = Acomo.Id;
                    // acomodacaoReserva.Reserva.Codigo = reser.Codigo;
                    acomodacaoReserva.Acomodacao.Tipo = Acomo.Tipo;
                    //    acomodacaoReserva.Reserva.EntradaPrevista = reser.EntradaPrevista;
                    //  acomodacaoReserva.Reserva.SaidaPrevista = reser.SaidaPrevista;
                    ListaacomodacaoReserva.Add(acomodacaoReserva);
                }
            }

            return View(ListaacomodacaoReserva);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
