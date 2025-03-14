using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExemploEF.Data;
using ExemploEF.Models;

namespace ExemploEF.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly Context _context;

        public ProdutoController(Context context)
        {
            _context = context;
        }

        // GET: Produto
        public async Task<IActionResult> Index()
        {
            var context = _context.Produto_1.Include(p => p.Categorias);
            return View(await context.ToListAsync());
        }

        // GET: Produto/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto_1
                .Include(p => p.Categorias)
                .FirstOrDefaultAsync(m => m.Produtoid == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produto/Create
        public IActionResult Create()
        {
            ViewData["CategoriasId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId");
            return View();
        }

        // POST: Produto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Produtoid,Nome,Estoque,CategoriasId")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                produto.Produtoid = Guid.NewGuid();
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriasId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", produto.CategoriasId);
            return View(produto);
        }

        // GET: Produto/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto_1.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["CategoriasId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", produto.CategoriasId);
            return View(produto);
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Produtoid,Nome,Estoque,CategoriasId")] Produto produto)
        {
            if (id != produto.Produtoid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Produtoid))
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
            ViewData["CategoriasId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", produto.CategoriasId);
            return View(produto);
        }

        // GET: Produto/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto_1
                .Include(p => p.Categorias)
                .FirstOrDefaultAsync(m => m.Produtoid == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await _context.Produto_1.FindAsync(id);
            if (produto != null)
            {
                _context.Produto_1.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(Guid id)
        {
            return _context.Produto_1.Any(e => e.Produtoid == id);
        }
    }
}
