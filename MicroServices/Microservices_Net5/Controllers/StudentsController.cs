using Microservices_Net5.DTOs;
using Microservices_Net5.Helper;
using Microservices_Net5.Models;
using Microservices_Net5.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices_Net5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRespository;

        public StudentsController(IStudentRepository studentRespository)
        {
            _studentRespository = studentRespository;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
        {
            // Get user infor as token provided in API header
            var Id = this.User.GetId();
            var Claims = this.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);

            try
            {
                var students = await _studentRespository.GetStudent(id);
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetALl()
        {
            try
            {
                var students = await _studentRespository.GetStudent();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        /// <summary>
        /// Add comment
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
        {
            if (student.ID == 1)
            {
                return null;
            }
            return student;
        }
    }
}
