using System.Collections.Generic;
using System.Linq;
using GigHub.Models;

namespace GigHub.ViewModels {
    public class GigsViewModel {
        public IEnumerable<Gig> Gigs { get; set; }
        public bool ShowAction { get; set; }
        public string Heading { get; set; }
        public string SearchTerm { get; set; }
        public ILookup<int, Attendance> Attendances { get; set; }
    }
}