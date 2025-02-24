using Diplom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Diplom;

public partial class test_CursachContext : DbContext
{
	public test_CursachContext()
	{
	}

	public test_CursachContext(DbContextOptions<test_CursachContext> options)
		: base(options)
	{
	}

	public virtual DbSet<GroupId> GroupIds { get; set; }
	public virtual DbSet<NewWview> NewWviews { get; set; }
	public virtual DbSet<Participant> Participants { get; set; }
	public virtual DbSet<Question> Questions { get; set; }
	public virtual DbSet<RightAnswer> RightAnswers { get; set; }
	public virtual DbSet<Specialization> Specializations { get; set; }
	public virtual DbSet<Student> Students { get; set; }
	public virtual DbSet<StudentSubject> StudentSubjects { get; set; }
	public virtual DbSet<StudentsAnswer> StudentsAnswers { get; set; }
	public virtual DbSet<Test> Tests { get; set; }
	public virtual DbSet<Test1> Tests1 { get; set; }
	public virtual DbSet<Tutor> Tutors { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer("Server=DESKTOP-KN71N92\\BD_SQL;Database=test_Cursach;Trusted_Connection=True;");
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

		modelBuilder.Entity<GroupId>(entity =>
		{
			entity.HasKey(e => e.GroupId1)
				.HasName("PK__Group_id__D57795A091C7D69C");

			entity.ToTable("Group_id");

			entity.HasIndex(e => new { e.GroupId1, e.GroupName, e.Course, e.SpecialId }, "newIndexForGroups");

			entity.Property(e => e.GroupId1).HasColumnName("group_id");

			entity.Property(e => e.Course)
				.IsRequired()
				.HasMaxLength(5)
				.IsUnicode(false)
				.HasColumnName("course");

			entity.Property(e => e.GroupName)
				.IsRequired()
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("group_name");

			entity.Property(e => e.SpecialId).HasColumnName("special_id");

			entity.HasOne(d => d.Special)
				.WithMany(p => p.GroupIds)
				.HasForeignKey(d => d.SpecialId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__Group_id__specia__29572725");
		});

		modelBuilder.Entity<NewWview>(entity =>
		{
			entity.HasNoKey();

			entity.ToView("NewWView");

			entity.Property(e => e.Fio)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("fio");

			entity.Property(e => e.GroupId).HasColumnName("group_id");

			entity.Property(e => e.LogIn)
				.IsRequired()
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("log_in");

			entity.Property(e => e.PassWord)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("pass_word");

			entity.Property(e => e.StudentId)
				.ValueGeneratedOnAdd()
				.HasColumnName("student_id");
		});

		modelBuilder.Entity<Participant>(entity =>
		{
			entity.HasKey(e => e.PartId)
				.HasName("PK__particip__A0E3FAB8A632DB05");

			entity.ToTable("participant");

			entity.Property(e => e.PartId).HasColumnName("part_id");

			entity.Property(e => e.SpecialId).HasColumnName("special_id");

			entity.Property(e => e.TestId).HasColumnName("test_id");

			entity.HasOne(d => d.Special)
				.WithMany(p => p.Participants)
				.HasForeignKey(d => d.SpecialId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__participa__speci__38996AB5");

			entity.HasOne(d => d.Test)
				.WithMany(p => p.Participants)
				.HasForeignKey(d => d.TestId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__participa__test___398D8EEE");
		});

		modelBuilder.Entity<Question>(entity =>
		{
			entity.HasKey(e => e.QuestId)
				.HasName("PK__Question__9A0F00CDD156A54B");

			entity.HasIndex(e => e.QuestId, "newIndexForQuestions");

			entity.Property(e => e.QuestId).HasColumnName("quest_id");

			entity.Property(e => e.Photo)
				.IsRequired()
				.HasMaxLength(300)
				.IsUnicode(false)
				.HasColumnName("photo");

			entity.Property(e => e.QuestScore).HasColumnName("quest_score");

			entity.Property(e => e.QuestText)
				.IsRequired()
				.HasColumnType("text")
				.HasColumnName("quest_text");

			entity.Property(e => e.TestId).HasColumnName("test_id");

			entity.HasOne(d => d.Test)
				.WithMany(p => p.Questions)
				.HasForeignKey(d => d.TestId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__Questions__test___3C69FB99");
		});

		modelBuilder.Entity<RightAnswer>(entity =>
		{
			entity.HasKey(e => e.AnswerId)
				.HasName("PK__Right_an__33724318976C196A");

			entity.ToTable("Right_answers");

			entity.Property(e => e.AnswerId).HasColumnName("answer_id");

			entity.Property(e => e.QuestId).HasColumnName("quest_id");

			entity.Property(e => e.RightAnswer1)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("right_answer");

			entity.HasOne(d => d.Quest)
				.WithMany(p => p.RightAnswers)
				.HasForeignKey(d => d.QuestId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__Right_ans__quest__3F466844");
		});

		modelBuilder.Entity<Specialization>(entity =>
		{
			entity.HasKey(e => e.SpecialId)
				.HasName("PK__Speciali__D325960633289B16");

			entity.ToTable("Specialization");

			entity.HasIndex(e => new { e.SpecialId, e.SpecialName }, "newIndexForSpecial");

			entity.Property(e => e.SpecialId).HasColumnName("special_id");

			entity.Property(e => e.SpecialName)
				.IsRequired()
				.HasMaxLength(30)
				.IsUnicode(false)
				.HasColumnName("Special_name");
		});

		modelBuilder.Entity<Student>(entity =>
		{
			entity.HasIndex(e => e.StudentId, "newIndexForStudents");

			entity.Property(e => e.StudentId).HasColumnName("student_id");

			entity.Property(e => e.Fio)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("fio");

			entity.Property(e => e.GroupId).HasColumnName("group_id");

			entity.Property(e => e.LogIn)
				.IsRequired()
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("log_in");

			entity.Property(e => e.PassWord)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("pass_word");

			entity.HasOne(d => d.Group)
				.WithMany(p => p.Students)
				.HasForeignKey(d => d.GroupId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__Students__group___2C3393D0");
		});

		modelBuilder.Entity<StudentSubject>(entity =>
		{
			entity.HasKey(e => e.SubjectId)
				.HasName("PK__student___5004F660E2EF05F0");

			entity.ToTable("student_subject");

			entity.HasIndex(e => new { e.SubjectId, e.SubjectName, e.TutorId }, "StudentsSubject");

			entity.Property(e => e.SubjectId).HasColumnName("subject_id");

			entity.Property(e => e.SubjectName)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("subject_name");

			entity.Property(e => e.TutorId).HasColumnName("Tutor_id");

			entity.HasOne(d => d.Tutor)
				.WithMany(p => p.StudentSubjects)
				.HasForeignKey(d => d.TutorId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__student_s__Tutor__32E0915F");
		});

		modelBuilder.Entity<StudentsAnswer>(entity =>
		{
			entity.HasKey(e => e.AnswerId)
				.HasName("PK__Students__337243180F9322C1");

			entity.ToTable("Students_answers");

			entity.Property(e => e.AnswerId).HasColumnName("answer_id");

			entity.Property(e => e.EnteredAnswer)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("entered_answer");

			entity.Property(e => e.QuestId).HasColumnName("quest_id");

			entity.Property(e => e.StudentId).HasColumnName("student_id");

			entity.HasOne(d => d.Quest)
				.WithMany(p => p.StudentsAnswers)
				.HasForeignKey(d => d.QuestId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__Students___quest__4222D4EF");

			entity.HasOne(d => d.Student)
				.WithMany(p => p.StudentsAnswers)
				.HasForeignKey(d => d.StudentId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__Students___stude__4316F928");
		});

		modelBuilder.Entity<Test>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("Test");

			entity.Property(e => e.AuthorId).HasColumnName("Author_id");

			entity.Property(e => e.MinScore).HasColumnName("Min_score");

			entity.Property(e => e.SubjectId).HasColumnName("subject_id");

			entity.Property(e => e.TestDate)
				.HasColumnType("datetime")
				.HasColumnName("test_date");

			entity.Property(e => e.TestId)
				.ValueGeneratedOnAdd()
				.HasColumnName("test_id");

			entity.Property(e => e.TestName)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("test_name");

			entity.Property(e => e.TestTime).HasColumnName("test_time");
		});

		modelBuilder.Entity<Test1>(entity =>
		{
			entity.HasKey(e => e.TestId)
				.HasName("PK__Tests__F3FF1C0221B3D4E9");

			entity.ToTable("Tests");

			entity.HasIndex(e => e.TestId, "newIndexForTests");

			entity.Property(e => e.TestId).HasColumnName("test_id");

			entity.Property(e => e.AuthorId).HasColumnName("Author_id");

			entity.Property(e => e.MinScore).HasColumnName("Min_score");

			entity.Property(e => e.SubjectId).HasColumnName("subject_id");

			entity.Property(e => e.TestDate)
				.HasColumnType("datetime")
				.HasColumnName("test_date");

			entity.Property(e => e.TestName)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("test_name");

			entity.Property(e => e.TestTime).HasColumnName("test_time");
		});

		modelBuilder.Entity<Tutor>(entity =>
		{
			entity.ToTable("Tutor");

			entity.Property(e => e.TutorId).HasColumnName("tutor_id");

			entity.Property(e => e.Fio)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("FIO");

			entity.Property(e => e.LogIn)
				.IsRequired()
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("log_in");

			entity.Property(e => e.PassWord)
				.IsRequired()
				.HasMaxLength(50)
				.IsUnicode(false)
				.HasColumnName("pass_word");

			entity.Property(e => e.TutorRole)
				.IsRequired()
				.HasMaxLength(10)
				.IsUnicode(false)
				.HasColumnName("tutor_role");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}