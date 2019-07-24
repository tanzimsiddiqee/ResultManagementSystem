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
    public class ClassController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Class
        public async Task<IActionResult> Index()
        {
            return View(await _context.Classes.ToListAsync());
        }

        // GET: Class/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Class/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassID,Name")] Class_ class_)
        {
            if (ModelState.IsValid)
            {
                _context.Add(class_);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(class_);
        }

        // GET: Class/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_ = await _context.Classes.FindAsync(id);
            if (class_ == null)
            {
                return NotFound();
            }
            return View(class_);
        }

        // POST: Class/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassID,Name")] Class_ class_)
        {
            if (id != class_.ClassID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(class_);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Class_Exists(class_.ClassID))
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
            return View(class_);
        }

        // GET: Class/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_ = await _context.Classes
                .FirstOrDefaultAsync(m => m.ClassID == id);
            if (class_ == null)
            {
                return NotFound();
            }

            return View(class_);
        }

        // POST: Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var class_ = await _context.Classes.FindAsync(id);
            _context.Classes.Remove(class_);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Class_Exists(int id)
        {
            return _context.Classes.Any(e => e.ClassID == id);
        }
    }
}
