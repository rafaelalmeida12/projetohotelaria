using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projetohotelaria.Models;
using projetohotelaria.Businnes;
using projetohotelaria.Infraestrutura.Interface;

namespace projetohotelaria.Controllers
{

    public class ProdutoController : Controller
    {
        private readonly InterfaceProduto _InterfaceProduto;

        public ProdutoController(InterfaceProduto InterfaceProduto)
        {

            _InterfaceProduto = InterfaceProduto;
        }

        public async Task<IActionResult> Index()
        {
            var consulta = await _InterfaceProduto.List();

            return View(consulta);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _InterfaceProduto.GetEntityById((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Produto product)
        {
            if (ModelState.IsValid)
            {
                await _InterfaceProduto.Add(product);

                return View("Create", product);

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _InterfaceProduto.GetEntityById((int)id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Produto product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _InterfaceProduto.Update(product);
                    return View("Edit", product);

                }
                catch (Exception)
                {
                    if (!await ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _InterfaceProduto.GetEntityById((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _InterfaceProduto.GetEntityById(id);
            await _InterfaceProduto.Delete(product);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {

            var objeto = await _InterfaceProduto.GetEntityById(id);

            return objeto != null;
        }
    }
}
