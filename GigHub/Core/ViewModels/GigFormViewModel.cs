using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GigHub.Controllers;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels {
    public class GigFormViewModel {
        [Required]
        public string Venue { get; set; }

        public int Id { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Heading => Id != 0 ? "Edit a Gig" : "Create a Gig";

        public string Action => Id != 0 ? nameof(GigsController.Update) : nameof(GigsController.Create);

        public DateTime GetDateTime() {
            return DateTime.Parse($"{Date} {Time}");
        }
    }
}