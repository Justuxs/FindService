using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindService.EF;
using FindService.EF.Context;
using FindService.Services.CityService;
using FindService.Dto;

namespace FindService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AdvertisementService _advertisementService;

        public AdvertisementsController(ApplicationDbContext context, AdvertisementService advertisementService)
        {
            _context = context;
            _advertisementService=advertisementService;
        }

        // GET: api/Advertisements
        [HttpGet]
        public async Task<ActionResult<APIResponse<List<AdvertisementDtoResponse>>>> GetAdvertisements()
        {
            var response = await _advertisementService.GetAdvertisements();
            return Ok(response);
        }


        // GET: api/Advertisements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse<AdvertisementDtoResponse>>> GetAdvertisement(Guid id)
        {
            var response = await _advertisementService.GetAdvertisementByIdAsync(id);
            return Ok(response);
        }

        // PUT: api/Advertisements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdvertisement(Guid id, Advertisement advertisement)
        {
            if (id != advertisement.Id)
            {
                return BadRequest();
            }

            _context.Entry(advertisement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertisementExists(id))
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

        // POST: api/Advertisements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Advertisement>> PostAdvertisement(Advertisement advertisement)
        {
            _context.Advertisements.Add(advertisement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdvertisement", new { id = advertisement.Id }, advertisement);
        }

        // DELETE: api/Advertisements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdvertisement(Guid id)
        {
            var advertisement = await _context.Advertisements.FindAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }

            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdvertisementExists(Guid id)
        {
            return _context.Advertisements.Any(e => e.Id == id);
        }
    }
}
