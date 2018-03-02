﻿using System;
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
    public class FollowingsController : ApiController {
        private readonly ApplicationDbContext _context;

        public FollowingsController() {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowDto dto) {
            var userId = User.Identity.GetUserId();

            if (_context.Follows.Any(
                    f => f.FolloweeId == dto.ArtistId
                     && f.FollowerId == userId)) {
                return BadRequest("You've already followed this artist.");
            }

            var follow = new Following {
                FolloweeId = dto.ArtistId,
                FollowerId = userId
            };

            _context.Follows.Add(follow);
            _context.SaveChanges();

            return Ok();
        }
    }
}