using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Diplom.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Controllers;

public class AdminController : Controller
{
	private readonly test_CursachContext _context;
	private readonly IWebHostEnvironment _env;
	private readonly DataManager _dataManager;

	public AdminController(test_CursachContext context, IWebHostEnvironment env, DataManager dataManager)
	{
		_context = context;
		_env = env;
		_dataManager = dataManager;
	}

	public ActionResult Index() => View();

	[Authorize]
	public IActionResult user()
	{
		string UserName = User.Identity.Name;
		var tutor = new Tutor();
		foreach (var item in _context.Tutors)
		{
			if (item.LogIn == UserName)
			{
				tutor = item;
			}
		}

		return View(tutor);
	}

	public async Task<IActionResult> ChangePassword(string password)
	{
		string UserName = User.Identity.Name;
		Tutor tutor = new Tutor();
		foreach (var item in _context.Tutors)
		{
			if (item.LogIn == UserName) tutor = item;
		}

		tutor.PassWord = password;
		_context.Update(tutor);
		await _context.SaveChangesAsync();
		return View(tutor);
	}

	public IActionResult AllTests() => View(_dataManager._testRepository.GetTests());
	public IActionResult AllStudents() => View(_dataManager._studRepository.GetStudents());

	public async Task<IActionResult> AddFromFile(IFormFile file)
	{
		/*Test1 Test1 = new Test1();

		foreach (var item in _context.Test1) { if (item.Test1Name!=null) { Test1 = item; }}
		FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate);
		System.IO.File.WriteAllText(@"c:\movie.json", JsonConvert.SerializeObject(Test1,Formatting.Indented));*/

		var dir = _env.ContentRootPath;

		Test1 newTes;
		using (var fileStream =
		       new FileStream(Path.Combine(dir, "file.json"), FileMode.Create, FileAccess.ReadWrite))
		{
			newTes = await JsonSerializer.DeserializeAsync<Test1>(fileStream);
		}

		return View();
	}

	public async Task<IActionResult> DetailsStudent(int? id)
	{
		if (id == null) return NotFound();
		var student = await _dataManager._studRepository.GetStudentsById(id.Value);
		return View(student);
	}

	public IActionResult DetailsQuestion(int? id)
	{
		if (id == null) return NotFound();
		var question = _dataManager._questsRepository.GetQuestById(id.Value);
		return View(question);
	}

	public IActionResult DetailsTest1(int? id)
	{
		if (id == null) return NotFound();
		var Test1 = _dataManager._testRepository.GetTestById(id.Value);
		return View(Test1);
	}

	public IActionResult AddStudent() => View(_context.GroupIds.ToList());
	public IActionResult AddTest() => View(_context.StudentSubjects.ToList());

	public IActionResult AddQuestion(int? id)
	{
		if (id != null)
		{
			var list = _context.Tests1.Find(id);
			return View(list);
		}
		else return NotFound();
	}

	public async Task<IActionResult> CreateStudent(string[] items, string user)
	{
		int groupid = 0;
		if (user == null) groupid = 4;
		else
		{
			var Student = new Student();
			Student.Fio = items[0];
			Student.LogIn = items[1];
			Student.PassWord = items[2];

			foreach (var item in _context.GroupIds)
			{
				if (item.GroupName == user) groupid = item.GroupId1;
			}

			Student.GroupId = groupid;
			if (ModelState.IsValid)
			{
				_context.Add(Student);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
		}

		return View();
	}

	public async Task<IActionResult> CreateTest(string[] items, string user)
	{
		int ident = 0;
		foreach (var item in _context.Tutors)
		{
			if (item.LogIn == User.Identity.Name) ident = item.TutorId;
		}

		int groupid = 0;
		if (user == null) groupid = 2;
		else
		{
			foreach (var item in _context.StudentSubjects)
			{
				if (item.SubjectName == user) groupid = item.SubjectId;
			}

			var Test1 = new Test1()
			{
				TestName = items[0],
				TestTime = TimeSpan.FromMinutes(Convert.ToDouble(items[1])),
				MinScore = Convert.ToInt32(items[2]),
				AuthorId = ident,
				TestDate = DateTime.Now,
				SubjectId = groupid
			};
			if (ModelState.IsValid)
			{
				_context.Add(Test1);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
		}

		return View();
	}

	public async Task<IActionResult> CreateQuestion(string[] items, int user)
	{
		if (user == 0) return NotFound();
		else
		{
			var quest = new Question()
			{
				QuestScore = int.Parse(items[0]),
				QuestText = items[1],
				Photo = items[2],
				TestId = user
			};

			if (ModelState.IsValid)
			{
				_context.Add(quest);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
		}

		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(
		[Bind("TestId,Test1Name,SubjectId,AuthorId,Test1Time,MinScore,Test1Date")] Test1 Test1)
	{
		if (ModelState.IsValid)
		{
			_context.Add(Test1);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		return View(Test1);
	}

	public IActionResult EditQuestion(int? id)
	{
		if (id == null) return NotFound();
		var question = _dataManager._questsRepository.GetQuestById(id.Value);
		return View(question);
	}

	public async Task<IActionResult> EditStudent(int? id)
	{
		if (id == null) return NotFound();
		var student = await _dataManager._studRepository
			.GetStudentsById(id.Value);
		return View(student);
	}

	public IActionResult EditTest(int? id)
	{
		if (id == null) return NotFound();
		var Test1 = _dataManager._testRepository
			.GetTestById(id.Value);
		return View(Test1);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id,
		[Bind("TestId,Test1Name,SubjectId,AuthorId,Test1Time,MinScore,Test1Date")] Test1 Test1)
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
				if (!_dataManager._testRepository.TestExist(Test1.TestId))
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

	public async Task<IActionResult> DeleteStudents(int? id)
	{
		if (id == null) return NotFound();
		var stud = await _dataManager._studRepository
			.GetStudentsById(id.Value);
		return View(stud);
	}

	public IActionResult DeleteQuestion(int? id)
	{
		if (id == null) return NotFound();
		var quest = _dataManager._questsRepository
			.GetQuestById(id.Value);
		return View(quest);
	}

	public IActionResult Delete(int? id)
	{
		if (id == null) return NotFound();
		var Test1 = _dataManager._testRepository
			.GetTestById(id.Value);
		return View(Test1);
	}

	public IActionResult AllQuestions(int? id)
	{
		if (id == null) return NotFound();
		var questions = new List<Question>();
		var Test1 = _dataManager._testRepository
			.GetTestById(id.Value);
		foreach (var item in _context.Questions)
		{
			if (item.TestId == Test1.TestId) questions.Add(item);
		}

		return View(questions);
	}

	public async Task<IActionResult> CreateReports() =>
		View(await _dataManager._studRepository.GetStudents().ToListAsync());

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Reports(string users)

	{
		if (users != null)
		{
			var stud = new Student();
			var Test1 = new List<Test1>();
			var questions = new List<Question>();
			var quests = new List<int>();
			foreach (var item in _context.Students)
			{
				if (item.Fio == users) stud = item;
			}

			foreach (var item in _context.StudentsAnswers)
			{
				if (item.StudentId == stud.StudentId) quests.Add(item.QuestId);
			}

			foreach (var item in quests)
			{
				foreach (var item2 in _context.Questions)
				{
					if (item2.QuestId == item)
					{
						questions.Add(item2);
					}
				}
			}

			foreach (var item in questions)
			{
				foreach (var item2 in _context.Tests1)
				{
					if (item2.TestId == item.TestId)
					{
						Test1.Add(item2);
					}
				}
			}

			var Test12 = Test1;

			var result = Test12.Concat(Test1).Distinct();
			var contexts = _context;
			/* Report report = new Report(contexts);
			 foreach (var item in result)
			 {
			     report.CreatePdf(item, stud);
			 }*/
		}

		return View();
	}

	public async Task<IActionResult> DeleteConfirmedQuest(int id)
	{
		var stud = await _context.Questions.FindAsync(id);
		_context.Questions.Remove(stud);
		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmedStud(int id)
	{
		var stud = await _context.Students.FindAsync(id);
		_context.Students.Remove(stud);
		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmedTest(int id)
	{
		var Test1 = await _context.Tests1.FindAsync(id);
		_context.Tests1.Remove(Test1);
		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	private bool StudExists(int id) => _context.Students.Any(e => e.StudentId == id);
}