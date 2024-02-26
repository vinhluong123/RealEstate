using Microservices_Net5.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices_Net5.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _schoolContext;
        public StudentRepository(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        public async Task<Student> AddStudent(Student student)
        {
            var result = await _schoolContext.AddAsync(student);
            await _schoolContext.SaveChangesAsync();
            return result.Entity;
        }

        public void DeleteStudent(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetStudent()
        {
            return await _schoolContext.Students.ToListAsync();
        }

        public async Task<Student> GetStudent(int id)
        {
            return await _schoolContext.Students.FindAsync(id);
        }

        public Task<Student> UpdateStudent(Student student)
        {
            throw new System.NotImplementedException();
        }
    }
}
