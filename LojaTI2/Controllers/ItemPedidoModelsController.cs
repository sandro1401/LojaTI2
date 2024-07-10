using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojaTI2.Data;
using LojaTI2.Models;

namespace LojaTI2.Controllers
{
    public class ItemPedidoModelsController : Controller
    {
        private readonly LojaTI2Context _context;

        public ItemPedidoModelsController(LojaTI2Context context)
        {
            _context = context;
        }

        // GET: ItemPedidoModels
        public async Task<IActionResult> Index()
        {
            var lojaTI2Context = _context.ItemPedidoModel.Include(i => i.Pedido).Include(i => i.Produto);
            return View(await lojaTI2Context.ToListAsync());
        }

        // GET: ItemPedidoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedidoModel = await _context.ItemPedidoModel
                .Include(i => i.Pedido)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemPedidoModel == null)
            {
                return NotFound();
            }

            return View(itemPedidoModel);
        }

        // GET: ItemPedidoModels/Create
        public IActionResult Create()
        {
            ViewData["IdPedido"] = new SelectList(_context.Set<PedidoModel>(), "Id", "Id");
            ViewData["IdProduto"] = new SelectList(_context.Set<ProdutoModel>(), "IdProduto", "Nome");
            return View();
        }

        // POST: ItemPedidoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPedido,IdProduto,Quantidade,ValorUnitario")] ItemPedidoModel itemPedidoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemPedidoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPedido"] = new SelectList(_context.Set<PedidoModel>(), "Id", "Id", itemPedidoModel.IdPedido);
            ViewData["IdProduto"] = new SelectList(_context.Set<ProdutoModel>(), "IdProduto", "Nome", itemPedidoModel.IdProduto);
            return View(itemPedidoModel);
        }

        // GET: ItemPedidoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedidoModel = await _context.ItemPedidoModel.FindAsync(id);
            if (itemPedidoModel == null)
            {
                return NotFound();
            }
            ViewData["IdPedido"] = new SelectList(_context.Set<PedidoModel>(), "Id", "Id", itemPedidoModel.IdPedido);
            ViewData["IdProduto"] = new SelectList(_context.Set<ProdutoModel>(), "IdProduto", "Nome", itemPedidoModel.IdProduto);
            return View(itemPedidoModel);
        }

        // POST: ItemPedidoModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPedido,IdProduto,Quantidade,ValorUnitario")] ItemPedidoModel itemPedidoModel)
        {
            if (id != itemPedidoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemPedidoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemPedidoModelExists(itemPedidoModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPedido"] = new SelectList(_context.Set<PedidoModel>(), "Id", "Id", itemPedidoModel.IdPedido);
            ViewData["IdProduto"] = new SelectList(_context.Set<ProdutoModel>(), "IdProduto", "Nome", itemPedidoModel.IdProduto);
            return View(itemPedidoModel);
        }

        // GET: ItemPedidoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedidoModel = await _context.ItemPedidoModel
                .Include(i => i.Pedido)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemPedidoModel == null)
            {
                return NotFound();
            }

            return View(itemPedidoModel);
        }

        // POST: ItemPedidoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemPedidoModel = await _context.ItemPedidoModel.FindAsync(id);
            if (itemPedidoModel != null)
            {
                _context.ItemPedidoModel.Remove(itemPedidoModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemPedidoModelExists(int id)
        {
            return _context.ItemPedidoModel.Any(e => e.Id == id);
        }
    }
}
