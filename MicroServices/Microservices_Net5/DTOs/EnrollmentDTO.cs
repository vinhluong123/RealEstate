using Microservices_Net5.Models;

namespace Microservices_Net5.DTOs
{
    public class EnrollmentDTO
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        public CourseDTO CourseModel { get; set; }
        public StudentDTO StudentModel { get; set; }
    }
}
