using Microsoft.AspNetCore.Mvc;
using RiskAnalysis.Models;
using System.Collections.Generic;
using System.Linq;

namespace RiskAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        [HttpGet]
        public ActionResult<IEnumerable<Cities>> GetCities()
        {
            return _context.Cities.ToList();  // Tüm şehirleri getir
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public ActionResult<Cities> GetCity(int id)
        {
            var city = _context.Cities.Find(id);

            if (city == null)
            {
                return NotFound();  // Eğer id ile şehir bulunamazsa 404 döndür
            }

            return city;
        }

        // POST: api/Cities
        [HttpPost]
        public ActionResult<Cities> PostCity(Cities city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();  // Yeni şehir ekle ve değişiklikleri kaydet

            return CreatedAtAction("GetCity", new { id = city.CityId }, city);
        }

        // PUT: api/Cities/5
        [HttpPut("{id}")]
        public IActionResult PutCity(int id, Cities city)
        {
            if (id != city.CityId)
            {
                return BadRequest();  // ID uyuşmazsa 400 döndür
            }

            _context.Entry(city).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();  // Şehri güncelle
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
                {
                    return NotFound();  // Eğer şehir yoksa 404 döndür
                }
                else
                {
                    throw;
                }
            }

            return NoContent();  // Başarılıysa 204 döndür
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public ActionResult<Cities> DeleteCity(int id)
        {
            var city = _context.Cities.Find(id);
            if (city == null)
            {
                return NotFound();  // Eğer id ile şehir bulunamazsa 404 döndür
            }

            _context.Cities.Remove(city);
            _context.SaveChanges();  // Şehri sil ve değişiklikleri kaydet

            return city;
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.CityId == id);
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
    public class CitiesController : Controller
    {
        private readonly AppDbContext _context;

        public CitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
              return _context.Cities != null ? 
                          View(await _context.Cities.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Cities'  is null.");
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var cities = await _context.Cities
                .FirstOrDefaultAsync(m => m.CityId == id);
            if (cities == null)
            {
                return NotFound();
            }

            return View(cities);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityId,CityName")] Cities cities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cities);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var cities = await _context.Cities.FindAsync(id);
            if (cities == null)
            {
                return NotFound();
            }
            return View(cities);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityId,CityName")] Cities cities)
        {
            if (id != cities.CityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitiesExists(cities.CityId))
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
            return View(cities);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var cities = await _context.Cities
                .FirstOrDefaultAsync(m => m.CityId == id);
            if (cities == null)
            {
                return NotFound();
            }

            return View(cities);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cities == null)
            {
                return Problem("Entity set 'AppDbContext.Cities'  is null.");
            }
            var cities = await _context.Cities.FindAsync(id);
            if (cities != null)
            {
                _context.Cities.Remove(cities);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitiesExists(int id)
        {
          return (_context.Cities?.Any(e => e.CityId == id)).GetValueOrDefault();
        }
    }
}
*/