using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Diplom.Service;

namespace Diplom.Controllers;

public class PersonalStudController : Controller
{
	private readonly test_CursachContext _context;
	private readonly DataManager _dataManager;

	public PersonalStudController(test_CursachContext context, DataManager dataManager)
	{
		_context = context;
		_dataManager = dataManager;
	}

	// GET: PersonalStud
	[Authorize]
	public async Task<IActionResult> Index()
	{
		string userName = User.Identity.Name;
		var id = _context.Students
			.Where(x => x.LogIn == userName)
			.Select(x => x.StudentId)
			.FirstOrDefault();

		if (id == 0)
		{
			id = _context.Tutors
				.Where(x => x.LogIn == userName)
				.Select(x => x.TutorId)
				.FirstOrDefault();
		}

		var student = await _context.Students.FindAsync(id);
		ViewData["GroupId"] = new SelectList(_context.GroupIds, "GroupId1", "Course", student.GroupId);
		return View(student);
	}

	[HttpGet]
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
		else return View(student);
	}

	public async Task<IActionResult> ChangePassword(string password)
	{
		var userName = User.Identity.Name;
		var student = _context.Students
			.Where(x => x.LogIn == userName)
			.FirstOrDefault();

		if (student != null)
		{
			student.PassWord = password;
			_context.Update(student);
			await _context.SaveChangesAsync();
			return View(student);
		}
		else return NotFound();
	}

	[HttpGet]
	public async Task<IActionResult> CreateReports()
	{
		var answers = await _context.StudentsAnswers.ToListAsync();
		var alltests = new List<Test1>();
		var questions = new List<Question>();
		int i = 0;
		var userName = User.Identity.Name;
		var studId = _context.Students
			.Where(x => x.LogIn == userName)
			.Select(x => x.StudentId)
			.First();

		while (i < answers.Count)
		{
			foreach (var item in _context.Questions)
			{
				if (answers[i].QuestId == item.QuestId && answers[i].StudentId == studId)
				{
					questions.Add(item);
				}
			}

			i++;
		}

		foreach (var item in _context.Tests1)
		{
			for (int j = 0; j < questions.Count; j++)
			{
				if (item.TestId == questions[j].TestId)
				{
					alltests.Add(item);
					break;
				}
			}
		}

		return View(alltests);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Reports(string users, string format)
	{
		if (users != null)
		{
			var stud = new Student();
			var Test1 = new Test1();

			foreach (var item in _context.Students)
			{
				if (item.LogIn == User.Identity.Name) stud = item;
			}

			foreach (var item in _context.Tests1)
			{
				if (item.TestId.ToString() == users) Test1 = item;
			}

			var contexts = _context;
			/*Report report = new Report(contexts);*/
			/* if (format != "doc") report.CreatePdf(Test1, stud);
			 else report.CreateDoc(Test1, stud);*/
		}

		return View(await _context.Tests1.ToListAsync());
	}

	public async Task<IActionResult> Edit(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		var student = await _dataManager._studRepository.GetStudentsById(id);
		ViewData["GroupId"] = new SelectList(
			_context.GroupIds, "GroupId1", "Course", student.GroupId
		);
		return View(student);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
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
			"GroupId1", "Course",
			student.GroupId
		);
		return View(student);
	}

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
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		var student = _dataManager._studRepository.GetStudentsById(id);
		_context.Students.Remove(student.Result);
		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	private bool StudentExists(int id) => _context.Students.Any(e => e.StudentId == id);
}