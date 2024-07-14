using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojaTI2.Data;
using LojaTI2.Models;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace LojaTI2.Controllers
{
    public class EnderecoModelsController : Controller
    {
        private readonly LojaContext _context;

        public EnderecoModelsController(LojaContext context)
        {
            _context = context;
        }

        // GET: EnderecoModels
        public async Task<IActionResult> Index(int? id)
        {
            if (id.HasValue)
            {
                var cliente = await _context.Clientes.FindAsync(id);
                if (cliente != null)
                {
                    _context.Entry(cliente).Collection(c => c.Enderecos).Load();
                    ViewBag.ClienteModel = cliente;
                    return View(cliente.Enderecos);
                }
                else
                {
                    TempData["mensagem"] = MensagemModel.Serializar("Cliente não encontrado.", TipoMensagem.Erro);
                    return RedirectToAction("Index", "ClienteModels");
                }
            }
            else
            {
                TempData["mensagem"] = MensagemModel.Serializar("Só é possível mostrar endereços de um cliente específico.", TipoMensagem.Erro);
                return RedirectToAction("Index", "clienteModels");
            }
         
        }

        // GET: EnderecoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            _context.Entry(cliente).Collection(c => c.Enderecos).Load();
            var enderecoModel = cliente.Enderecos.FirstOrDefault(e => e.Id == id);

               
            if (enderecoModel == null)
            {
                return NotFound();
            }

            return View(enderecoModel);
        }

        // GET: EnderecoModels/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id.HasValue)
            {
                var cliente = await _context.Clientes.FindAsync(id);
                if(cliente != null)
                {
                    ViewBag.ClienteModel = cliente;
                }

            }
            return View();
        }
        private bool EnderecoExiste(int id, int eid)
        {
            return _context.Clientes.FirstOrDefault(c => c.Id == id)
                .Enderecos.Any(e => e.Id == eid);
        }
        // POST: EnderecoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] int? idCliente, [FromForm] EnderecoModel enderecoModel)
        {
            if (!idCliente.HasValue)
            {
                return RedirectToAction("Index", "ClienteModels");
            }

            var cliente = await _context.Clientes
                                        .Include(c => c.Enderecos)
                                        .FirstOrDefaultAsync(c => c.Id == idCliente);

            if (cliente == null)
            {
                return RedirectToAction("Index", "ClienteModels");
            }

            if (ModelState.IsValid)
            {
                if (!cliente.Enderecos.Any())
                {
                    enderecoModel.Selecionado = true;
                }

                enderecoModel.CEP = ObterCepNormalizado(enderecoModel.CEP);
                enderecoModel.ClienteModelId = idCliente.Value;

                // Adiciona o endereço diretamente à lista de endereços do cliente
                cliente.Enderecos.Add(enderecoModel);

                // Salva as alterações no contexto
                await _context.SaveChangesAsync();

                TempData["mensagem"] = MensagemModel.Serializar("Endereço cadastrado com sucesso.");
                return RedirectToAction("Index", new { id = idCliente });
            }

            ViewBag.ClienteModel = cliente;
            return View(enderecoModel);
        }


        private static string ObterCepNormalizado(string cep)
        {
            string cepNormalizado = cep.Replace("-", "").Replace(".", "").Trim();
            return cepNormalizado.Insert(5, "-");
        }

        // GET: EnderecoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            _context.Entry(cliente).Collection(c => c.Enderecos).Load();
            var enderecoModel = cliente.Enderecos.FirstOrDefault(e => e.Id == id);
            if (enderecoModel == null)
            {
                return NotFound();
            }
            return View(enderecoModel);
        }

        // POST: EnderecoModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logradouro,Numero,Complemento,Bairro,Cidade,Estado,CEP,Referencia,Selecionado")] EnderecoModel enderecoModel)
        {
            if (id != enderecoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enderecoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecoModelExists(enderecoModel.Id))
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
            return View(enderecoModel);
        }

        // GET: EnderecoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            _context.Entry(cliente).Collection(c => c.Enderecos).Load();
            var enderecoModel = cliente.Enderecos.FirstOrDefault(e => e.Id == id);
            if (enderecoModel == null)
            {
                return NotFound();
            }

            return View(enderecoModel);
        }

        // POST: EnderecoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Entry(cliente).Collection(c => c.Enderecos).Load();
            var endereco = cliente.Enderecos.FirstOrDefault(e => e.Id == id);
            if (endereco != null)
            {
                _context.Remove(endereco);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnderecoModelExists(int id)
        {
            return _context.Clientes.FirstOrDefault(c => c.Id == id)
                .Enderecos.Any(e => e.Id == id);
        }
    }
}