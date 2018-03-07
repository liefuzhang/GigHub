﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Persistence.Repositories {
    [TestClass]
    public class GigRepositoryTests {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;
        private Mock<DbSet<Attendance>> _mockAttendings;

        [TestInitialize]
        public void TestInitialize() {
            _mockGigs = new Mock<DbSet<Gig>>();
            _mockAttendings = new Mock<DbSet<Attendance>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            mockContext.SetupGet(c => c.Attendances).Returns(_mockAttendings.Object);

            _repository = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned() {
            var gig = new Gig {
                DateTime = DateTime.Now.AddDays(-1),
                ArtistId = "1"
            };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCanceled_ShouldNotBeReturned() {
            var gig = new Gig {
                DateTime = DateTime.Now.AddDays(1),
                ArtistId = "1"
            };

            gig.Cancel();

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForADifferentArtist_ShouldNotBeReturned() {
            var gig = new Gig {
                DateTime = DateTime.Now.AddDays(1),
                ArtistId = "1"
            };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId + "-");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturned() {
            var gig = new Gig {
                DateTime = DateTime.Now.AddDays(1),
                ArtistId = "1"
            };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId);

            gigs.Should().Contain(gig);
        }

        [TestMethod]
        public void GetGigsUserAttending_GigIsInThePast_ShouldNotBeReturned() {
            var gig = new Gig {
                DateTime = DateTime.Now.AddDays(-1),
                ArtistId = "1"
            };
            var attendance = new Attendance {
                Gig = gig,
                AttendeeId = "2"
            };

            _mockGigs.SetSource(new[] { gig });
            _mockAttendings.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_GigIsCanceled_ShouldNotBeReturned() {
            var gig = new Gig {
                DateTime = DateTime.Now.AddDays(1),
                ArtistId = "1"
            };
            gig.Cancel();

            var attendance = new Attendance {
                Gig = gig,
                AttendeeId = "2"
            };

            _mockGigs.SetSource(new[] { gig });
            _mockAttendings.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending("2");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_GigIsForADifferentUser_ShouldNotBeReturned() {
            var gig = new Gig {
                DateTime = DateTime.Now.AddDays(1)
            };

            var attendance = new Attendance {
                Gig = gig,
                AttendeeId = "1"
            };

            _mockGigs.SetSource(new[] { gig });
            _mockAttendings.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId + "-");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_GigIsForTheGivenUserAndIsInTheFuture_ShouldBeReturned() {
            var gig = new Gig {
                DateTime = DateTime.Now.AddDays(1)
            };

            var attendance = new Attendance {
                Gig = gig,
                AttendeeId = "1"
            };

            _mockGigs.SetSource(new[] { gig });
            _mockAttendings.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId);

            gigs.Should().Contain(gig);
        }
    }
}
