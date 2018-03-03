﻿using System;
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
                .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now)
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

            var viewModel = new GigsViewModel {
                Gigs = gigs,
                ShowAction = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending"
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Create() {
            var viewModel = new GigFormViewModel {
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public ActionResult Create(GigFormViewModel viewModel) {
            if (!ModelState.IsValid) {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
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
    }
}