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
    public class StudentTest1Controller : Controller
    {
        private readonly test_CursachContext _context;

        public StudentTest1Controller(test_CursachContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var answers = await _context.StudentsAnswers.ToListAsync();
            var Test1 = await _context.Tests1.ToListAsync();
            List<Test1> Test1s = new List<Test1>();
            List<Question> questions = new List<Question>();
            int i = 0;
            var UserName = User.Identity.Name;
            var idOfStud = 0;
            foreach (var item in _context.Students)
            {
                if (item.LogIn == UserName) idOfStud = item.StudentId;
            }
            while (i < answers.Count)
            {
                foreach (var item in _context.Questions)
                {
                    if (answers[i].QuestId == item.QuestId && idOfStud == answers[i].StudentId)
                    {
                        questions.Add(item);
                    }
                }
                i++;
            }
            var list = new List<string>();
            var list2 = new List<string>();
            foreach (var item in questions)
            {
                list.Add(item.TestId.ToString());
            }
            foreach (var m in list.Distinct<string>())
            {
                list2.Add(m);
            }
            list.Clear();

            foreach (var item in _context.Tests1)
            {
                list.Add(item.TestId.ToString());
            }
            var diff = list.Except(list2);
            foreach (var item in diff)
            {
                for (int g = 0; g < Test1.Count; g++)
                {
                    if (item == Test1[g].TestId.ToString()) Test1s.Add(Test1[g]);
                }
            }
            return View(Test1s);
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
        private bool Test1Exists(int id)
        {
            return _context.Tests1.Any(e => e.TestId == id);
        }
    }
}
