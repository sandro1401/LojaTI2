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
    public class ProdutoModelsController : Controller
    {
        private readonly LojaContext _context;

        public ProdutoModelsController(LojaContext context)
        {
            _context = context;
        }

        // GET: ProdutoModels
        public async Task<IActionResult> Index()
        {
            var listaProdutos = _context.Produtos.Include("Categoria").ToListAsync();
            return View(await listaProdutos);
        }

        // GET: ProdutoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoModel = await _context.Produtos.Include(c => c.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtoModel == null)
            {
                return NotFound();
            }

            return View(produtoModel);
        }

        // GET: ProdutoModels/Create
        public IActionResult Create()

        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
            return View();
        }

        // POST: ProdutoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoModel produtoModel)
        {
            _context.Add(produtoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
         ProdutoModel produtoContext = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produtoContext == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", produtoContext.CategoriaId);
            return View(produtoContext);
        }

        // POST: ProdutoModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProdutoModel produtoModel)
        {
            if (produtoModel == null)
            {
                return NotFound();
            }

            
            _context.Update(produtoModel);
            await _context.SaveChangesAsync();
               
                 
            return RedirectToAction(nameof(Index));
          
       
        }

        // GET: ProdutoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produtoModel = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtoModel == null)
            {
                return NotFound();
            }

            return View(produtoModel);
        }

        // POST: ProdutoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtoModel = await _context.Produtos.FindAsync(id);
            if (produtoModel != null)
            {
                _context.Produtos.Remove(produtoModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoModelExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
