using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Persistence;

namespace GigHub.Controllers {
    public class GigsController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        protected override void Dispose(bool disposing) {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        [Authorize]
        public ActionResult Mine() {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(userId);

            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending() {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel {
                Gigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
                ShowAction = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Create() {
            var viewModel = new GigFormViewModel {
                Genres = _unitOfWork.Genres.GetGenres()
            };

            return View("GigForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public ActionResult Create(GigFormViewModel viewModel) {
            if (!ModelState.IsValid) {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _unitOfWork.Gigs.Add(gig);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }


        [Authorize]
        public ActionResult Edit(int id) {
            var gig = _unitOfWork.Gigs.GetGigWithArtist(id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId()) {
                return new HttpUnauthorizedResult();
            }

            var viewModel = new GigFormViewModel {
                Id = id,
                Genres = _unitOfWork.Genres.GetGenres(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };

            return View("GigForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public ActionResult Update(GigFormViewModel viewModel) {
            if (!ModelState.IsValid) {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId()) {
                return new HttpUnauthorizedResult();
            }

            gig.Update(gig.DateTime, gig.Venue, gig.GenreId);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel) {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        public ActionResult Details(int id) {
            var gig = _unitOfWork.Gigs.GetGigWithArtist(id);

            if (gig == null) {
                return HttpNotFound();
            }

            var following = false;
            var attending = false;

            var isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated) {
                string userId = User.Identity.GetUserId();
                following = _unitOfWork.Followings.GetFollowing(userId, gig.ArtistId) != null;
                attending = _unitOfWork.Attendances.GetAttendance(gig.Id, userId) != null;
            }

            var viewModel = new GigDetailsViewModel {
                Gig = gig,
                ShowAction = isAuthenticated,
                IsFollowing = following,
                IsAttending = attending
            };

            return View(viewModel);
        }
    }
}