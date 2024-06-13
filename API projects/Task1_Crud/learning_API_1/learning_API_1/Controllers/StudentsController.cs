using learning_API_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace learning_API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class StudentsController : ControllerBase //
    {
        List<Student> students = new List<Student>
        {
            new Student{ Id = 0,Name="Yaman", Bio="this is Yaman"},
            new Student{ Id = 1,Name="Kenan", Bio="this is Kenan"},
            new Student{ Id = 2,Name="Ibrahim", Bio="this is Ibrahim"}
        };

        [HttpGet("getAll")]
        public IActionResult GetAllStudents()
        {
            return Ok(students); // return status code =200 and student list
        }

        [HttpGet("getFirst")]
        public IActionResult GetFirstStudent()
        {
            return Ok(students[0]); // return status code =200 and first student
        }


        [HttpGet("{id}")] // the url is api/students/id_value
        public IActionResult getById(int id)
        {
            var student = students.Find(std => std.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(student);
            }
        }


        [HttpPost]
        public IActionResult addNewStudent(Student request)
        {
            if(request is null)
            {
                return BadRequest();
            }
            else{

                var student = new Student
                {
                    Id = request.Id,
                    Name = request.Name,
                    Bio = request.Bio,
                };
               students.Add(student);
                return Ok(student);
            }


        }

        [HttpPut("{id}")]
        public IActionResult editStudent(int id ,Student request)
        {
            var currentStudent=students.FirstOrDefault(student => student.Id == id);
            if(request is null)
            {
                return NotFound();
            }
            else
            {
                currentStudent.Name = request.Name;
                currentStudent.Bio = request.Bio;
                return Ok(currentStudent);
            }



        }


        [HttpDelete("{id}")]
        public IActionResult deleteStudent(int id)
        {

            var student = students.FirstOrDefault(student => student.Id == id);
            if (student is null)
            {
                return NotFound();
            }
            else
            {
                students.Remove(student);
                return Ok();
            }

            }









        }
}
