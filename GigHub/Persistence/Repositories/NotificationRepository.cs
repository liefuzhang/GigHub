using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories {
    public class NotificationRepository : INotificationRepository {
        private readonly IApplicationDbContext _context;

        public NotificationRepository(IApplicationDbContext context) {
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