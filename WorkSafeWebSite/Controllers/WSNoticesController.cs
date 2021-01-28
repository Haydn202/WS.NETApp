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
    public class WSNoticesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WSNoticesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WSNotices
        public async Task<IActionResult> Index()
        {
            return View(await _context.WSNotice.ToListAsync());
        }

        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // Shows search results on index page
        public async Task<IActionResult> ShowSearchResults(string Search_Phrase, string Search_Topic)
        {
            return View("Index", await _context.WSNotice.Where(n => n.business_Name.Contains(Search_Phrase) & n.topic.Contains(Search_Topic)).ToListAsync());
        }

        public async Task<IActionResult> ShowSearchResultsWithTopic(string Search_Phrase, string Search_Topic)
        {
            return View("Index", await _context.WSNotice.Where(n => n.business_Name.Contains(Search_Phrase) & n.business_Name.Contains(Search_Topic)).ToListAsync());
        }

        // GET: WSNotices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wSNotice = await _context.WSNotice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wSNotice == null)
            {
                return NotFound();
            }

            return View(wSNotice);
        }

        // GET: WSNotices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WSNotices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,business_Name,business_Number,location,industry,date_Issued,issued_To,notice_Type,topic,pdf_URL")] WSNotice wSNotice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wSNotice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wSNotice);
        }

        // GET: WSNotices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wSNotice = await _context.WSNotice.FindAsync(id);
            if (wSNotice == null)
            {
                return NotFound();
            }
            return View(wSNotice);
        }

        // POST: WSNotices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,business_Name,business_Number,location,industry,date_Issued,issued_To,notice_Type,topic,pdf_URL")] WSNotice wSNotice)
        {
            if (id != wSNotice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wSNotice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WSNoticeExists(wSNotice.Id))
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
            return View(wSNotice);
        }

        // GET: WSNotices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wSNotice = await _context.WSNotice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wSNotice == null)
            {
                return NotFound();
            }

            return View(wSNotice);
        }

        // POST: WSNotices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wSNotice = await _context.WSNotice.FindAsync(id);
            _context.WSNotice.Remove(wSNotice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WSNoticeExists(int id)
        {
            return _context.WSNotice.Any(e => e.Id == id);
        }
    }
}
