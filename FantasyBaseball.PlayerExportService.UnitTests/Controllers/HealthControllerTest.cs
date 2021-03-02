using FantasyBaseball.Common.Exceptions;
using FantasyBaseball.PlayerExportService.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace FantasyBaseball.PlayerExportService.Controllers.UnitTests
{
    public class HealthControllerTest
    {
        [Fact] public void BadPlayerService() 
        {
            var playerSection = new Mock<IConfigurationSection>();
            playerSection.Setup(o => o.Value).Returns("player-url");
            var config = new Mock<IConfiguration>();
            config.Setup(o => o.GetSection("ServiceUrls:PlayerService")).Returns(playerSection.Object);
            var service = new Mock<IHealthCheckService>();
            service.Setup(o => o.CheckHealth("player-url")).Throws(new ServiceException("Player Service Unhealthy"));
            var e = Assert.Throws<ServiceException>(() => new HealthController(config.Object, service.Object).GetHealth());
            Assert.Equal("Player Service Unhealthy", e.Message);
        }

        [Fact] public void ValidServices() 
        {
            var playerSection = new Mock<IConfigurationSection>();
            playerSection.Setup(o => o.Value).Returns("player-url");
            var config = new Mock<IConfiguration>();
            config.Setup(o => o.GetSection("ServiceUrls:PlayerService")).Returns(playerSection.Object);
            var service = new Mock<IHealthCheckService>();
            service.Setup(o => o.CheckHealth("player-url"));
            new HealthController(config.Object, service.Object).GetHealth();
            service.VerifyAll();
        }
    }
}