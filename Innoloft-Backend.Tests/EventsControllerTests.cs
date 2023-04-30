using AutoMapper;
using Innoloft_Backend.Controllers;
using Innoloft_Backend.DTO;
using Innoloft_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace Innoloft_Backend.Tests {
    public class EventsControllerTests {
        private readonly Mock<IConfiguration> _configMock;
        private readonly Mock<EventsContext> _contextMock;
        private readonly Mock<ILogger<EventsController>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly EventsController _controller;

        public EventsControllerTests() {
            _configMock = new Mock<IConfiguration>();
            _contextMock = new Mock<EventsContext>();
            _loggerMock = new Mock<ILogger<EventsController>>();
            _mapperMock = new Mock<IMapper>();
            _controller = new EventsController(_configMock.Object, _contextMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task PostEvent_ReturnsBadRequest_WhenEventsNull() {
            // Arrange
            _contextMock.Setup(x => x.Events).Returns((DbSet<Event>)null);

            // Act
            var result = await _controller.PostEvent(new EventPostDto());

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PostEvent_ReturnsBadRequest_WhenEndTimeBeforeStartTime() {
            // Arrange
            var eventDto = new EventPostDto { StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(-1) };
            var eventEntity = new Event { StartTime = eventDto.StartTime, EndTime = eventDto.EndTime };

            _mapperMock.Setup(x => x.Map<Event>(It.IsAny<EventPostDto>())).Returns(eventEntity);
            _contextMock.Setup(x => x.Events).Returns(MockDbSet(new List<Event>()));

            // Act
            var result = await _controller.PostEvent(eventDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal("EndTime must be after the StartTime", badRequestResult.Value);
        }

        [Fact]
        public async Task PostEvent_ReturnsOk_WhenSuccessful() {
            // Arrange
            var eventDto = new EventPostDto { StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) };
            var eventEntity = new Event { StartTime = eventDto.StartTime, EndTime = eventDto.EndTime, Id = 1 };

            _mapperMock.Setup(x => x.Map<Event>(It.IsAny<EventPostDto>())).Returns(eventEntity);
            _contextMock.Setup(x => x.Events).Returns(MockDbSet(new List<Event>()));
            _contextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _controller.PostEvent(eventDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(eventEntity.Id, okResult.Value);
        }


        private DbSet<T> MockDbSet<T>(List<T> sourceList) where T : class {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();

            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }

    }
}