using System.Collections.Generic;
using System.Linq;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories {
    public class UserRepository : IUserRepository {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetFolloweesByUserId(string userId) {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();
        }

        public IEnumerable<ApplicationUser> GetUsers() {
            return _context.Users.ToList();
        }
    }
}