﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories {
    public class GigRepository : IGigRepository {
        private readonly IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context) {
            _context = context;
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId) {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now && a.Gig.IsCanceled == false)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigWithAttendees(int gigId) {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGigWithArtist(int gigId) {
            return _context.Gigs
                .Include(g => g.Artist)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public void Add(Gig gig) {
            _context.Gigs.Add(gig);
        }

        public IEnumerable<Gig > GetUpcomingGigsByArtist(string artistId) {
            return _context.Gigs
                .Include(g => g.Genre)
                .Where(g => g.ArtistId == artistId
                            && g.DateTime > DateTime.Now
                            && g.IsCanceled == false)
                .ToList();
        }

        public IEnumerable<Gig> GetUpcomingGigs() {
            return _context.Gigs
                .Include(g => g.Genre)
                .Include(g => g.Artist)
                .Where(g => g.DateTime > DateTime.Now
                            && g.IsCanceled == false)
                .ToList();
        }
    }
}