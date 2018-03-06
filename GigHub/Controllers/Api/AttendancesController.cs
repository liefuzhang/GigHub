using System.Linq;
using System.Web.Http;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api {
    [Authorize]
    public class AttendancesController : ApiController {
        private readonly ApplicationDbContext _context;

        public AttendancesController() {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto) {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(
                    a => a.GigId == dto.GigId
                     && a.AttendeeId == userId)) {
                return BadRequest("The attendance already exists.");
            }

            var attendance = new Attendance {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id) {
            var userId = User.Identity.GetUserId();
            var attendance = _context.Attendances
                .SingleOrDefault(a => a.GigId == id && a.AttendeeId == userId);

            if (attendance == null)
                return NotFound();

            _context.Attendances.Remove(attendance);
            _context.SaveChanges();

            return Ok(id);
        }
    }
}
