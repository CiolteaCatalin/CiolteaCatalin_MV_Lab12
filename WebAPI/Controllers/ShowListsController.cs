using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowListsController : ControllerBase
    {
        private readonly WebAPIContext _context;

        public ShowListsController(WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/ShowLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowList>>> GetShowList()
        {
            return await _context.ShowList.ToListAsync();
        }

        // GET: api/ShowLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShowList>> GetShowList(int id)
        {
            var showList = await _context.ShowList.FindAsync(id);

            if (showList == null)
            {
                return NotFound();
            }

            return showList;
        }

        // PUT: api/ShowLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShowList(int id, ShowList showList)
        {
            if (id != showList.ID)
            {
                return BadRequest();
            }

            _context.Entry(showList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowListExists(id))
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

        // POST: api/ShowLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShowList>> PostShowList(ShowList showList)
        {
            _context.ShowList.Add(showList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShowList", new { id = showList.ID }, showList);
        }

        // DELETE: api/ShowLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShowList(int id)
        {
            var showList = await _context.ShowList.FindAsync(id);
            if (showList == null)
            {
                return NotFound();
            }

            _context.ShowList.Remove(showList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShowListExists(int id)
        {
            return _context.ShowList.Any(e => e.ID == id);
        }
    }
}
