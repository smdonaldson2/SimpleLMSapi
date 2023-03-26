using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    private readonly List<Assignment> _assignments = new List<Assignment>();

    // GET api/assignments
    [HttpGet]
    public ActionResult<IEnumerable<Assignment>> Get()
    {
        return _assignments;
    }

    // GET api/assignments/5
    [HttpGet("{id}")]
    public ActionResult<Assignment> Get(int id)
    {
        var assignment = _assignments.FirstOrDefault(a => a.ID == id);
        if (assignment == null)
        {
            return NotFound();
        }

        return assignment;
    }

    // POST api/assignments
    [HttpPost]
    public ActionResult<Assignment> Post(Assignment assignment)
    {
        assignment.ID = _assignments.Count + 1;
        _assignments.Add(assignment);

        return CreatedAtAction(nameof(Get), new { id = assignment.ID }, assignment);
    }

    // PUT api/assignments/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, Assignment updatedAssignment)
    {
        var assignment = _assignments.FirstOrDefault(a => a.ID == id);
        if (assignment == null)
        {
            return NotFound();
        }

        assignment.Name = updatedAssignment.Name;
        assignment.Grade = updatedAssignment.Grade;
        assignment.DueDate = updatedAssignment.DueDate;

        return NoContent();
    }

    // DELETE api/assignments/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var assignment = _assignments.FirstOrDefault(a => a.ID == id);
        if (assignment == null)
        {
            return NotFound();
        }

        _assignments.Remove(assignment);

        return NoContent();
    }
}