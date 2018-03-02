using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.ViewModels {
    public class ArtistsViewModel {
        public IEnumerable<ApplicationUser> Artists { get; set; }
        public string Heading { get; set; }
        public bool ShowAction { get; set; }
    }
}