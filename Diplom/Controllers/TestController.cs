using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom;
using Microsoft.AspNetCore.Authorization;
using Diplom.Domain.Repositories.EntityFramework;

namespace Diplom.Controllers
{
    public class Test1Controller : Controller
    {
        private readonly test_CursachContext _context;
        public Test1Controller(test_CursachContext context)
        {
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> Index() => View(await _context.Tests1.ToListAsync());
        
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TestId,Test1Name,SubjectId,AuthorId,Test1Time,MinScore,Test1Date")] Test1 Test1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Test1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Test1);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Test1 = await _context.Tests1.FindAsync(id);
            if (Test1 == null)
            {
                return NotFound();
            }
            return View(Test1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TestId,Test1Name,SubjectId,AuthorId,Test1Time,MinScore,Test1Date")] Test1 Test1)
        {
            if (id != Test1.TestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Test1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Test1Exists(Test1.TestId))
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
            return View(Test1);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Test1 = await _context.Tests1
                .FirstOrDefaultAsync(m => m.TestId == id);
            if (Test1 == null)
            {
                return NotFound();
            }
            return View(Test1);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Test1 = await _context.Tests1.FindAsync(id);
            _context.Tests1.Remove(Test1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Test1 = await _context.Tests1
                .FirstOrDefaultAsync(m => m.TestId == id);
            if (Test1 == null)
            {
                return NotFound();
            }
            return View(Test1);
        }
        private bool Test1Exists(int id)
        {
            return _context.Tests1.Any(e => e.TestId == id);
        }
    }
}
