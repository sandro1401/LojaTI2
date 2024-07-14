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
    public class PedidoModelsController : Controller
    {
        private readonly LojaContext _context;

        public PedidoModelsController(LojaContext context)
        {
            _context = context;
        }

        // GET: PedidoModels
        public async Task<IActionResult> Index(int? id)
        {
            if(id.HasValue)
            {
                var cliente = await _context.Clientes.FindAsync(id);
                if (cliente == null)
                {
                    var pedidosContext = _context.Pedidos
                        .Where(P => P.ClienteId == id).OrderByDescending(p => p.Id).AsNoTracking();
                    
                    ViewBag.Cliente = cliente;
                    return View(pedidosContext);
                }
                else
                {
                    TempData["mensagem"] = MensagemModel.Serializar("Cliente não encontrado",
                        TipoMensagem.Erro);
                    return RedirectToAction("Index", "Cliente");
                }

            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar("Cliente não informado",
                    TipoMensagem.Erro);
                return RedirectToAction("Index", "Cliente");
            }
            

        }
          

        // GET: PedidoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoModel = await _context.Pedidos
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedidoModel == null)
            {
                return NotFound();
            }

            return View(pedidoModel);
        }

        // GET: PedidoModels/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "CPF");
            return View();
        }

        // POST: PedidoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataPedido,DataEntrega,ValorTotal,IdCliente")] PedidoModel pedidoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "CPF", pedidoModel.ClienteId);
            return View(pedidoModel);
        }

        // GET: PedidoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoModel = await _context.Pedidos.FindAsync(id);
            if (pedidoModel == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "CPF", pedidoModel.ClienteId);
            return View(pedidoModel);
        }

        // POST: PedidoModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataPedido,DataEntrega,ValorTotal,IdCliente")] PedidoModel pedidoModel)
        {
            if (id != pedidoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoModelExists(pedidoModel.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "CPF", pedidoModel.ClienteId);
            return View(pedidoModel);
        }

        // GET: PedidoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoModel = await _context.Pedidos
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedidoModel == null)
            {
                return NotFound();
            }

            return View(pedidoModel);
        }

        // POST: PedidoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedidoModel = await _context.Pedidos.FindAsync(id);
            if (pedidoModel != null)
            {
                _context.Pedidos.Remove(pedidoModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoModelExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
