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
    public class BookLibraryController : Controller
    {
        private readonly LibraryManagementContext _db;

        public BookLibraryController(LibraryManagementContext db)
        {
            _db = db;
        }

        // GET: BookLibrary
        public async Task<IActionResult> Index()
        {
            var libraryManagementContext = _db.BookLibrary.Include(b => b.Book).Include(b => b.Library);
            return View(await libraryManagementContext.ToListAsync());
        }

        // GET: BookLibrary/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookLibrary = await _db.BookLibrary
                .Include(b => b.Book)
                .Include(b => b.Library)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookLibrary == null)
            {
                return NotFound();
            }

            return View(bookLibrary);
        }

        // GET: BookLibrary/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_db.Book, "Id", "Title");
            ViewData["LibraryId"] = new SelectList(_db.Library, "Id", "Name");
            return View();
        }

        // POST: BookLibrary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LibraryId,BookId")] BookLibrary bookLibrary)
        {
            if (ModelState.IsValid)
            {
                bookLibrary.Id = Guid.NewGuid();
                _db.Add(bookLibrary);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_db.Book, "Id", "Title", bookLibrary.BookId);
            ViewData["LibraryId"] = new SelectList(_db.Library, "Id", "Name", bookLibrary.LibraryId);
            return View(bookLibrary);
        }

        // GET: BookLibrary/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookLibrary = await _db.BookLibrary.FindAsync(id);
            if (bookLibrary == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_db.Book, "Id", "Title", bookLibrary.BookId);
            ViewData["LibraryId"] = new SelectList(_db.Library, "Id", "Name", bookLibrary.LibraryId);
            return View(bookLibrary);
        }

        // POST: BookLibrary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,LibraryId,BookId")] BookLibrary bookLibrary)
        {
            if (id != bookLibrary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(bookLibrary);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookLibraryExists(bookLibrary.Id))
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
            ViewData["BookId"] = new SelectList(_db.Book, "Id", "Title", bookLibrary.BookId);
            ViewData["LibraryId"] = new SelectList(_db.Library, "Id", "Name", bookLibrary.LibraryId);
            return View(bookLibrary);
        }

        // GET: BookLibrary/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookLibrary = await _db.BookLibrary
                .Include(b => b.Book)
                .Include(b => b.Library)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookLibrary == null)
            {
                return NotFound();
            }

            return View(bookLibrary);
        }

        // POST: BookLibrary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bookLibrary = await _db.BookLibrary.FindAsync(id);
            _db.BookLibrary.Remove(bookLibrary);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookLibraryExists(Guid id)
        {
            return _db.BookLibrary.Any(e => e.Id == id);
        }
    }
}
