using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok("This is the list of students");
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            return Ok("This is the student with id: " + id);
        }

        [HttpPost]
        public IActionResult AddStudent()
        {
            return Ok("Student added");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id)
        {
            return Ok("Student updated with id: " + id);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Student deleted with id: " + id);
        }
    }
}