using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories {
    public class FollwingRepository : IFollwingRepository {
        private readonly ApplicationDbContext _context;

        public FollwingRepository(ApplicationDbContext context) {
            _context = context;
        }

        public Following GetFollowing(string userId, string artistId) {
            return _context.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == artistId);
        }

        public void Add(Following follow) {
            _context.Followings.Add(follow);
        }

        public void Remove(Following following) {
            _context.Followings.Remove(following);
        }
    }
}