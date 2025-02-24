using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom.Domain.Entities;

namespace Diplom.Controllers;

public class StartTestController : Controller
{
	private readonly test_CursachContext _context;

	public StartTestController(test_CursachContext context)
	{
		_context = context;
	}

	public async Task<IActionResult> Start(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		var quests = _context.Questions.Where(x => x.TestId == id);
		if (quests == null)
		{
			return NotFound();
		}

		return View(await quests.ToListAsync());
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> StartPost(string[] items, int Test1_id)
	{
		double Score = 0;
		double MaxScore = 0;
		Results newResult = new();
		int ident = 0;
		int i = 0;
		if (ModelState.IsValid)
		{
			string UserName = User.Identity.Name;
			foreach (var item in _context.Students)
			{
				if (item.LogIn == UserName) ident = item.StudentId;
			}

			var questions = new List<Question>();
			foreach (var item in _context.Questions)
			{
				if (item.TestId == Test1_id)
				{
					questions.Add(item);
					MaxScore += item.QuestScore;
				}
			}


			foreach (var item in questions)
			{
				var stud = new StudentsAnswer
				{
					StudentId = ident,
					QuestId = item.QuestId,
					EnteredAnswer = items[i].ToLower()
				};
				_context.Add(stud);
				await _context.SaveChangesAsync();
				i++;
			}

			var rights = _context.RightAnswers.ToList();
			var n1 = new List<int>();
			var n2 = new List<int>();
			foreach (var item in rights)
			{
				n1.Add(item.QuestId);
			}

			foreach (var item in questions)
			{
				n2.Add(item.QuestId);
			}

			var df = n1.Except(n2);
			var rights2 = new List<RightAnswer>();
			foreach (var item in df)
			{
				var value = rights.Find(item2 => item2.QuestId == item);
				rights.RemoveAt(rights.IndexOf(value));
			}

			foreach (var item in items)
			{
				var value = rights.Find(item2 => item2.RightAnswer1 == item);
				if (value != null)
				{
					if (item == value.RightAnswer1)
						Score += questions[rights.IndexOf(value)].QuestScore;
				}
			}

			newResult.MaxScore = MaxScore;
			newResult.Score = Score;
			int MinScore = 0;
			foreach (var item in _context.Tests1)
			{
				if (item.TestId == Test1_id) MinScore = item.MinScore;
			}

			if (Score >= MinScore) newResult.passed = true;
			else newResult.passed = false;
		}

		return View(newResult);
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
	public async Task<IActionResult> Edit(int id,
		[Bind("QuestId,QuestScore,QuestText,TestId,Photo")] Question question)
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