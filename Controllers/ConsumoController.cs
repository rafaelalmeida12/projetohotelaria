using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projetohotelaria.Models;
using projetohotelaria.Businnes;
using projetohotelaria.Infraestrutura.Interface;
using projetohotelaria.Models.Enum;

namespace projetohotelaria.Controllers
{

    public class ConsumoController : Controller
    {
        private readonly InterfaceConsumo _interfaceConsumo;
        private readonly InterfaceProduto _interfaceProduto;
        private readonly InterfaceReserva _interfaceReserva;
        public ConsumoController(InterfaceConsumo interfaceConsumo, InterfaceProduto interfaceProduto, InterfaceReserva interfaceReserva)
        {
            _interfaceConsumo = interfaceConsumo;
            _interfaceProduto = interfaceProduto;
            _interfaceReserva = interfaceReserva;
        }

        public async Task<IActionResult> Index()
        {
            var reservas = await _interfaceReserva.BuscarReservas();

            return View(reservas);
        }
        public async Task<IActionResult> Lista(int CodReserva)
        {
            var consumo = await _interfaceConsumo.BuscarConsumoReserva(CodReserva);
            ViewBag.CodReserva = CodReserva;
            return View(consumo);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumo = await _interfaceConsumo.GetEntityById((int)id);
            if (consumo == null)
            {
                return NotFound();
            }

            return View(consumo);
        }
        public async Task<IActionResult> Criar(int id, int codReserva)
        {
            Consumo consumo = new Consumo();
            consumo.ReservaId = id;
            consumo.CodigoReserva = codReserva;
            ViewBag.Produtos = await _interfaceProduto.List();
            return View(consumo);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Produtos = await _interfaceProduto.List();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Consumo consumo)
        {
            if (ModelState.IsValid)
            {
                Produto produto = await _interfaceProduto.GetEntityById(consumo.ProdutoId);
                Reserva reserva = await _interfaceReserva.GetEntityById(consumo.ReservaId);

                double valorConsumo=Consumo.CalcularValor(produto.Valor, consumo.Quantidade);
                reserva.ValorConsumo+=valorConsumo;
                 await _interfaceReserva.Update(reserva);
                consumo.Data = DateTime.Now;
                consumo.Valor = valorConsumo;
                await _interfaceConsumo.Add(consumo);

                return RedirectToAction(nameof(Lista), new { CodReserva = consumo.CodigoReserva });
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumo = await _interfaceConsumo.GetEntityById((int)id);
            if (consumo == null)
            {
                return NotFound();
            }
            return View(consumo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Consumo consumo)
        {
            if (id != consumo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _interfaceConsumo.Update(consumo);
                    return View("Edit", consumo);

                }
                catch (Exception)
                {
                    if (!await ProductExists(consumo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return View(consumo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumo = await _interfaceConsumo.GetEntityById((int)id);
            if (consumo == null)
            {
                return NotFound();
            }

            return View(consumo);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumo = await _interfaceConsumo.GetEntityById(id);
            await _interfaceConsumo.Delete(consumo);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {

            var consumo = await _interfaceConsumo.GetEntityById(id);

            return consumo != null;
        }
    }
}
