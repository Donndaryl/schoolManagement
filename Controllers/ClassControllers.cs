using Microsoft.AspNetCore.Mvc;
using schoolManagement.Models;
namespace schoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassControllers
    {
        ClassR classObj = new ClassR();
        [HttpPost]
        public ActionResult<bool> addClass(ClassR c)
        {
            return classObj.addClass(c);
        }
        [HttpGet]
        public ActionResult<List<ClassR>> getClasses()
        {
            return classObj.getAllClass();
        }
        [HttpPut("id")]
        public bool updateClass(int id, ClassR c)
        {
            return classObj.updateClass(id, c);
        }
        [HttpDelete("id")]
        public bool deleteClass(int id)
        {
            return classObj.deleteClass(id);
        }
        [HttpGet("schoolId")]
        public ActionResult<List<ClassR>> getClassesofSchool(int schoolId)
        {
            return classObj.getClassOfSchool(schoolId);
        }
    }
}
