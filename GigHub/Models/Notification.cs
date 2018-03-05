using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models {
    public class Notification {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType NotificationType { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        protected Notification() {
        }

        private Notification(Gig gig, NotificationType type) {
            if (gig == null) {
                throw new ArgumentNullException(nameof(gig));
            }

            Gig = gig;
            NotificationType = type;
            DateTime = DateTime.Now;
        }

        // factory method
        public static Notification GigCanceled(Gig gig) {
            return new Notification(gig, NotificationType.GigCanceled);
        }

        public static Notification GigCreated(Gig gig) {
            return new Notification(gig, NotificationType.GigCreated);
        }

        // ensure GigUpdated notification always has originalDateTime and originalVenue
        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue) {
            var notification = new Notification(newGig, NotificationType.GigUpdated) {
                OriginalDateTime = originalDateTime,
                OriginalVenue = originalVenue
            };

            return notification;
        }
    }
}