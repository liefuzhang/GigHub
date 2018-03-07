using System;
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
    public class NotificationRepositoryTests {
        private NotificationRepository _repository;
        private Mock<DbSet<UserNotification>> _mockUserNotification;

        [TestInitialize]
        public void TestInitialize() {
            _mockUserNotification = new Mock<DbSet<UserNotification>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.UserNotifications).Returns(_mockUserNotification.Object);

            _repository = new NotificationRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetNewNotificationByUserId_NotificationIsRead_ShouldNotBeReturned() {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            userNotification.Read();

            _mockUserNotification.SetSource(new[] { userNotification });

            var userNotifications = _repository.GetNewNotificationByUserId(user.Id);

            userNotifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotificationByUserId_NotificationIsForOtherUser_ShouldNotBeReturned() {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockUserNotification.SetSource(new[] { userNotification });

            var userNotifications = _repository.GetNewNotificationByUserId(user + "-");

            userNotifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotificationByUserId_NotificationIsForGivenUserAndIsNotRead_ShouldBeReturned() {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockUserNotification.SetSource(new[] { userNotification });

            var notifications = _repository.GetNewNotificationByUserId(user.Id);

            notifications.Should().HaveCount(1);
            notifications.Should().Contain(notification);
        }
    }
}
