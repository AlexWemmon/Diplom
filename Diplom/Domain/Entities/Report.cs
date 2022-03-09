
using iText.IO.Font;
using iText.Kernel;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Diplom.Domain.Entities
{

	public class Report
	{
		private readonly test_CursachContext _context;

		public Report(test_CursachContext context)
		{
			_context = context;
		}
		public const String FONT = "wwwroot/webfonts/FreeSans.ttf";
		public void CreatePdf(Test1 test, Student student)
		{
			var path = System.IO.Path.GetFullPath(FONT);
			PdfWriter writer = new("C:\\" + student.Fio + "_" + test.TestName + "demo.pdf");
			PdfDocument pdf = new(writer);
			Document document = new(pdf, PageSize.A4);
			GroupId group = new();
			string subjectName = " ";
			PdfFont font1250 = PdfFontFactory.CreateFont(path, PdfEncodings.IDENTITY_H);
			double Score = 0;
			double MaxScore = 0;
			int i = 0;
			var Rights = new List<RightAnswer>();
			var Answers = _context.StudentsAnswers.ToList();
			var questions = new List<Question>();
			foreach (var item in _context.Questions)
			{
				if (item.TestId == test.TestId)
				{
					questions.Add(item);
					MaxScore += item.QuestScore;
				}
			}
			var elems = _context.StudentsAnswers.ToList();
			foreach (var item in questions)
			{
				var value = _context.RightAnswers.ToList().Find(item2 => item2.QuestId == item.QuestId);
				Rights.Add(value);
			}
			foreach (var item in questions)
			{
				var value = Answers.Find(item2 => item2.QuestId != item.QuestId);
				Answers.RemoveAt(Answers.IndexOf(value));
			}
			var listR = new List<StudentsAnswer>();
			foreach (var item2 in questions)
			{
				foreach (var item in Answers)
				{
					if (item.StudentId == student.StudentId && item2.QuestId == item.QuestId) { listR.Add(item); }
				}
			}
			Answers.Clear();
			Answers = listR;

			foreach (var item in Rights)
			{
				for (int q = 0; q < Answers.Count; q++)
				{
					if (Answers[q].EnteredAnswer == item.RightAnswer1 && Answers[q].StudentId == student.StudentId)
					{
						var value = questions.Find(item2 => item2.QuestId == item.QuestId);
						Score += value.QuestScore;

					}
				}
			}
			foreach (var item in _context.GroupIds)
			{
				if (student.GroupId == item.GroupId1) group = item;
			}
			foreach (var item in _context.StudentSubjects)
			{
				if (test.SubjectId == item.SubjectId) subjectName = item.SubjectName;
			}
			string AuthorName = "";
			foreach (var item in _context.Tutors)
			{
				if (item.TutorId == test.AuthorId) AuthorName = item.Fio;

			}
			Paragraph header = new Paragraph(
				"Студент: " + student.Fio +
				"\nГруппа: " + group.GroupName +
				"\nКурс: " + group.Course +
				"\nДата создания отчёта: " +
				"\n" + DateTime.Now.ToString())
			   .SetTextAlignment(TextAlignment.RIGHT)
			   .SetFontSize(14)
			   .SetPaddingTop(20)
			.SetFont(font1250);

			Paragraph MainH = new Paragraph("Отчёт по тесту")
				.SetTextAlignment(TextAlignment.CENTER)
				.SetFontSize(16)
				.SetBold()
				.SetPaddingTop(40)
				.SetFont(font1250);

			Paragraph Content = new Paragraph(
				"Название теста: " + test.TestName + "\n" +
				"Предмет: " + subjectName + "\n" +
				"Автор: " + AuthorName + "\n" +
				"Время на прохождение: " + test.TestTime + "\n" +
				"Проходной балл: " + test.MinScore + "\n" +
				"Дата создания теста: " + test.TestDate + "\n " +
				"Набрано баллов: " + Score + "\n" +
				"Процент правильных ответов: " + (Score / MaxScore) * 100 + "%"
				)

				.SetTextAlignment(TextAlignment.LEFT)
				.SetFontSize(14)
				.SetPaddingTop(20)
				.SetFont(font1250);

			document.Add(header);
			document.Add(MainH);
			document.Add(Content);

			document.Close();
		}
		public void CreateDoc(Test1 test, Student student)
		{
			/*string pdfFile = @"c:\demo.pdf";*//*
            MemoryStream docxStream = new MemoryStream();
            // Convert PDF to word in memory
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();

            // Assume that we already have a PDF document as stream.
            using (FileStream pdfStream = new FileStream(pdfFile, FileMode.Open, FileAccess.Read))
            {
                f.OpenPdf(pdfStream);

                if (f.PageCount > 0)
                {
                    int res = f.ToWord(docxStream);

                    // Save docxStream to a file for demonstration purposes.
                    if (res > 0)
                    {
                        string docxFile = System.IO.Path.ChangeExtension(pdfFile, ".docx");
                        File.WriteAllBytes(docxFile, docxStream.ToArray());
                        System.Diagnostics.Process.Start(docxFile);
                    }
                }
            }*/

		}
	}
}
