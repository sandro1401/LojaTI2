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
    public class CategoriaModelsController : Controller
    {
        private readonly LojaContext _context;

        public CategoriaModelsController(LojaContext context)
        {
            _context = context;
        }

        // GET: CategoriaModels
        public IActionResult Index()
        {
           var categorias = _context.Categorias.ToList();
            return View(categorias);
        }

        // GET: CategoriaModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaModel = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaModel == null)
            {
                return NotFound();
            }

            return View(categoriaModel);
        }
        // GET: CategoriaModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaModel categoriaModel)
        {
            if (categoriaModel == null)
            {
                return NotFound();
            }

            _context.Add(categoriaModel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
          
        }

        // GET: CategoriaModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            CategoriaModel categoriaContext = _context.Categorias.FirstOrDefault(x  => x.Id == id);
            if ( categoriaContext == null)
            {
                return NotFound();
            }

          return View(categoriaContext);
        }

        // POST: CategoriaModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriaModel categoriaModel)
        {
            if (categoriaModel == null)
            {
                return NotFound();
            }


            _context.Update(categoriaModel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
        // GET: CategoriaModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaModel = await _context.Categorias.FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaModel == null)
            {
                return NotFound();
            }

            return View(categoriaModel);
        }

        // POST: CategoriaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaModel = await _context.Categorias.FindAsync(id);
            if (categoriaModel != null)
            {
                _context.Categorias.Remove(categoriaModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaModelExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
    }
}
