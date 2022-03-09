﻿using Diplom.Domain.Repositories.Abstract;
using Diplom;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Domain.Repositories.EntityFramework
{
    public class EFStudentsRepository : IStudentsRepository
    {
        private test_CursachContext _context;
        public EFStudentsRepository(test_CursachContext context)
        {
            _context = context;
        }
        public void DeleteStudent(int ? id) //добавить проверку на null
        {
            _context.Remove(new Student() { StudentId = id.Value });
            _context.SaveChanges();
        }
        public IQueryable<Student> GetStudents() => _context.Students;
        public Student GetStudentsById(int? id)
        {
            return _context.Students.FirstOrDefault(m => m.StudentId == id);
        }
        public void SaveStudent(Student entity)
        {
            if (entity.StudentId == default) _context.Entry(entity).State = EntityState.Added;
            else _context.Entry<Student>(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
