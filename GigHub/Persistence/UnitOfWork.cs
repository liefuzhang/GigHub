using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Persistence {
    public class UnitOfWork {
        private readonly ApplicationDbContext _context;
        public AttendanceRepository Attendances { get; private set; }
        public GigRepository Gigs { get; private set; }
        public GenreRepository Genres { get; private set; }
        public FollwingRepository Followings { get; private set; }

        public UnitOfWork(ApplicationDbContext context) {
            _context = context;
            Attendances = new AttendanceRepository(_context);
            Gigs = new GigRepository(_context);
            Genres = new GenreRepository(_context);
            Followings = new FollwingRepository(_context);
        }

        public void Complete() {
            _context.SaveChanges();
        }
    }
}