using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("courses")]
    public class CourseController : ControllerBase
    {

        private readonly List<Course> _courses = new List<Course>
        {
            new Course { ID = 1, Name = "Course 1", Modules = new List<Module>() },
            new Course { ID = 2, Name = "Course 2", Modules = new List<Module>() },
            new Course { ID = 3, Name = "Course 3", Modules = new List<Module>() }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Course>> Get()
        {
            return Ok(_courses);
        }

        [HttpGet("{id}")]
        public ActionResult<Course> Get(int id)
        {
            var course = _courses.Find(c => c.ID == id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        public ActionResult<Course> Post(Course course)
        {
            _courses.Add(course);
            return CreatedAtAction(nameof(Get), new { id = course.ID }, course);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Course course)
        {
            var existingCourse = _courses.Find(c => c.ID == id);

            if (existingCourse == null)
            {
                return NotFound();
            }

            existingCourse.Name = course.Name;
            existingCourse.Modules = course.Modules;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var course = _courses.Find(c => c.ID == id);

            if (course == null)
            {
                return NotFound();
            }

            _courses.Remove(course);

            return NoContent();
        }

        
    }
}