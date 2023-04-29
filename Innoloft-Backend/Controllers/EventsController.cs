using AutoMapper;
using Innoloft_Backend.DTO;
using Innoloft_Backend.Helpers;
using Innoloft_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Innoloft_Backend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase {

        private readonly IConfiguration _config;
        private readonly EventsContext _context;
        private readonly ILogger<EventsController> _logger;
        private readonly IMapper _mapper;



        public EventsController(IConfiguration config, EventsContext context, ILogger<EventsController> logger, IMapper mapper) {
            _config = config;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<EventInListDto>>> GetAllEvents(int pageNumber = 1, int pageSize = 10) {

            if (_context.Events == null) {
                return BadRequest();
            }

            var allEvents = _context.Events.AsNoTracking()
                .Select(x => new EventInListDto {
                    Address = x.Address,
                    Description = x.Description,
                    EndTime = x.EndTime,
                    Id = x.Id,
                    IsOnline = x.IsOnline,
                    Name = x.Name,
                    StartTime = x.StartTime
                })
                .OrderBy(x => x.Id);

            return Ok(await PaginatedList<EventInListDto>.CreateAsync(allEvents, pageNumber, pageSize));

        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<PaginatedList<EventInListDto>>> GetAllEvents(int userId, int pageNumber = 1, int pageSize = 10) {

            if (_context.Events == null) {
                return BadRequest();
            }

            var allEvents = _context.Events.AsNoTracking()
                .Where(u => u.UserId == userId)
                .Select(x => new EventInListDto {
                    Address = x.Address,
                    Description = x.Description,
                    EndTime = x.EndTime,
                    Id = x.Id,
                    IsOnline = x.IsOnline,
                    Name = x.Name,
                    StartTime = x.StartTime,
                })
                .OrderBy(x => x.Id);

            return Ok(await PaginatedList<EventInListDto>.CreateAsync(allEvents, pageNumber, pageSize));

        }




        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEventDetails(int id) {

            var eventDetails = await _context.Events.AsNoTracking()
                .Include(x => x.User)
                .Include(p => p.ParticipatingUsers)
                .Include(i => i.InvitedUsers)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (eventDetails == null) {
                return NotFound();
            }

            return Ok(_mapper.Map<EventDto>(eventDetails));
        }


        [HttpPost]
        //[Authorize]
        public async Task<ActionResult> PostEvent(EventPostDto eventDto) {
            if (_context.Events == null) {
                return BadRequest();
            }

            //we should get User ID from token 
            //var uid = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var eventPost = _mapper.Map<Event>(eventDto);

            if (eventPost.StartTime > eventPost.EndTime) {
                return BadRequest("EndTime must be after the StartTime");
            }

            _context.Events.Add(eventPost);
            await _context.SaveChangesAsync();

            return Ok(eventPost.Id);
        }

        [HttpPut("{id}")]
        //[Authorize]
        public async Task<ActionResult> PutEvent(int id, EventPutDto eventDto) {

            if (eventDto.Id != id) {
                return BadRequest();
            }

            if (_context.Events == null) {
                return BadRequest();
            }

            //we should get User ID from token 
            //var uid = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var eventPut = _mapper.Map<Event>(eventDto);

            if (eventPut.StartTime > eventPut.EndTime) {
                return BadRequest("EndTime must be after the StartTime");
            }

            var eventFromDb = await _context.Events.FindAsync(id);
            

            foreach (var item in typeof(Event).GetProperties()) {
                if (item.GetValue(eventPut) is not null) {

                    try {
                        item.SetValue(eventFromDb, item.GetValue(eventPut));

                    } catch (Exception e) {
                        _logger.LogError(e.Message);

                    }
                }
            }


            _context.Events.Update(eventFromDb);
            await _context.SaveChangesAsync();

            return Ok(eventPut.Id);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteEvent(int id) {
            var eventToDelete = await _context.Events.FindAsync(id);
            if (eventToDelete == null) {
                return NotFound();
            }
            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();
            return Ok(eventToDelete.Id);
        }

    }
}
