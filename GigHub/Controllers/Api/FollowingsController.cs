using System.Linq;
using System.Web.Http;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api {
    [Authorize]
    public class FollowingsController : ApiController {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        protected override void Dispose(bool disposing) {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowDto dto) {
            var userId = User.Identity.GetUserId();

            if (_unitOfWork.Followings.GetFollowing(userId, dto.ArtistId) != null) {
                return BadRequest("You've already followed this artist.");
            }

            var follow = new Following {
                FolloweeId = dto.ArtistId,
                FollowerId = userId
            };

            _unitOfWork.Followings.Add(follow);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id) {
            var userId = User.Identity.GetUserId();

            var following = _unitOfWork.Followings.GetFollowing(userId, id);

            if (following == null)
                return BadRequest();

            _unitOfWork.Followings.Remove(following);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
