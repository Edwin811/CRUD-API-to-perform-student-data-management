using Microsoft.AspNetCore.Mvc;
using percobaan2.Models;
using percobaan2.Helpers;

namespace PercobaanApi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private string __constr;

        public StudentController(IConfiguration configuration)
        {
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }

        [HttpGet]
        public ActionResult<List<Student>> GetAllStudents()
        {
            StudentContext context = new StudentContext(this.__constr);
            List<Student> students = context.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("Pencarian dengan id")]
        public ActionResult<Student> GetStudentById(int id)
        {
            StudentContext context = new StudentContext(__constr);
            Student student = context.GetStudentById(id);
            if (student == null)
            {
                return NotFound(new {message = "Id student tersebut tidak ditemukan"});
            }
            
            return Ok(student);
            
        }

        [HttpGet("Pencarian dengan nama")]
        public ActionResult<Student> SearchStudentByFullName(string nama)
        {
            StudentContext context = new StudentContext(__constr);
            Student student = context.GetStudentByFullName(nama);

            if (student == null)
            {
                return NotFound(new { message = $"Student dengan nama '{nama}' tidak ditemukan." });
            }

            return Ok(student);
        }


        [HttpPost("Menambahkan data student")]
        public IActionResult CreateStudent( string nama, string alamat, string email)
        {
            Student student = new Student()
            {
                nama = nama,
                alamat = alamat,
                email = email
            };

            StudentContext context = new StudentContext(__constr);
            context.AddStudent(student);

            return Ok(new {message = $"Behasil menambahkan data student dengan nama: {nama},alamat: {alamat},email: {email}"});
        }

        [HttpPut("Memperbaharui data student")]
        public IActionResult UpdateStudent(int id, string nama, string alamat, string email)
        {
            Student student = new Student()
            {
                id_student = id,
                nama = nama,
                alamat = alamat,
                email = email
            };

            StudentContext context = new StudentContext(__constr);
            context.UpdateStudent(student);

            Student existingStudent = context.GetStudentById(id);
            if (existingStudent == null)
            {
                return NotFound(new { message = "Student dengan ID tersebut tidak ditemukan." });
            }
            return Ok(new {message = $"Student dengan id {id} berhasil di ubah dengan nama:{nama},alamat: {alamat},email: {email}"});
        }


        [HttpDelete("Menghapus data Student")]
        public IActionResult DeleteStudent(int id)
        {
            StudentContext context = new StudentContext(__constr);

            Student existingStudent = context.GetStudentById(id);
            if (existingStudent == null)
            {
                return NotFound(new { message = "Student dengan ID tersebut tidak ditemukan." });
            }

            context.DeleteStudent(id);

            return Ok(new {message = $"Student dengan ID {id} Berhasil di hapus"});
        }
    }
}