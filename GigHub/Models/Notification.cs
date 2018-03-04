﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models {
    public class Notification {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType NotificationType { get; private set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }

        [Required]
        public Gig Gig { get; private set; }

        protected Notification() {

        }

        public Notification(Gig gig, NotificationType type) {
            if (gig == null) {
                throw new ArgumentNullException(nameof(gig));
            }
            
            Gig = gig;
            NotificationType = type;
            DateTime = DateTime.Now;
        }
    }
}