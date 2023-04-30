using AutoMapper;
using Innoloft_Backend.DTO;
using Innoloft_Backend.Helpers;
using Innoloft_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Innoloft_Backend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {

        private readonly IConfiguration _config;
        private readonly EventsContext _context;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public UsersController(IConfiguration config, EventsContext context, ILogger<UsersController> logger, IMapper mapper) {
            _config = config;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("Participate")]
        public async Task<ActionResult> Participate(EventUserDto eventUser) {

            
            var user = await _context.Users.FindAsync(eventUser.UserId);
            var evnt = await _context.Events.FindAsync(eventUser.EventId);
            if (user == null || evnt == null) {
                return BadRequest();
            }
            if (user.ParticipatingEvents.Contains(evnt)) {
                return BadRequest("already participating");
            }

            evnt.ParticipatingUsers.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Invite")]
        public async Task<ActionResult> Invite(InviteUsersDto inviteUsers) {

            var evnt = await _context.Events
                .Include(x=>x.ParticipatingUsers)
                .Include(x => x.InvitedUsers)
                .FirstOrDefaultAsync(x => x.Id == inviteUsers.EventId);


            if (evnt ==null) {
                return BadRequest();
            }

            var errorCount = 0; 
            foreach (var user in inviteUsers.UserIds) {
                var usrExist = await _context.Users.FindAsync(user);
                if (usrExist == null) {
                    errorCount++;
                    continue;
                }
                if (evnt.ParticipatingUsers.Any(x=>x.Id==user)|| evnt.InvitedUsers.Any(x => x.Id == user)) {
                    errorCount++;
                    continue;
                } else {
                    evnt.InvitedUsers.Add(usrExist);
                }
            }
          
            await _context.SaveChangesAsync();
            if (errorCount > 0) {
                return Ok($"{errorCount} users couldn't be invited");
            }
            return Ok();
        }

        [HttpGet("participating/{eventId}")]
        public async Task<ActionResult<List<UserInListDto>>> ParticipatingUsers(int eventId) {
            var evnt = await _context.Events.Include(p=>p.ParticipatingUsers).FirstOrDefaultAsync(x => x.Id == eventId);

            if (evnt == null) {
                return BadRequest();
            }

            
            return Ok(_mapper.Map<List<UserInListDto>>(evnt.ParticipatingUsers));

        }

        [HttpGet("Invited/{eventId}")]
        public async Task<ActionResult<List<UserInListDto>>> InvitedUsers(int eventId) {
            var evnt = await _context.Events.Include(p => p.InvitedUsers).FirstOrDefaultAsync(x => x.Id == eventId);

            if (evnt == null) {
                return BadRequest();
            }


            return Ok(_mapper.Map<List<UserInListDto>>(evnt.InvitedUsers));

        }




    }
}
