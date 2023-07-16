using Microsoft.AspNetCore.Mvc;
using schoolManagement.Models;

namespace schoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseNoteControllers
    {

        CourseNote c = new CourseNote();
        [HttpGet]
        public List<CourseNote> getCourseNote()
        {
            return c.GetCourseNotes();
        }

        [HttpPost]
        public ActionResult<bool> postCourseNote(CourseNote courseN)
        {
            return c.adcourseNote(courseN);
        }
        [HttpDelete]
        public bool deleteGradeOfStudent([FromBody]Dictionary<string, string> infos)
        {
            return c.deleteGradeOfStudent(infos);
        }
        [HttpDelete("dag")]
        public bool deleteAllGradeOfStudent([FromBody] Dictionary<string, string> infos)
        {
            return c.deleteGradeOfStudent(infos);
        }
    }
}
