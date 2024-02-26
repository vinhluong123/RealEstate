using System.Collections.Generic;

namespace Microservices_Net5.DTOs
{
    public class CourseDTO
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<EnrollmentDTO> EnrollmentModels { get; set; }
    }
}
