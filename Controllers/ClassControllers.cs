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
    }
}
