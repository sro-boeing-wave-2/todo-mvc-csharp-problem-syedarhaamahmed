using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo.Model;
using todo.Models;
using todo.Services;

namespace todo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly INoteService _context;

        public DataController(todoContext context)
        {
            _context = new NoteService(context);
        }

        // GET: api/Data
        [HttpGet]
        public async  Task<IActionResult> GetData()
        {
            var note = await _context.GetAllNotesData();
            return Ok(note);
        }

        // GET: api/Data/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var data = await _context.GetNoteByID(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // Get via mutiple labels

        [HttpGet]
        public async Task<IActionResult> GetMultipleData([FromQuery] string title, [FromQuery] string label, [FromQuery] bool? pinned)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var a = await _context.GetMultipleLabels(title, label, pinned);

            return Ok(a);
        }

        // PUT: api/Data/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutData([FromRoute] int id, [FromBody] Data data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != data.ID)
            {
                return BadRequest();
            }


            try
            {
                await _context.EditNoteData(data);
            }
            catch (NotImplementedException e)
            {
                  return NotFound();                
            }

            return Ok(data);
        }

        // POST: api/Data
        [HttpPost]
        public async Task<IActionResult> PostData([FromBody] Data data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _context.CreateNoteData(data);
            return CreatedAtAction("GetData", new { id = data.ID }, data);
        }

        // DELETE: api/Data/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var data = await _context.DeleteNoteData(id);
                return Ok(data);
            }
            catch (NotImplementedException e)
            {
                return NotFound();
            }
        }

        //// DELETE Multiple notes

        //[HttpDelete("{Title}")]
        //public async Task<IActionResult> DeleteMultipleData([FromRoute] string title)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var data = await _context.Data.Include(a => a.checklist).Include(b => b.label).Where(c => c.Title == title).ToListAsync();
        //    if (data == null)
        //    {
        //        return NotFound();
        //    }
        //    foreach (var notes in data)
        //    {
        //        _context.Data.Remove(notes);
        //    }
        //    await _context.SaveChangesAsync();

        //    return Ok(data);
        //}

        //private bool DataExists(int id)
        //{
        //    return _context.Data.Any(e => e.ID == id);
        //}
    }
}