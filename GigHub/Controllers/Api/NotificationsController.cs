﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api {
    [Authorize]
    public class NotificationsController : ApiController {
        private ApplicationDbContext _context;

        public NotificationsController() {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications() {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && un.IsRead == false)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPut]
        public IHttpActionResult MarkNotificationAsRead() {
            var userId = User.Identity.GetUserId();
            var userNotifications = _context.UserNotifications
                .Where(un => un.UserId == userId && un.IsRead == false)
                .ToList();

            userNotifications.ForEach(un => un.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}
