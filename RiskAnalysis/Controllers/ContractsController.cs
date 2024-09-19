using Microsoft.AspNetCore.Mvc;
using RiskAnalysis.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RiskAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContractsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Contracts
        [HttpGet]
        public ActionResult<IEnumerable<Contracts>> GetContracts()
        {
            // Tüm kontratları ve ilişkili iş, ortak, ve risk bilgilerini getirir
            return _context.Contracts
                           .Include(c => c.Business)    // İlişkili iş bilgilerini getirir
                           .Include(c => c.Partner)     // İlişkili ortak bilgilerini getirir
                           .Include(c => c.Risk)        // İlişkili risk bilgilerini getirir
                           .ToList();
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public ActionResult<Contracts> GetContract(int id)
        {
            // Belirtilen ID'ye sahip kontratı getirir
            var contract = _context.Contracts
                                   .Include(c => c.Business)
                                   .Include(c => c.Partner)
                                   .Include(c => c.Risk)
                                   .FirstOrDefault(c => c.ContractId == id);

            if (contract == null)
            {
                return NotFound();
            }

            return contract;
        }

        // POST: api/Contracts
        [HttpPost]
        public ActionResult<Contracts> PostContract(Contracts contract)
        {
            // Yeni bir kontrat oluşturur
            contract.CreatedDate = DateTime.Now;  // Kaydın oluşturulma tarihini belirler
            _context.Contracts.Add(contract);
            _context.SaveChanges();

            return CreatedAtAction("GetContract", new { id = contract.ContractId }, contract);
        }

        // PUT: api/Contracts/5
        [HttpPut("{id}")]
        public IActionResult PutContract(int id, Contracts contract)
        {
            // Var olan bir kontratı günceller
            if (id != contract.ContractId)
            {
                return BadRequest();
            }

            _context.Entry(contract).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Contracts.Any(e => e.ContractId == id))
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

        // DELETE: api/Contracts/5
        [HttpDelete("{id}")]
        public IActionResult DeleteContract(int id)
        {
            // Belirtilen ID'ye göre kontratı siler
            var contract = _context.Contracts.Find(id);
            if (contract == null)
            {
                return NotFound();
            }

            _context.Contracts.Remove(contract);
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
    public class ContractsController : Controller
    {
        private readonly AppDbContext _context;

        public ContractsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Contracts.Include(c => c.Business).Include(c => c.Partner).Include(c => c.Risk);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contracts = await _context.Contracts
                .Include(c => c.Business)
                .Include(c => c.Partner)
                .Include(c => c.Risk)
                .FirstOrDefaultAsync(m => m.ContractId == id);
            if (contracts == null)
            {
                return NotFound();
            }

            return View(contracts);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["BusinessId"] = new SelectList(_context.Businesses, "BusinessId", "BusinessDescription");
            ViewData["PartnerId"] = new SelectList(_context.Partners, "PartnerId", "ContactEMail");
            ViewData["RiskId"] = new SelectList(_context.Risks, "RiskId", "RiskId");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractId,BusinessId,PartnerId,ContractName,StartDate,EndDate,RiskId,CreatedDate")] Contracts contracts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contracts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessId"] = new SelectList(_context.Businesses, "BusinessId", "BusinessDescription", contracts.BusinessId);
            ViewData["PartnerId"] = new SelectList(_context.Partners, "PartnerId", "ContactEMail", contracts.PartnerId);
            ViewData["RiskId"] = new SelectList(_context.Risks, "RiskId", "RiskId", contracts.RiskId);
            return View(contracts);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contracts = await _context.Contracts.FindAsync(id);
            if (contracts == null)
            {
                return NotFound();
            }
            ViewData["BusinessId"] = new SelectList(_context.Businesses, "BusinessId", "BusinessDescription", contracts.BusinessId);
            ViewData["PartnerId"] = new SelectList(_context.Partners, "PartnerId", "ContactEMail", contracts.PartnerId);
            ViewData["RiskId"] = new SelectList(_context.Risks, "RiskId", "RiskId", contracts.RiskId);
            return View(contracts);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractId,BusinessId,PartnerId,ContractName,StartDate,EndDate,RiskId,CreatedDate")] Contracts contracts)
        {
            if (id != contracts.ContractId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contracts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractsExists(contracts.ContractId))
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
            ViewData["BusinessId"] = new SelectList(_context.Businesses, "BusinessId", "BusinessDescription", contracts.BusinessId);
            ViewData["PartnerId"] = new SelectList(_context.Partners, "PartnerId", "ContactEMail", contracts.PartnerId);
            ViewData["RiskId"] = new SelectList(_context.Risks, "RiskId", "RiskId", contracts.RiskId);
            return View(contracts);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contracts == null)
            {
                return NotFound();
            }

            var contracts = await _context.Contracts
                .Include(c => c.Business)
                .Include(c => c.Partner)
                .Include(c => c.Risk)
                .FirstOrDefaultAsync(m => m.ContractId == id);
            if (contracts == null)
            {
                return NotFound();
            }

            return View(contracts);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contracts == null)
            {
                return Problem("Entity set 'AppDbContext.Contracts'  is null.");
            }
            var contracts = await _context.Contracts.FindAsync(id);
            if (contracts != null)
            {
                _context.Contracts.Remove(contracts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractsExists(int id)
        {
          return (_context.Contracts?.Any(e => e.ContractId == id)).GetValueOrDefault();
        }
    }
}
*/