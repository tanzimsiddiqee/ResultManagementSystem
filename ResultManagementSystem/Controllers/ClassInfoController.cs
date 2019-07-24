using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResultManagementSystem.Data;
using ResultManagementSystem.Models;

namespace ResultManagementSystem.Controllers
{
    public class ClassInfoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClassInfo
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClassInfo.Include(c => c.Class_).Include(c => c.Section);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClassInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classInfo = await _context.ClassInfo
                .Include(c => c.Class_)
                .Include(c => c.Section)
                .FirstOrDefaultAsync(m => m.ClassInfoID == id);
            if (classInfo == null)
            {
                return NotFound();
            }

            return View(classInfo);
        }

        // GET: ClassInfo/Create
        public IActionResult Create()
        {
            ViewData["ClassID"] = new SelectList(_context.Classes, "ClassID", "Name");
            ViewData["SectionID"] = new SelectList(_context.Sections, "SectionID", "Name");
            return View();
        }

        // POST: ClassInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassInfoID,ClassInfoName,ClassID,SectionID")] ClassInfo classInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassID"] = new SelectList(_context.Classes, "ClassID", "ClassID", classInfo.ClassID);
            ViewData["SectionID"] = new SelectList(_context.Sections, "SectionID", "SectionID", classInfo.SectionID);
            return View(classInfo);
        }

        // GET: ClassInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classInfo = await _context.ClassInfo.FindAsync(id);
            if (classInfo == null)
            {
                return NotFound();
            }
            ViewData["ClassID"] = new SelectList(_context.Classes, "ClassID", "ClassID", classInfo.ClassID);
            ViewData["SectionID"] = new SelectList(_context.Sections, "SectionID", "SectionID", classInfo.SectionID);
            return View(classInfo);
        }

        // POST: ClassInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassInfoID,ClassInfoName,ClassID,SectionID")] ClassInfo classInfo)
        {
            if (id != classInfo.ClassInfoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassInfoExists(classInfo.ClassInfoID))
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
            ViewData["ClassID"] = new SelectList(_context.Classes, "ClassID", "ClassID", classInfo.ClassID);
            ViewData["SectionID"] = new SelectList(_context.Sections, "SectionID", "SectionID", classInfo.SectionID);
            return View(classInfo);
        }

        // GET: ClassInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classInfo = await _context.ClassInfo
                .Include(c => c.Class_)
                .Include(c => c.Section)
                .FirstOrDefaultAsync(m => m.ClassInfoID == id);
            if (classInfo == null)
            {
                return NotFound();
            }

            return View(classInfo);
        }

        // POST: ClassInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classInfo = await _context.ClassInfo.FindAsync(id);
            _context.ClassInfo.Remove(classInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassInfoExists(int id)
        {
            return _context.ClassInfo.Any(e => e.ClassInfoID == id);
        }
    }
}
