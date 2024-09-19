









/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RiskAnalysis.Models;

namespace RiskAnalysis.Controllers
{
    public class PartnersController : Controller
    {
        private readonly AppDbContext _context;

        public PartnersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Partners
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Partners.Include(p => p.City).Include(p => p.Sector);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Partners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Partners == null)
            {
                return NotFound();
            }

            var partners = await _context.Partners
                .Include(p => p.City)
                .Include(p => p.Sector)
                .FirstOrDefaultAsync(m => m.PartnerId == id);
            if (partners == null)
            {
                return NotFound();
            }

            return View(partners);
        }

        // GET: Partners/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName");
            ViewData["SectorId"] = new SelectList(_context.Sectors, "SectorId", "SectorDescription");
            return View();
        }

        // POST: Partners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartnerId,SectorId,PartnerName,ContactPerson,CityId,ContactEMail,RiskFactor,CreatedDate")] Partners partners)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partners);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName", partners.CityId);
            ViewData["SectorId"] = new SelectList(_context.Sectors, "SectorId", "SectorDescription", partners.SectorId);
            return View(partners);
        }

        // GET: Partners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Partners == null)
            {
                return NotFound();
            }

            var partners = await _context.Partners.FindAsync(id);
            if (partners == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName", partners.CityId);
            ViewData["SectorId"] = new SelectList(_context.Sectors, "SectorId", "SectorDescription", partners.SectorId);
            return View(partners);
        }

        // POST: Partners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartnerId,SectorId,PartnerName,ContactPerson,CityId,ContactEMail,RiskFactor,CreatedDate")] Partners partners)
        {
            if (id != partners.PartnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partners);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartnersExists(partners.PartnerId))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName", partners.CityId);
            ViewData["SectorId"] = new SelectList(_context.Sectors, "SectorId", "SectorDescription", partners.SectorId);
            return View(partners);
        }

        // GET: Partners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Partners == null)
            {
                return NotFound();
            }

            var partners = await _context.Partners
                .Include(p => p.City)
                .Include(p => p.Sector)
                .FirstOrDefaultAsync(m => m.PartnerId == id);
            if (partners == null)
            {
                return NotFound();
            }

            return View(partners);
        }

        // POST: Partners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Partners == null)
            {
                return Problem("Entity set 'AppDbContext.Partners'  is null.");
            }
            var partners = await _context.Partners.FindAsync(id);
            if (partners != null)
            {
                _context.Partners.Remove(partners);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartnersExists(int id)
        {
          return (_context.Partners?.Any(e => e.PartnerId == id)).GetValueOrDefault();
        }
    }
}
*/