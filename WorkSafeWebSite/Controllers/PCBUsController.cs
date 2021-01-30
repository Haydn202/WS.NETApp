using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkSafeWebSite.Data;
using WorkSafeWebSite.Models;

namespace WorkSafeWebSite.Controllers
{
    public class PCBUsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PCBUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PCBUs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PCBU.ToListAsync());
        }

        public async Task<IActionResult> ShowSearchResultsPCBU(string Search_Phrase, string Search_Location)
        {
            if (Search_Location != null)
            {
                return View("Index", await _context.PCBU.Where(n => n.business_Name.Contains(Search_Phrase) & n.location.Contains(Search_Location)).ToListAsync());
            }
            if (Search_Phrase != null)
            {
                return View("Index", await _context.PCBU.Where(n => n.location.Contains(Search_Location)).ToListAsync());
            }
            else
            {
                return View("Index", await _context.PCBU.Where(n => n.business_Name.Contains(Search_Phrase)).ToListAsync());
            }

        }

        // GET: PCBUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pCBU = await _context.PCBU
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pCBU == null)
            {
                return NotFound();
            }

            return View(pCBU);
        }

        // GET: PCBUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PCBUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,business_Number,business_Name,location,industry")] PCBU pCBU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pCBU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pCBU);
        }

        // GET: PCBUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pCBU = await _context.PCBU.FindAsync(id);
            if (pCBU == null)
            {
                return NotFound();
            }
            return View(pCBU);
        }

        // POST: PCBUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,business_Number,business_Name,location,industry")] PCBU pCBU)
        {
            if (id != pCBU.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pCBU);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PCBUExists(pCBU.Id))
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
            return View(pCBU);
        }

        // GET: PCBUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pCBU = await _context.PCBU
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pCBU == null)
            {
                return NotFound();
            }

            return View(pCBU);
        }

        // POST: PCBUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pCBU = await _context.PCBU.FindAsync(id);
            _context.PCBU.Remove(pCBU);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PCBUExists(int id)
        {
            return _context.PCBU.Any(e => e.Id == id);
        }
    }
}
