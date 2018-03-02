using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.ViewModels {
    public class GigsViewModel {
        public IEnumerable<Gig> Gigs { get; set; }
        public bool ShowAction { get; set; }
        public string Heading { get; set; }
    }
}