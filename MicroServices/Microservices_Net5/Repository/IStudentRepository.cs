using Microservices_Net5.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices_Net5.Repository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudent();
        Task<Student> GetStudent(int id);
        Task<Student> AddStudent(Student student);
        Task<Student> UpdateStudent(Student student);
        void DeleteStudent(int id);
    }
}
