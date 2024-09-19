using Microsoft.AspNetCore.Mvc;
using RiskAnalysis.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RiskAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SectorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Sectors
        [HttpGet]
        public ActionResult<IEnumerable<Sectors>> GetSectors()
        {
            return _context.Sectors.Include(s => s.Business).ToList();
        }

        // GET: api/Sectors/5
        [HttpGet("{id}")]
        public ActionResult<Sectors> GetSector(int id)
        {
            var sector = _context.Sectors.Include(s => s.Business).FirstOrDefault(s => s.SectorId == id);

            if (sector == null)
            {
                return NotFound();
            }

            return sector;
        }

        // POST: api/Sectors
        [HttpPost]
        public ActionResult<Sectors> PostSector(Sectors sector)
        {
            _context.Sectors.Add(sector);
            _context.SaveChanges();

            return CreatedAtAction("GetSector", new { id = sector.SectorId }, sector);
        }

        // PUT: api/Sectors/5
        [HttpPut("{id}")]
        public IActionResult PutSector(int id, Sectors sector)
        {
            if (id != sector.SectorId)
            {
                return BadRequest();
            }

            _context.Entry(sector).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Sectors.Any(e => e.SectorId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Sectors/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSector(int id)
        {
            var sector = _context.Sectors.Find(id);
            if (sector == null)
            {
                return NotFound();
            }

            _context.Sectors.Remove(sector);
            _context.SaveChanges();

            return NoContent();
        }
    }
}





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
    public class SectorsController : Controller
    {
        private readonly AppDbContext _context;

        public SectorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Sectors
        public async Task<IActionResult> Index()
        {
              return _context.Sectors != null ? 
                          View(await _context.Sectors.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Sectors'  is null.");
        }

        // GET: Sectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sectors == null)
            {
                return NotFound();
            }

            var sectors = await _context.Sectors
                .FirstOrDefaultAsync(m => m.SectorId == id);
            if (sectors == null)
            {
                return NotFound();
            }

            return View(sectors);
        }

        // GET: Sectors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectorId,SectorName,SectorDescription,CreatedDate")] Sectors sectors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sectors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sectors);
        }

        // GET: Sectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sectors == null)
            {
                return NotFound();
            }

            var sectors = await _context.Sectors.FindAsync(id);
            if (sectors == null)
            {
                return NotFound();
            }
            return View(sectors);
        }

        // POST: Sectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SectorId,SectorName,SectorDescription,CreatedDate")] Sectors sectors)
        {
            if (id != sectors.SectorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sectors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectorsExists(sectors.SectorId))
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
            return View(sectors);
        }

        // GET: Sectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sectors == null)
            {
                return NotFound();
            }

            var sectors = await _context.Sectors
                .FirstOrDefaultAsync(m => m.SectorId == id);
            if (sectors == null)
            {
                return NotFound();
            }

            return View(sectors);
        }

        // POST: Sectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sectors == null)
            {
                return Problem("Entity set 'AppDbContext.Sectors'  is null.");
            }
            var sectors = await _context.Sectors.FindAsync(id);
            if (sectors != null)
            {
                _context.Sectors.Remove(sectors);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectorsExists(int id)
        {
          return (_context.Sectors?.Any(e => e.SectorId == id)).GetValueOrDefault();
        }
    }
}
*/