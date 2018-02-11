using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_project.Models;

namespace dotnet_project.Controllers
{
    [Produces("application/json")]
    [Route("api/CodeLouisvilleStudents")]
    public class CodeLouisvilleStudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CodeLouisvilleStudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CodeLouisvilleStudents
        [HttpGet]
        public IEnumerable<CodeLouisvilleStudent> GetCodeLouisvileStudents()
        {
            return _context.CodeLouisvileStudents;
        }

        // GET: api/CodeLouisvilleStudents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCodeLouisvilleStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var codeLouisvilleStudent = await _context.CodeLouisvileStudents.SingleOrDefaultAsync(m => m.Id == id);

            if (codeLouisvilleStudent == null)
            {
                return NotFound();
            }

            return Ok(codeLouisvilleStudent);
        }

        // PUT: api/CodeLouisvilleStudents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCodeLouisvilleStudent([FromRoute] int id, [FromBody] CodeLouisvilleStudent codeLouisvilleStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != codeLouisvilleStudent.Id)
            {
                return BadRequest();
            }

            _context.Entry(codeLouisvilleStudent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CodeLouisvilleStudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CodeLouisvilleStudents
        [HttpPost]
        public async Task<IActionResult> PostCodeLouisvilleStudent([FromBody] CodeLouisvilleStudent codeLouisvilleStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CodeLouisvileStudents.Add(codeLouisvilleStudent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCodeLouisvilleStudent", new { id = codeLouisvilleStudent.Id }, codeLouisvilleStudent);
        }

        // DELETE: api/CodeLouisvilleStudents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCodeLouisvilleStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var codeLouisvilleStudent = await _context.CodeLouisvileStudents.SingleOrDefaultAsync(m => m.Id == id);
            if (codeLouisvilleStudent == null)
            {
                return NotFound();
            }

            _context.CodeLouisvileStudents.Remove(codeLouisvilleStudent);
            await _context.SaveChangesAsync();

            return Ok(codeLouisvilleStudent);
        }

        private bool CodeLouisvilleStudentExists(int id)
        {
            return _context.CodeLouisvileStudents.Any(e => e.Id == id);
        }
    }
}