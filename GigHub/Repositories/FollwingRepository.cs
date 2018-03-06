using System.Linq;
using GigHub.Models;

namespace GigHub.Repositories {
    public class FollwingRepository {
        private readonly ApplicationDbContext _context;

        public FollwingRepository(ApplicationDbContext context) {
            _context = context;
        }

        public Following GetFollowing(string userId, string artistId) {
            return _context.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == artistId);
        }
    }
}