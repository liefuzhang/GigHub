﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace GigHub.Controllers {
    public class GigsController : Controller {
        private readonly ApplicationDbContext _context;

        public GigsController() {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        [Authorize]
        public ActionResult Mine() {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Include(g => g.Genre)
                .Where(g => g.ArtistId == userId
                    && g.DateTime > DateTime.Now
                    && g.IsCanceled == false)
                .ToList();

            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending() {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var attendances = _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel {
                Gigs = gigs,
                ShowAction = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = attendances
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Create() {
            var viewModel = new GigFormViewModel {
                Genres = _context.Genres.ToList(),
            };

            return View("GigForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public ActionResult Create(GigFormViewModel viewModel) {
            if (!ModelState.IsValid) {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = new Gig {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int id) {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var viewModel = new GigFormViewModel {
                Id = id,
                Genres = _context.Genres.ToList(),
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
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Update(gig.DateTime, gig.Venue, gig.GenreId);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel) {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        public ActionResult Details(int id) {
            var gig = _context.Gigs
                .Include(g => g.Artist)
                .Single(g => g.Id == id);

            var following = false;
            var attending = false;

            var isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated) {
                string userId = User.Identity.GetUserId();
                following = _context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == gig.ArtistId);
                attending = _context.Attendances.Any(a => a.GigId == gig.Id && a.AttendeeId == userId);
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