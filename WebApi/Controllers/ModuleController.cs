using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

//using WebApi.Repositories;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("modules")]
    public class ModuleController : ControllerBase
    {
        private readonly List<Module> _modules = new List<Module>
        {
            new Module { ID = 1, Name = "Module 1", Assignments = new List<Assignment>()},
            new Module { ID = 2, Name = "Module 2", Assignments = new List<Assignment>()},
            new Module { ID = 3, Name = "Module 3", Assignments = new List<Assignment>()}
        };

        [HttpGet]
        public ActionResult<IEnumerable<Module>> Get()
        {
            return Ok(_modules);
        }

        [HttpGet("{id}")]
        public ActionResult<Module> Get(int id)
        {
            var module = _modules.Find(m => m.ID == id);

            if (module == null)
            {
                return NotFound();
            }

            return Ok(module);
        }

        [HttpPost]
        public ActionResult<Module> Post(Module module)
        {
            _modules.Add(module);
            return CreatedAtAction(nameof(Get),new {id = module.ID}, module);
        }

        [HttpPut("{id}")]
        public ActionResult Put (int id, Module module)
        {
            var existingModule = _modules.Find(m => m.ID == id);

            if (existingModule == null)
            {
                return NotFound();
            }

            existingModule.Name = module.Name;
            existingModule.Assignments = module.Assignments;

            return NoContent();
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var module = _modules.Find(m => m.ID == id);

            if (module == null)
            {
                return NotFound();
            }

            _modules.Remove(module);

            return NoContent();
        }

        
    }
}