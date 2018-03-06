using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories {
    public interface IGigRepository {
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
        Gig GetGigWithArtist(int gigId);
        void Add(Gig gig);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId);
        IEnumerable<Gig> GetUpcomingGigs();
    }
}