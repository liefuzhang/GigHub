using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Core.Models {
    public class UserNotification {
        public ApplicationUser User { get; private set; }
        public Notification Notification { get; private set; }

        // only to be used by EF
        protected UserNotification() {
        }

        public UserNotification(ApplicationUser user, Notification notification) {
            if (user == null) {
                throw new ArgumentNullException(nameof(user));
            }

            if (notification == null) {
                throw new ArgumentNullException(nameof(notification));
            }

            User = user;
            Notification = notification;
            UserId = user.Id;
            NotificationId = notification.Id;
        }

        public string UserId { get; private set; }

        public int NotificationId { get; private set; }

        public bool IsRead { get; private set; }

        public void Read() {
            IsRead = true;
        }
    }
}