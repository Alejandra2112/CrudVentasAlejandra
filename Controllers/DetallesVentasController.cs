using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudVentasAlejandra.Models;

namespace CrudVentasAlejandra.Controllers
{
    public class DetallesVentasController : Controller
    {
        private readonly TallerCrudContext _context;

        public DetallesVentasController(TallerCrudContext context)
        {
            _context = context;
        }

        // GET: DetallesVentas
        public async Task<IActionResult> Index()
        {
            var tallerCrudContext = _context.DetallesVentas.Include(d => d.Producto).Include(d => d.Venta);
            return View(await tallerCrudContext.ToListAsync());

          

        }

        // GET: DetallesVentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetallesVentas == null)
            {
                return NotFound();
            }

            var detallesVenta = await _context.DetallesVentas
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.DetalleVentaId == id);
            if (detallesVenta == null)
            {
                return NotFound();
            }

            return View(detallesVenta);
        }

        // GET: DetallesVentas/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "IdProductos", "IdProductos");
            ViewData["VentaId"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta");
            return View();
        }

        // POST: DetallesVentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetalleVentaId,VentaId,ProductoId,Cantidad,Valor")] DetallesVenta detallesVenta)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(detallesVenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "IdProductos", "IdProductos", detallesVenta.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", detallesVenta.VentaId);
            return View(detallesVenta);
        }

        // GET: DetallesVentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetallesVentas == null)
            {
                return NotFound();
            }

            var detallesVenta = await _context.DetallesVentas.FindAsync(id);
            if (detallesVenta == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "IdProductos", "IdProductos", detallesVenta.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", detallesVenta.VentaId);
            return View(detallesVenta);
        }

        // POST: DetallesVentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetalleVentaId,VentaId,ProductoId,Cantidad,Valor")] DetallesVenta detallesVenta)
        {
            if (id != detallesVenta.DetalleVentaId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallesVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallesVentaExists(detallesVenta.DetalleVentaId))
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
            ViewData["ProductoId"] = new SelectList(_context.Productos, "IdProductos", "IdProductos", detallesVenta.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", detallesVenta.VentaId);
            return View(detallesVenta);
        }

        // GET: DetallesVentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetallesVentas == null)
            {
                return NotFound();
            }

            var detallesVenta = await _context.DetallesVentas
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.DetalleVentaId == id);
            if (detallesVenta == null)
            {
                return NotFound();
            }

            return View(detallesVenta);
        }

        // POST: DetallesVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetallesVentas == null)
            {
                return Problem("Entity set 'TallerCrudContext.DetallesVentas'  is null.");
            }
            var detallesVenta = await _context.DetallesVentas.FindAsync(id);
            if (detallesVenta != null)
            {
                _context.DetallesVentas.Remove(detallesVenta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallesVentaExists(int id)
        {
          return (_context.DetallesVentas?.Any(e => e.DetalleVentaId == id)).GetValueOrDefault();
        }
    }
}
