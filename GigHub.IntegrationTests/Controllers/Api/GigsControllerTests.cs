using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.IntegrationTests.Extensions;
using GigHub.Persistence;
using NUnit.Framework;

namespace GigHub.IntegrationTests.Controllers.Api {
    [TestFixture]
    public class GigsControllerTests {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void Setup() {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown() {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Cancel_WhenCalled_ShouldCancelTheGivenGigs() {
            // Arrange
            var users = _context.Users.ToList();
            _controller.MockCurrentUser(users[0].Id, users[0].UserName);

            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig { Artist = users[0], DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            var attendee = new Attendance {
                Attendee = users[1],
                Gig = gig
            };
            
            gig.Attendances.Add(attendee);
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            // Act
            var result = _controller.Cancel(gig.Id);

            var userNotifications = _context.UserNotifications.ToList();
            var notifications = _context.Notifications.ToList();

            // Assert
            _context.Entry(gig).Reload();
            gig.IsCanceled.Should().BeTrue();
            userNotifications.Should().HaveCount(1);
            notifications.Should().HaveCount(1);
            notifications.First().NotificationType.Should().Be(NotificationType.GigCanceled);
        }
    }
}
