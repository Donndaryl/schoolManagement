using Microsoft.AspNetCore.Mvc;
using schoolManagement.Models;

namespace schoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectControllers
    {
        Subject s = new Subject();
        [HttpGet]
        public List<Subject> getSubject()
        {
            return s.getSubject();
        }

        [HttpPost]
        public ActionResult<bool> postSubject(Subject subject)
        {
          return  s.addSubject(subject);
        }
        [HttpGet("classId")]
        public List<Subject> getSubjectOfClass(int classId) 
            {
                return s.getSubjectOfClass(classId);
            }
        [HttpPut]
        public bool upadteSubject([FromBody]Subject s)
        {
            return s.upadteSubject(s);
        }
        [HttpDelete]
        public bool deleteSubject(int subjectId)
        {
            return s.deleteSubject(subjectId);
        }
    }
}
    