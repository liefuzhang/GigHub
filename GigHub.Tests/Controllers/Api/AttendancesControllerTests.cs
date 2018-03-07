using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api {
    /// <summary>
    /// Summary description for GigsControllerTests
    /// </summary>
    [TestClass]
    public class AttendancesControllerTests {
        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepository;
        private string _userId;
        private AttendanceDto _dto;

        [TestInitialize]
        public void TestInitialize() {
            _mockRepository = new Mock<IAttendanceRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Attendances).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1");

            _dto = new AttendanceDto {
                GigId = 1
            };
        }

        [TestMethod]
        public void Attend_AttendanceAlreadyExistsWithGivenGidIdAndUserId_ReturnBadRequest() {
            _mockRepository.Setup(r => r.GetAttendance(_dto.GigId, _userId)).Returns(new Attendance());

            var result = _controller.Attend(_dto);

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidRequest_ReturnOk() {
            var result = _controller.Attend(_dto);

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void DeleteAttendance_ValidRequest_ReturnOkWithTheIdOfDeletedAttendance() {
            _mockRepository.Setup(r => r.GetAttendance(1, _userId)).Returns(new Attendance());

            var result = _controller.DeleteAttendance(1);

            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
            ((OkNegotiatedContentResult<int>)result).Content.Should().Be(1);
        }

        [TestMethod]
        public void DeleteAttendance_NoAttendanceWithGivenGidId_ReturnNotFound() {
            var result = _controller.DeleteAttendance(1);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
