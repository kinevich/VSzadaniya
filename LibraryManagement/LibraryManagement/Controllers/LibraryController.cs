using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Models.ViewModels;

namespace LibraryManagement.Controllers
{
    public class LibraryController : Controller
    {
        private readonly LibraryManagementContext _db;

        public LibraryController(LibraryManagementContext db)
        {
            _db = db;
        }

        // GET: Library
        public async Task<IActionResult> Index()
        {
            var districts = _db.District.AsEnumerable();
            var libraries = _db.Library.AsEnumerable();

            var librariesByDistricts = from district in districts
                                       join library in libraries on district.Id equals library.DistrictId into libs
                                       select new DistrictLibrariesVM { District = district, Libraries = libs };


            return View(librariesByDistricts.ToList());

            //var libraryManagementContext = _db.Library.Include(l => l.District);
            //return View(await libraryManagementContext.ToListAsync());
        }

        // GET: Library/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library = await _db.Library
                .Include(l => l.District)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        // GET: Library/Create
        public IActionResult Create()
        {
            ViewData["DistrictId"] = new SelectList(_db.District, "Id", "Name");
            return View();
        }

        // POST: Library/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LimitDays,DistrictId")] Library library)
        {
            if (ModelState.IsValid)
            {
                library.Id = Guid.NewGuid();
                _db.Add(library);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistrictId"] = new SelectList(_db.District, "Id", "Name", library.DistrictId);
            return View(library);
        }

        // GET: Library/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library = await _db.Library.FindAsync(id);
            if (library == null)
            {
                return NotFound();
            }
            ViewData["DistrictId"] = new SelectList(_db.District, "Id", "Name", library.DistrictId);
            return View(library);
        }

        // POST: Library/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,LimitDays,DistrictId")] Library library)
        {
            if (id != library.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(library);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibraryExists(library.Id))
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
            ViewData["DistrictId"] = new SelectList(_db.District, "Id", "Name", library.DistrictId);
            return View(library);
        }

        // GET: Library/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library = await _db.Library
                .Include(l => l.District)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        // POST: Library/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var library = await _db.Library.FindAsync(id);
            _db.Library.Remove(library);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryExists(Guid id)
        {
            return _db.Library.Any(e => e.Id == id);
        }
    }
}
