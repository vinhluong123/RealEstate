using System;
using System.Collections.Generic;

namespace Microservices_Net5.DTOs
{
    public class StudentDTO
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        
        public DateTime EnrollmentDate { get; set; }
        public ICollection<EnrollmentDTO> EnrollmentModels { get; set; }
    }
}
