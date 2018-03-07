using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories {
    public class UserNotificationRepository : IUserNotificationRepository {
        private readonly ApplicationDbContext _context;

        public UserNotificationRepository(ApplicationDbContext context) {
            _context = context;
        }

        public IEnumerable<UserNotification> GetNewUserNotificationByUserId(string userId) {
            var userNotifications = _context.UserNotifications
                .Where(un => un.UserId == userId && un.IsRead == false)
                .ToList();

            return userNotifications;
        }
    }

}