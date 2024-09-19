using Microsoft.AspNetCore.Mvc;
using RiskAnalysis.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RiskAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BusinessesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Businesses
        [HttpGet]
        public ActionResult<IEnumerable<Businesses>> GetBusinesses()
        {
            // Tüm işletmeleri ve ilişkili sektör ve kontrat bilgilerini getirir
            return _context.Businesses
                           .Include(b => b.Sector)
                           .Include(b => b.ContractsList)
                           .ToList();
        }

        // GET: api/Businesses/5
        [HttpGet("{id}")]
        public ActionResult<Businesses> GetBusiness(int id)
        {
            // Belirtilen ID'ye sahip işletmeyi getirir
            var business = _context.Businesses
                                   .Include(b => b.Sector)
                                   .Include(b => b.ContractsList)
                                   .FirstOrDefault(b => b.BusinessId == id);

            if (business == null)
            {
                return NotFound();
            }

            return business;
        }

        // POST: api/Businesses
        [HttpPost]
        public ActionResult<Businesses> PostBusiness(Businesses business)
        {
            // Yeni bir işletme oluşturur
            business.CreatedDate = DateTime.Now;  // Kaydın oluşturulma tarihini belirler
            _context.Businesses.Add(business);
            _context.SaveChanges();

            return CreatedAtAction("GetBusiness", new { id = business.BusinessId }, business);
        }

        // PUT: api/Businesses/5
        [HttpPut("{id}")]
        public IActionResult PutBusiness(int id, Businesses business)
        {
            // Var olan bir işletmeyi günceller
            if (id != business.BusinessId)
            {
                return BadRequest();
            }

            _context.Entry(business).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Businesses.Any(e => e.BusinessId == id))
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

        // DELETE: api/Businesses/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBusiness(int id)
        {
            // Belirtilen ID'ye göre işletmeyi siler
            var business = _context.Businesses.Find(id);
            if (business == null)
            {
                return NotFound();
            }

            _context.Businesses.Remove(business);
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
    public class BusinessesController : Controller
    {
        private readonly AppDbContext _context;

        public BusinessesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Businesses
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Businesses.Include(b => b.Sector);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Businesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Businesses == null)
            {
                return NotFound();
            }

            var businesses = await _context.Businesses
                .Include(b => b.Sector)
                .FirstOrDefaultAsync(m => m.BusinessId == id);
            if (businesses == null)
            {
                return NotFound();
            }

            return View(businesses);
        }

        // GET: Businesses/Create
        public IActionResult Create()
        {
            ViewData["SectorId"] = new SelectList(_context.Sectors, "SectorId", "SectorDescription");
            return View();
        }

        // POST: Businesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessId,SectorId,BusinessName,BusinessDescription,RiskFactor,CreatedDate")] Businesses businesses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(businesses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SectorId"] = new SelectList(_context.Sectors, "SectorId", "SectorDescription", businesses.SectorId);
            return View(businesses);
        }

        // GET: Businesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Businesses == null)
            {
                return NotFound();
            }

            var businesses = await _context.Businesses.FindAsync(id);
            if (businesses == null)
            {
                return NotFound();
            }
            ViewData["SectorId"] = new SelectList(_context.Sectors, "SectorId", "SectorDescription", businesses.SectorId);
            return View(businesses);
        }

        // POST: Businesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessId,SectorId,BusinessName,BusinessDescription,RiskFactor,CreatedDate")] Businesses businesses)
        {
            if (id != businesses.BusinessId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businesses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessesExists(businesses.BusinessId))
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
            ViewData["SectorId"] = new SelectList(_context.Sectors, "SectorId", "SectorDescription", businesses.SectorId);
            return View(businesses);
        }

        // GET: Businesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Businesses == null)
            {
                return NotFound();
            }

            var businesses = await _context.Businesses
                .Include(b => b.Sector)
                .FirstOrDefaultAsync(m => m.BusinessId == id);
            if (businesses == null)
            {
                return NotFound();
            }

            return View(businesses);
        }

        // POST: Businesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Businesses == null)
            {
                return Problem("Entity set 'AppDbContext.Businesses'  is null.");
            }
            var businesses = await _context.Businesses.FindAsync(id);
            if (businesses != null)
            {
                _context.Businesses.Remove(businesses);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessesExists(int id)
        {
          return (_context.Businesses?.Any(e => e.BusinessId == id)).GetValueOrDefault();
        }
    }
}
*/