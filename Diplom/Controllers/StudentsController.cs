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
using Diplom.Domain.Repositories.Abstract;

namespace Diplom.Controllers
{
    public class StudentsController : Controller
    {
        private readonly test_CursachContext _context;
        private IStudentsRepository _studentsRepository;
        public StudentsController(test_CursachContext context, IStudentsRepository studentsRepository)
        {
            _context = context;
            _studentsRepository = studentsRepository;
        }
        [Authorize]
        public IActionResult Index() => View();
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Group)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        [Authorize]
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.GroupIds, "GroupId1", "Course");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("StudentId,Fio,GroupId,LogIn,PassWord")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(
                _context.GroupIds,
                "GroupId1",
                "Course",
                student.GroupId
                );
            return View(student);
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var student = _studentsRepository.GetStudentsById(id.Value);
            if (student == null) return NotFound();
            else
            {
                ViewData["GroupId"] = new SelectList(
                    _context.GroupIds,
                    "GroupId1",
                    "Course",
                    student.Result.GroupId
                    );
                return View(student);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,Fio,GroupId,LogIn,PassWord")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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
            ViewData["GroupId"] = new SelectList(
                _context.GroupIds,
                "GroupId1",
                "Course",
                student.GroupId
                );
            return View(student);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Group)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
