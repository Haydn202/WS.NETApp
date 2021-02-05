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
        public async Task<IActionResult> ShowSearchResults(string Search_Phrase, string Search_Location, string Search_Industry, bool notice_Picker)
        {
            DateTime thisDay = DateTime.Today;
            DateTime Year_Ago = new DateTime(thisDay.Year - 1, 1, 1);

            if (notice_Picker == true)
            {
                if (Search_Location.Equals("All") & Search_Industry.Equals("Any"))
                {
                    return View("ViewCompanyNotices", await _context.WSNotice.OrderByDescending(n => n.date_Issued).Where(n => n.business_Name.Contains(Search_Phrase) | n.topic.Contains(Search_Phrase) & n.date_Issued < thisDay & n.date_Issued > Year_Ago).ToListAsync());
                }
                else if (Search_Industry.Equals("Any"))
                {
                    return View("ViewCompanyNotices", await _context.WSNotice.OrderByDescending(n => n.date_Issued).Where(n => n.business_Name.Contains(Search_Phrase) | n.topic.Contains(Search_Phrase) & n.location.Contains(Search_Location) & n.date_Issued < thisDay & n.date_Issued > Year_Ago).ToListAsync());
                }
                else if (Search_Location.Equals("All"))
                {
                    return View("ViewCompanyNotices", await _context.WSNotice.OrderByDescending(n => n.date_Issued).Where(n => n.business_Name.Contains(Search_Phrase) | n.topic.Contains(Search_Phrase) & n.industry.Contains(Search_Industry) & n.date_Issued < thisDay & n.date_Issued > Year_Ago).ToListAsync());
                }
                else
                {
                    return View("ViewCompanyNotices", await _context.WSNotice.OrderByDescending(n => n.date_Issued).Where(n => n.business_Name.Contains(Search_Phrase) | n.topic.Contains(Search_Phrase) & n.location.Contains(Search_Location) & n.industry.Contains(Search_Industry) & n.date_Issued < thisDay & n.date_Issued > Year_Ago).ToListAsync());
                }
            }
            else
            {
                if (Search_Location.Equals("All") & Search_Industry.Equals("Any"))
                {
                    return View("Index", await _context.PCBU.Where(n => n.business_Name.Contains(Search_Phrase)).ToListAsync());
                }
                else if (Search_Industry.Equals("Any"))
                {
                    return View("Index", await _context.PCBU.Where(n => n.business_Name.Contains(Search_Phrase) & n.location.Contains(Search_Location)).ToListAsync());
                }
                else if (Search_Location.Equals("All"))
                {
                    return View("Index", await _context.PCBU.Where(n => n.business_Name.Contains(Search_Phrase) & n.industry.Contains(Search_Industry)).ToListAsync());
                }
                else
                {
                    return View("Index", await _context.PCBU.Where(n => n.business_Name.Contains(Search_Phrase) & n.location.Contains(Search_Location) & n.industry.Contains(Search_Industry)).ToListAsync());
                }
            }    
        }

        public async Task<IActionResult> ViewCompanyNotices(int? id)
        {
            DateTime thisDay = DateTime.Today;
            DateTime Year_Ago = new DateTime(thisDay.Year - 1, 1, 1);

            //Takes Id of company clicked and finds it in the company db, then uses that to create a string with the propper company name.
            var pCBU = await _context.PCBU.FirstOrDefaultAsync(m => m.Id == id);
            string name = pCBU.business_Name;
            //Finds that companies notices and displays them, by most recent to oldest.
            return View("ViewCompanyNotices", await _context.WSNotice.OrderByDescending(n => n.date_Issued).Where(n => n.business_Name.Equals(name) & n.date_Issued < thisDay & n.date_Issued > Year_Ago).ToListAsync());
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
