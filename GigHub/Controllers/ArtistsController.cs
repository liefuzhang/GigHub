using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers {
    public class ArtistsController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public ArtistsController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        protected override void Dispose(bool disposing) {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        [Authorize]
        public ActionResult Followings() {
            var userId = User.Identity.GetUserId();
            var artists = _unitOfWork.Users.GetFolloweesByUserId(userId);

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
            var artists = _unitOfWork.Users.GetUsers().Where(a => a.Id != userId);

            var viewModel = new ArtistsViewModel {
                Artists = artists,
                ShowAction = true,
                Heading = "All Artists"
            };

            return View("Artists", viewModel);
        }
    }
}