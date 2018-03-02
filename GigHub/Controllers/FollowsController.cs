using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers {
    [Authorize]
    public class FollowsController : ApiController {
        private readonly ApplicationDbContext _context;

        public FollowsController() {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowDto dto) {
            var userId = User.Identity.GetUserId();

            if (_context.Follows.Any(
                    f => f.ArtistId == dto.ArtistId
                     && f.UserId == userId)) {
                return BadRequest("You've already followed this artist.");
            }

            var follow = new Follow {
                ArtistId = dto.ArtistId,
                UserId = userId
            };

            _context.Follows.Add(follow);
            _context.SaveChanges();

            return Ok();
        }
    }
}
