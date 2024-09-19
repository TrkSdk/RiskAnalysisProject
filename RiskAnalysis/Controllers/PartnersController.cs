using Microsoft.AspNetCore.Mvc;
using RiskAnalysis.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RiskAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PartnersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Partners
        [HttpGet]
        public ActionResult<IEnumerable<Partners>> GetPartners()
        {
            // Tüm ortakları ve ilişkili sektör, şehir, kontrat bilgilerini getirir
            return _context.Partners
                           .Include(p => p.Sector)      // İlişkili sektör bilgilerini getirir
                           .Include(p => p.City)        // İlişkili şehir bilgilerini getirir
                           .Include(p => p.ContractsList) // İlişkili kontrat bilgilerini getirir
                           .ToList();
        }

        // GET: api/Partners/5
        [HttpGet("{id}")]
        public ActionResult<Partners> GetPartner(int id)
        {
            // Belirtilen ID'ye sahip ortağı getirir
            var partner = _context.Partners
                                  .Include(p => p.Sector)
                                  .Include(p => p.City)
                                  .Include(p => p.ContractsList)
                                  .FirstOrDefault(p => p.PartnerId == id);

            if (partner == null)
            {
                return NotFound();
            }

            return partner;
        }

        // POST: api/Partners
        [HttpPost]
        public ActionResult<Partners> PostPartner(Partners partner)
        {
            // Yeni bir ortak oluşturur
            partner.CreatedDate = DateTime.Now;  // Kaydın oluşturulma tarihini belirler
            _context.Partners.Add(partner);
            _context.SaveChanges();

            return CreatedAtAction("GetPartner", new { id = partner.PartnerId }, partner);
        }

        // PUT: api/Partners/5
        [HttpPut("{id}")]
        public IActionResult PutPartner(int id, Partners partner)
        {
            // Var olan bir ortağı günceller
            if (id != partner.PartnerId)
            {
                return BadRequest();
            }

            _context.Entry(partner).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Partners.Any(e => e.PartnerId == id))
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

        // DELETE: api/Partners/5
        [HttpDelete("{id}")]
        public IActionResult DeletePartner(int id)
        {
            // Belirtilen ID'ye göre ortağı siler
            var partner = _context.Partners.Find(id);
            if (partner == null)
            {
                return NotFound();
            }

            _context.Partners.Remove(partner);
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