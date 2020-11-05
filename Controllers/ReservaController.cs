using System;
using projetohotelaria.Infraestrutura.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using projetohotelaria.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using projetohotelaria.Models.Enum;

namespace projetohotelaria.Controllers
{

    public class ReservaController : Controller
    {
        #region Construtor
        private readonly InterfaceReserva _interfaceReserva;
        private readonly InterfaceHospede _interfaceHospede;
        private readonly InterfaceAcomodacao _interfaceAcomodacao;
        public ReservaController(InterfaceReserva interfaceReserva, InterfaceHospede interfaceHospede, InterfaceAcomodacao interfaceAcomodacao)
        {
            _interfaceReserva = interfaceReserva;
            _interfaceHospede = interfaceHospede;
            _interfaceAcomodacao = interfaceAcomodacao;
        }
        #endregion
        public async Task<IActionResult> Index()
        {
            var consulta = await _interfaceReserva.BuscarReservas();
            return View(consulta);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var reserva = await _interfaceReserva.BuscarReservaPorId((int)id);
            if (reserva == null)
                return NotFound();
            return View(reserva);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Hospede = await _interfaceHospede.List();
            ViewBag.Acomodacao = await _interfaceAcomodacao.List();
            return View();
        }
        public async Task<IActionResult> Criar(int id)
        {
            Reserva nova = new Reserva();
            nova.AcomodacaoId = id;
            ViewBag.Hospede = await _interfaceHospede.List();
            ViewBag.Acomodacao = await _interfaceAcomodacao.List();
            return View(nova);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                Acomodacao acomodacao = await _interfaceAcomodacao.GetEntityById(reserva.AcomodacaoId);
                acomodacao.TipoStatusAcomodacao = Models.Enum.TipoStatusAcomodacao.Ocupados;
                if (reserva.TipoStatus == TipoStatus.Hospedado)
                    reserva.DataConfirmacao = DateTime.Now;

                Random randNum = new Random();
                reserva.Codigo = randNum.Next();
                reserva.ValorReserva = CalculaValorDiaria(reserva.SaidaPrevista, reserva.EntradaPrevista, acomodacao.ValorDiaria);
                reserva.ValorTotal = SetValorTotal(reserva.ValorReserva, reserva.ValorConsumo, reserva.Antecipacao);
                await _interfaceReserva.Add(reserva);
                await _interfaceAcomodacao.Update(acomodacao);
                var consulta = await _interfaceReserva.GetEntityById(reserva.Id);
                return View("ResumoGeral", consulta);

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var reserva = await _interfaceReserva.GetEntityById((int)id);
            if (reserva == null)
                return NotFound();
            ViewBag.Hospede = await _interfaceHospede.List();
            ViewBag.Acomodacao = await _interfaceAcomodacao.List();
            return View(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (reserva.DataConfirmacao != new DateTime())
                        reserva.TipoStatus = TipoStatus.Hospedado;

                    await _interfaceReserva.Update(reserva);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception)
                {
                    if (!await ProductExists(reserva.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return View(reserva);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _interfaceReserva.GetEntityById((int)id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _interfaceReserva.GetEntityById(id);
            await _interfaceReserva.Delete(reserva);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {

            var objeto = await _interfaceReserva.GetEntityById(id);

            return objeto != null;
        }

        private static double CalculaValorDiaria(DateTime saida, DateTime entrada, double valorDiaria)
        {
            int dias = (int)saida.Date.Subtract(entrada.Date).TotalDays;
            return valorDiaria * dias;
        }
        public static double SetValorTotal(double valorReserva, double valorConsumo, double antecipacao?)
        {
            return antecipacao - (valorReserva + valorConsumo);
        }
    }
}