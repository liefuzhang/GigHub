using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories {
    public class NotificationRepository : INotificationRepository {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context) {
            _context = context;
        }

        public IEnumerable<Notification> GetNewNotificationByUserId(string userId) {
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && un.IsRead == false)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            return notifications;
        }
    }

}