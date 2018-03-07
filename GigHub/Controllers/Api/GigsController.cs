using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Web.Http.Results;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Persistence;

namespace GigHub.Controllers.Api {
    [System.Web.Http.Authorize]
    public class GigsController : ApiController {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        protected override void Dispose(bool disposing) {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult Cancel(int id) {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);

            if (gig == null || gig.IsCanceled)
                return NotFound();

            if (gig.ArtistId != userId)
                return Unauthorized();
            
            gig.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
