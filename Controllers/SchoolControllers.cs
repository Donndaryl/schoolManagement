using schoolManagement.Models;
using Microsoft.AspNetCore.Mvc;
namespace schoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolControllers: ControllerBase
    {
        School s = new School();
        [HttpGet]
        public ActionResult<List<School>> GetAllSchool() 
        {
            return s.getAllSchool();
        }
        [HttpPost]
        public ActionResult<bool> addSchool(School school)
        {
            return s.addSchool(school);
        }
        [HttpGet("id")]
        public ActionResult<List<School>> GetSchollById(int id) {
            return s.getAllSchoolById(id);
        }
        [HttpPut("id")]
        public ActionResult<bool> updateSchool([FromBody] School school,int id) 
        {
            return s.updateSchool(school,id);
        }
        [HttpDelete("id")]
        public bool deleteSchoolById(int id)
        {
            return s.deleteSchool(id);
        }
    }
}
