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
    public class ClienteModelsController : Controller
    {
        private readonly LojaTI2Context _context;

        public ClienteModelsController(LojaTI2Context context)
        {
            _context = context;
        }

        // GET: ClienteModels
        public  IActionResult Index()
        {
            var clientes = _context.ClienteModel.ToList();
            return View(clientes);
        }

        // GET: ClienteModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteModel = await _context.ClienteModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteModel == null)
            {
                return NotFound();
            }

            return View(clienteModel);
        }

        // GET: ClienteModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClienteModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteModel clienteModel)
        {
            if (clienteModel == null)
            {
                return NotFound();
            }
            _context.Add(clienteModel);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        // GET: ClienteModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ClienteModel clienteContext = _context.ClienteModel.FirstOrDefault(x => x.Id == id);
            if (clienteContext == null) {

                return NotFound();
            }

           
            return View(clienteContext);
        }

        // POST: ClienteModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ClienteModel clienteModel)
        {
        

            if (clienteModel == null)
            {
                return NotFound();
            }

            _context.Update(clienteModel);
            _context.SaveChangesAsync();
            
             TempData["mensagem"] = MensagemModel.Serializar("Cliente alterado com sucesso.");
            



            return RedirectToAction("Index");
        }

        // GET: ClienteModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteModel = await _context.ClienteModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteModel == null)
            {
                return NotFound();
            }

            return View(clienteModel);
        }

        // POST: ClienteModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteModel = await _context.ClienteModel.FindAsync(id);
            if (clienteModel != null)
            {
                _context.ClienteModel.Remove(clienteModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteModelExists(int id)
        {
            return _context.ClienteModel.Any(e => e.Id == id);
        }
    }
}
