using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using System.Data.Entity;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers {
    public class HomeController : Controller {
        private readonly ApplicationDbContext _context;

        public HomeController() {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        public ActionResult Index(string query = null) {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && g.IsCanceled == false);

            if (!string.IsNullOrWhiteSpace(query))
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.Name.Contains(query)
                                || g.Genre.Name.Contains(query)
                                || g.Venue.Contains(query));

            var userId = User.Identity.GetUserId();
            var attendances = _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.GigId);


            var viewModel = new GigsViewModel {
                Gigs = upcomingGigs,
                ShowAction = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = attendances
            };

            return View("Gigs", viewModel);
        }
    }
}