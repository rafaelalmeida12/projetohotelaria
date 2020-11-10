using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projetohotelaria.Models;
using projetohotelaria.Businnes;
using projetohotelaria.Infraestrutura.Interface;
using projetohotelaria.Models.Enum;

namespace projetohotelaria.Controllers
{

    public class AcomodacaoController : Controller
    {
        private readonly InterfaceAcomodacao _interfaceAcomodacao;
        public AcomodacaoController(InterfaceAcomodacao interfaceAcomodacao)
        {
           
            _interfaceAcomodacao = interfaceAcomodacao;
        }

        public async Task<IActionResult> Index()
        {
            var consulta = await _interfaceAcomodacao.List();
           
            return View(consulta);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acomodacao = await _interfaceAcomodacao.GetEntityById((int)id);
            if (acomodacao == null)
            {
                return NotFound();
            }

            return View(acomodacao);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Acomodacao acomodacao)
        {
            if (ModelState.IsValid)
            {
                acomodacao.TipoStatusAcomodacao = TipoStatusAcomodacao.Disponivel;
                await _interfaceAcomodacao.Add(acomodacao);

                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acomodacao = await _interfaceAcomodacao.GetEntityById((int)id);
            if (acomodacao == null)
            {
                return NotFound();
            }
            return View(acomodacao);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Acomodacao acomodacao)
        {
            if (id != acomodacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _interfaceAcomodacao.Update(acomodacao);
                    return View("Edit", acomodacao);

                }
                catch (Exception)
                {
                    if (!await ProductExists(acomodacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return View(acomodacao);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acomodacao = await _interfaceAcomodacao.GetEntityById((int)id);
            if (acomodacao == null)
            {
                return NotFound();
            }

            return View(acomodacao);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var acomodacao = await _interfaceAcomodacao.GetEntityById(id);
            await _interfaceAcomodacao.Delete(acomodacao);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {

            var objeto = await _interfaceAcomodacao.GetEntityById(id);

            return objeto != null;
        }
    }
}
