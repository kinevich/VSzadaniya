using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    public class EditionController : Controller
    {
        private readonly LibraryManagementContext _db;

        public EditionController(LibraryManagementContext db)
        {
            _db = db;
        }

        // GET: Edition
        public async Task<IActionResult> Index()
        {
            var libraryManagementContext = _db.Edition.Include(e => e.Book);
            return View(await libraryManagementContext.ToListAsync());
        }

        // GET: Edition/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var edition = await _db.Edition
                .Include(e => e.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (edition == null)
            {
                return NotFound();
            }

            return View(edition);
        }

        // GET: Edition/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_db.Book, "Id", "Title");
            return View();
        }

        // POST: Edition/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EditionNumber,PagesAmount,BookId")] Edition edition)
        {
            if (ModelState.IsValid)
            {
                edition.Id = Guid.NewGuid();
                _db.Add(edition);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_db.Book, "Id", "Title", edition.BookId);
            return View(edition);
        }

        // GET: Edition/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var edition = await _db.Edition.FindAsync(id);
            if (edition == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_db.Book, "Id", "Title", edition.BookId);
            return View(edition);
        }

        // POST: Edition/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,EditionNumber,PagesAmount,BookId")] Edition edition)
        {
            if (id != edition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(edition);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditionExists(edition.Id))
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
            ViewData["BookId"] = new SelectList(_db.Book, "Id", "Title", edition.BookId);
            return View(edition);
        }

        // GET: Edition/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var edition = await _db.Edition
                .Include(e => e.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (edition == null)
            {
                return NotFound();
            }

            return View(edition);
        }

        // POST: Edition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var edition = await _db.Edition.FindAsync(id);
            _db.Edition.Remove(edition);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditionExists(Guid id)
        {
            return _db.Edition.Any(e => e.Id == id);
        }
    }
}
