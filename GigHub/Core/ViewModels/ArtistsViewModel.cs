using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels {
    public class ArtistsViewModel {
        public IEnumerable<ApplicationUser> Artists { get; set; }
        public string Heading { get; set; }
        public bool ShowAction { get; set; }
    }
}