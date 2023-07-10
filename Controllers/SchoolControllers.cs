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
    }
}
