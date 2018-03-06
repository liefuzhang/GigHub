using System.Linq;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api {
    [Authorize]
    public class FollowingsController : ApiController {
        private readonly ApplicationDbContext _context;

        public FollowingsController() {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowDto dto) {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(
                    f => f.FolloweeId == dto.ArtistId
                     && f.FollowerId == userId)) {
                return BadRequest("You've already followed this artist.");
            }

            var follow = new Following {
                FolloweeId = dto.ArtistId,
                FollowerId = userId
            };

            _context.Followings.Add(follow);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id) {
            var userId = User.Identity.GetUserId();

            var following = _context.Followings
                .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);

            if (following == null)
                return BadRequest();

            _context.Followings.Remove(following);
            _context.SaveChanges();

            return Ok(id);
        }
    }
}
