using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom;

namespace Diplom.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly test_CursachContext _context;
        public QuestionsController(test_CursachContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var test_CursachContext = _context.Questions.Include(q => q.Test);
            return View(await test_CursachContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Test)
                .FirstOrDefaultAsync(m => m.QuestId == id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }
        public IActionResult Create()
        {
            ViewData["TestId"] = new SelectList(_context.Tests1, "TestId", "Test1Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestId,QuestScore,QuestText,TestId,Photo")] Question question)
        {
            if (ModelState.IsValid)
            {
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TestId"] = new SelectList(
                _context.Tests1,
                "TestId",
                "Test1Name",
                question.TestId
                );
            return View(question);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["TestId"] = new SelectList(
                _context.Tests1,
                "TestId",
                "Test1Name",
                question.TestId
                );
            return View(question);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestId,QuestScore,QuestText,TestId,Photo")] Question question)
        {
            if (id != question.QuestId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.QuestId))
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
            ViewData["TestId"] = new SelectList(
                _context.Tests1,
                "TestId",
                "Test1Name",
                question.TestId
                );
            return View(question);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Test)
                .FirstOrDefaultAsync(m => m.QuestId == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool QuestionExists(int id) =>
            _context.Questions.Any(e => e.QuestId == id);
    }
}
