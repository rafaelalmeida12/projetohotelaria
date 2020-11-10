using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projetohotelaria.Models;
using projetohotelaria.Businnes;
using projetohotelaria.Infraestrutura.Interface;

namespace projetohotelaria.Controllers
{

    public class HospedeController : Controller
    {
        private readonly InterfaceHospede _InterfaceHospede;
        //  private readonly ProdutoServices _produtoService;
        public HospedeController(InterfaceHospede InterfaceHospede)
        {
            //   _produtoService = produtoService;
            _InterfaceHospede = InterfaceHospede;
        }

        public async Task<IActionResult> Index()
        {
            var consulta = await _InterfaceHospede.List();
            //_produtoService.ListarTodos();
            return View(consulta);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospede = await _InterfaceHospede.GetEntityById((int)id);
            if (hospede == null)
            {
                return NotFound();
            }

            return View(hospede);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Hospede hospede)
        {
            if (ModelState.IsValid)
            {
                await _InterfaceHospede.Add(hospede);

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

            var hospede = await _InterfaceHospede.GetEntityById((int)id);
            if (hospede == null)
            {
                return NotFound();
            }
            return View(hospede);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Hospede hospede)
        {
            if (id != hospede.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _InterfaceHospede.Update(hospede);
                    return View("Edit", hospede);

                }
                catch (Exception)
                {
                    if (!await ProductExists(hospede.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return View(hospede);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospede = await _InterfaceHospede.GetEntityById((int)id);
            if (hospede == null)
            {
                return NotFound();
            }

            return View(hospede);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hospede = await _InterfaceHospede.GetEntityById(id);
            await _InterfaceHospede.Delete(hospede);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {

            var objeto = await _InterfaceHospede.GetEntityById(id);

            return objeto != null;
        }
    }
}
