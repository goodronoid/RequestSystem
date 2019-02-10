using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RequestSystem.Models;

namespace RequestSystem.Controllers
{
    
    public class NotesController : Controller
    {
        private readonly RequestSystemContext _context;

        public NotesController(RequestSystemContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index(string searchString, DateTime start, DateTime end)
        {
            ViewBag.start = start;
            ViewBag.end = end;
            ViewBag.searchString = searchString;
            
            if (searchString is null)
            {searchString = "а";}

            if (start == null)
            {start = new DateTime(0002, 1, 01, 1, 01, 01, 01);}

            if (end == DateTime.MinValue.Date)
            { end = DateTime.MaxValue.Date; }

            return View(await _context.Note.Where(s => s.Status.Contains(searchString) && s.CreateDate >= start && s.CreateDate <= end).OrderBy(x => x.Id).ToListAsync()); // && s.CreateDate >= start && s.CreateDate <= end).ToListAsync());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // GET: Notes/Create
        public IActionResult Create(string CreateDate2)
        {
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CreateDate,Status,History")] Note note)
        {
            if (ModelState.IsValid)
            {
                note.CreateDate = DateTime.Now;
                note.History = DateTime.Now + " | " + note.Status + " | " + note.History + "\n\n";

                _context.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // if (_context.Note.Where(d => d.Id.Equals()))
            //     {
            //         
            //     }

            var note = await _context.Note.FindAsync(id);
            //ViewData["CurrentStatus"] = note.Status;
            //DateTime.ToString("yyyy-MM-dd HH:mm:ss")
            //note.CreateDate = note.CreateDate.ToString();

            if (note == null)
            {
                return NotFound();
            }
            

            else if (note.Status == "Открыта")
            {
                ViewBag.Statuseslist2 = (new string[] {"Открыта", "Решена"});
            }

            else if (note.Status == "Решена")
            {
                ViewBag.Statuseslist2 = (new string[] {"Решена", "Возврат",  "Закрыта" });
            }

            else if (note.Status == "Возврат")
            {
                ViewBag.Statuseslist2 = (new string[] {"Возврат", "Решена" });
            }

            else if (note.Status == "Закрыта")
            {
                ViewBag.Statuseslist2 = (new string[] { "Закрыта" });
            }

            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Status,History")] Note note)
        {
            if (id != note.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var oldhistory = _context.Note.Where(p => p.Id == note.Id).AsNoTracking().FirstOrDefault();
                if (oldhistory != null)
                {
                    note.History = oldhistory.History + DateTime.Now + " | " + note.Status + " | " + note.History + "\n\n";

                }
                note.CreateDate = _context.Note.Where(p => p.Id == note.Id).AsNoTracking().FirstOrDefault().CreateDate;
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.Id))
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
            return View(note);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Note.FindAsync(id);
            _context.Note.Remove(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.Id == id);
        }
    }
}
