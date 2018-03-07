using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api {
    [Authorize]
    public class NotificationsController : ApiController {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        protected override void Dispose(bool disposing) {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        public IEnumerable<NotificationDto> GetNewNotifications() {
            var userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.Notifications.GetNewNotificationByUserId(userId);

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPut]
        public IHttpActionResult MarkNotificationAsRead() {
            var userId = User.Identity.GetUserId();
            var userNotifications = _unitOfWork.UserNotifications.GetNewUserNotificationByUserId(userId);

            foreach (var userNotification in userNotifications) {
                userNotification.Read();
            }

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
