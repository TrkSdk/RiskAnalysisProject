using Microsoft.AspNetCore.Mvc;
using RiskAnalysis.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace RiskAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RisksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RisksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Risks
        [HttpGet]
        public ActionResult<IEnumerable<Risks>> GetRisks()
        {
            // Tüm riskleri ve ilişkili kontrat bilgilerini getirir
            return _context.Risks
                           .Include(r => r.ContractsList)  // İlişkili kontrat bilgilerini getirir
                           .ToList();
        }

        // GET: api/Risks/5
        [HttpGet("{id}")]
        public ActionResult<Risks> GetRisk(int id)
        {
            Console.WriteLine("Buraya geldi");
            Console.WriteLine(id + 1);
            // Belirtilen ID'ye sahip riski getirir
            /*var risk = _context.Risks
                               .Include(r => r.ContractsList)
                               .FirstOrDefault(r => r.RiskId == id);

            if (risk == null)
            {
                return NotFound();
            }

            return risk;*/

            var risk = _context.Risks
                   .FirstOrDefault(r => r.RiskId == id);

            if (risk == null)
            {
                return NotFound();
            }

            // Eğer ContractsList null ise boş bir liste döndür.
            risk.ContractsList = risk.ContractsList ?? new List<Contracts>();

            return risk;
        }

        // POST: api/Risks
        [HttpPost]
        public ActionResult<Risks> PostRisk(Risks risk)
        {
            // Yeni bir risk oluşturur
            _context.Risks.Add(risk);
            _context.SaveChanges();

            return CreatedAtAction("GetRisk", new { id = risk.RiskId }, risk);
        }

        // PUT: api/Risks/5
        [HttpPut("{id}")]
        public IActionResult PutRisk(int id, Risks risk)
        {
            // Var olan bir riski günceller
            if (id != risk.RiskId)
            {
                return BadRequest();
            }

            _context.Entry(risk).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Risks.Any(e => e.RiskId == id))
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

        // DELETE: api/Risks/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRisk(int id)
        {
            // Belirtilen ID'ye göre riski siler
            var risk = _context.Risks.Find(id);
            if (risk == null)
            {
                return NotFound();
            }

            _context.Risks.Remove(risk);
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
    public class RisksController : Controller
    {
        private readonly AppDbContext _context;

        public RisksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Risks
        public async Task<IActionResult> Index()
        {
              return _context.Risks != null ? 
                          View(await _context.Risks.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Risks'  is null.");
        }

        // GET: Risks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Risks == null)
            {
                return NotFound();
            }

            var risks = await _context.Risks
                .FirstOrDefaultAsync(m => m.RiskId == id);
            if (risks == null)
            {
                return NotFound();
            }

            return View(risks);
        }

        // GET: Risks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Risks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RiskId,RiskScore,RiskEstimationSuccess")] Risks risks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(risks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(risks);
        }

        // GET: Risks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Risks == null)
            {
                return NotFound();
            }

            var risks = await _context.Risks.FindAsync(id);
            if (risks == null)
            {
                return NotFound();
            }
            return View(risks);
        }

        // POST: Risks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RiskId,RiskScore,RiskEstimationSuccess")] Risks risks)
        {
            if (id != risks.RiskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(risks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RisksExists(risks.RiskId))
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
            return View(risks);
        }

        // GET: Risks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Risks == null)
            {
                return NotFound();
            }

            var risks = await _context.Risks
                .FirstOrDefaultAsync(m => m.RiskId == id);
            if (risks == null)
            {
                return NotFound();
            }

            return View(risks);
        }

        // POST: Risks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Risks == null)
            {
                return Problem("Entity set 'AppDbContext.Risks'  is null.");
            }
            var risks = await _context.Risks.FindAsync(id);
            if (risks != null)
            {
                _context.Risks.Remove(risks);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RisksExists(int id)
        {
          return (_context.Risks?.Any(e => e.RiskId == id)).GetValueOrDefault();
        }
    }
}
*/