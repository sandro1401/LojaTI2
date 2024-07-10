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
    public class NotaFiscalModelsController : Controller
    {
        private readonly LojaTI2Context _context;

        public NotaFiscalModelsController(LojaTI2Context context)
        {
            _context = context;
        }

        // GET: NotaFiscalModels
        public async Task<IActionResult> Index()
        {
            var lojaTI2Context = _context.NotaFiscalModel.Include(n => n.Cliente);
            return View(await lojaTI2Context.ToListAsync());
        }

        // GET: NotaFiscalModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaFiscalModel = await _context.NotaFiscalModel
                .Include(n => n.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notaFiscalModel == null)
            {
                return NotFound();
            }

            return View(notaFiscalModel);
        }

        // GET: NotaFiscalModels/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.ClienteModel, "Id", "CPF");
            return View();
        }

        // POST: NotaFiscalModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,DataEmissao,PedidoId,ClienteId,ValorProdutos,ValorTotal,Observacoes")] NotaFiscalModel notaFiscalModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notaFiscalModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.ClienteModel, "Id", "CPF", notaFiscalModel.ClienteId);
            return View(notaFiscalModel);
        }

        // GET: NotaFiscalModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaFiscalModel = await _context.NotaFiscalModel.FindAsync(id);
            if (notaFiscalModel == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.ClienteModel, "Id", "CPF", notaFiscalModel.ClienteId);
            return View(notaFiscalModel);
        }

        // POST: NotaFiscalModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,DataEmissao,PedidoId,ClienteId,ValorProdutos,ValorTotal,Observacoes")] NotaFiscalModel notaFiscalModel)
        {
            if (id != notaFiscalModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notaFiscalModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaFiscalModelExists(notaFiscalModel.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.ClienteModel, "Id", "CPF", notaFiscalModel.ClienteId);
            return View(notaFiscalModel);
        }

        // GET: NotaFiscalModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaFiscalModel = await _context.NotaFiscalModel
                .Include(n => n.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notaFiscalModel == null)
            {
                return NotFound();
            }

            return View(notaFiscalModel);
        }

        // POST: NotaFiscalModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notaFiscalModel = await _context.NotaFiscalModel.FindAsync(id);
            if (notaFiscalModel != null)
            {
                _context.NotaFiscalModel.Remove(notaFiscalModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaFiscalModelExists(int id)
        {
            return _context.NotaFiscalModel.Any(e => e.Id == id);
        }
    }
}
