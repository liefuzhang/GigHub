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
        public ActionResult Index() {
            var userId = User.Identity.GetUserId();
            var artists = _context.Users.Where(a => a.Id != userId);

            return View(artists);
        }
    }
}