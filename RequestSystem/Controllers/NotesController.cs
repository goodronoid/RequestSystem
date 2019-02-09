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
    public class Statuses
    {
        public string Name { get; set; }
    }

    public class NotesController : Controller
    {
        private readonly RequestSystemContext _context;

        public NotesController(RequestSystemContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Note.ToListAsync());
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
        public IActionResult Create(string Status)
        {
            ViewBag.CreateDate = DateTime.Now.ToString("g");
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CreateDate,Status")] Note note)
        {
            if (ModelState.IsValid)
            {
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
            ViewData["CurrentStatus"] = note.Status;

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

            // List<Statuses> statuseslist = new List<Statuses>
            // {
            //     new Statuses {Name="Открыта"},
            //     new Statuses {Name="Решена"},
            //     new Statuses {Name="Возврат"},
            //     new Statuses {Name="Закрыта"},
            // };
            // ViewBag.Statuseslist = new SelectList(statuseslist, "Statuses");
            // //ViewBag.Statuseslist2 = (new string[] { Statuses.Equals.ToString , "Galaxy 7 Edge", "HTC 10", "Honor 5X" });

            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CreateDate,Status")] Note note)
        {
            if (id != note.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
