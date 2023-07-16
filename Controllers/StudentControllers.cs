using Microsoft.AspNetCore.Mvc;
using schoolManagement.Models;
namespace schoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentControllers
    {
        Student s = new Student();
        [HttpGet]
        public List<Student> getstudent()
        {
            return s.getStudents();
        }
        [HttpPost]
        public ActionResult<bool> poststudent(Student student) {
        return s.addStudent(student);
        }
        [HttpGet("classId")]
        public ActionResult<List<Student>> getStudentOfClass(int classId) {
            return s.getStudentOfClass(classId);
        }
        [HttpGet("subjectId")]
        public ActionResult<Dictionary<string, string>> getGradeOfStudentOfSubject(int subjectId,int studentId)
        {
            return s.getGradeOfStudentOfSubject(subjectId,studentId);
        }
        [HttpPut("sudentId")]
        public bool modifyDetailsOfStudent(int studentId, [FromBody]Student s)
        {
            return s.modifyDetailsOfStudent(studentId,s);
        }
        [HttpPut]
        public bool ChangeClassOfStudent([FromBody]Dictionary<string, string> infos)
        {
            return s.ChangeClassOfStudent(infos);
        }
        [HttpPut("upd")]
        public bool updateGrateOfStudent([FromBody] Dictionary<string, string> infos)
        {
            return s.updateGrateOfStudent(infos);
        }
        [HttpDelete]
        public bool removeStudentFromClass(Dictionary<string, string> infos)
        {
            return s.removeStudentFromClass(infos);
        }
    }
}
