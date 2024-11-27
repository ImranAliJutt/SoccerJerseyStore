using Microsoft.AspNetCore.Mvc;
using SoccerJerseyStore.Data;
using SoccerJerseyStore.Models;
using System.Linq;

namespace SoccerJerseyStore.Controllers
{
    public class JerseysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JerseysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jerseys
        public IActionResult Index()
        {
            var jerseys = _context.Jerseys.ToList();
            return View(jerseys);
        }

        // GET: Jerseys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jerseys/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Jersey jersey)
        {
            if (ModelState.IsValid)
            {
                _context.Jerseys.Add(jersey);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(jersey);
        }

        // GET: Jerseys/Edit/5
        public IActionResult Edit(int id)
        {
            var jersey = _context.Jerseys.Find(id);
            if (jersey == null)
            {
                return NotFound();
            }
            return View(jersey);
        }

        // POST: Jerseys/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Jersey jersey)
        {
            if (id != jersey.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(jersey);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(jersey);
        }

        // GET: Jerseys/Details/5
        public IActionResult Details(int id)
        {
            var jersey = _context.Jerseys.Find(id);
            if (jersey == null)
            {
                return NotFound();
            }
            return View(jersey);
        }

        // GET: Jerseys/Delete/5
        public IActionResult Delete(int id)
        {
            var jersey = _context.Jerseys.Find(id);
            if (jersey == null)
            {
                return NotFound();
            }
            return View(jersey);
        }

        // POST: Jerseys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var jersey = _context.Jerseys.Find(id);
            _context.Jerseys.Remove(jersey);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
