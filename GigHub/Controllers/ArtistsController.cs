using System;
using System.Linq;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers {
    public class ArtistsController : Controller {
        private readonly ApplicationDbContext _context;

        public ArtistsController() {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        [Authorize]
        public ActionResult Followings() {
            var userId = User.Identity.GetUserId();
            var artists = _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();

            var viewModel = new ArtistsViewModel {
                Artists = artists,
                ShowAction = false,
                Heading = "Artists I'm Followings"
            };

            return View("Artists", viewModel);
        }

        [Authorize]
        public ActionResult Index() {
            var userId = User.Identity.GetUserId();
            var artists = _context.Users.Where(a => a.Id != userId);

            var viewModel = new ArtistsViewModel {
                Artists = artists,
                ShowAction = true,
                Heading = "All Artists"
            };

            return View("Artists", viewModel);
        }
    }
}