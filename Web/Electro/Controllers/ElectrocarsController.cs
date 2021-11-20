using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Electro.Models;
using Microsoft.AspNetCore.Identity;

namespace Electro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectrocarsController : ControllerBase
    {
        private readonly UserManager<User> _manager;
        private readonly ElectroDbContext _context;

        public ElectrocarsController(UserManager<User> userMgr, ElectroDbContext context)
        {
            _manager = userMgr;
            _context = context;
        }

        // GET: api/Electrocars
        [HttpGet]
        public async Task<ActionResult> GetElectrocars()
        {
            User user = await _manager.FindByNameAsync(User.Identity.Name);

            return Ok(_context.Electrocars.Where(e => e.OwnerId == user.Id));
        }

        // GET: api/Electrocars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Electrocar>> GetElectrocar(string id)
        {
            var electrocar = await _context.Electrocars.FindAsync(id);

            if (electrocar == null)
            {
                return NotFound();
            }

            return electrocar;
        }

        // PUT: api/Electrocars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElectrocar(string id, Electrocar electrocar)
        {
            if (id != electrocar.VinCode)
            {
                return BadRequest();
            }

            _context.Entry(electrocar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectrocarExists(id))
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

        // POST: api/Electrocars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Electrocar>> PostElectrocar(Electrocar electrocar)
        {
            User user = await _manager.FindByNameAsync(User.Identity.Name);

            electrocar.OwnerId = user.Id;

            _context.Electrocars.Add(electrocar);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ElectrocarExists(electrocar.VinCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetElectrocar", new { id = electrocar.VinCode }, electrocar);
        }

        // DELETE: api/Electrocars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElectrocar(string id)
        {
            var electrocar = await _context.Electrocars.FindAsync(id);
            if (electrocar == null)
            {
                return NotFound();
            }

            _context.Electrocars.Remove(electrocar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElectrocarExists(string id)
        {
            return _context.Electrocars.Any(e => e.VinCode == id);
        }
    }
}
