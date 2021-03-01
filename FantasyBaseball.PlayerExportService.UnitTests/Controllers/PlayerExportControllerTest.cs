using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FantasyBaseball.Common.Models;
using FantasyBaseball.PlayerExportService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace FantasyBaseball.PlayerExportService.Controllers.UnitTests
{
    public class PlayerExportControllerTest
    {
        [Fact] public async Task GetFile() 
        {
            var fileContent = Encoding.ASCII.GetBytes("file-content");
            var playerSection = new Mock<IConfigurationSection>();
            playerSection.Setup(o => o.Value).Returns("player-service/player");
            var config = new Mock<IConfiguration>();
            config.Setup(o => o.GetSection("ServiceUrls:PlayerService")).Returns(playerSection.Object);
            var getter = new Mock<IDataGetterService>();
            getter.Setup(o => o.GetData<PlayerCollection>("player-service/player")).ReturnsAsync(new PlayerCollection { Players = BuildTestPlayerList(1) });
            var writer = new Mock<ICsvFileWriterService>();
            writer.Setup(o => o.WriteCsvData(It.Is<List<BaseballPlayer>>(p => p.Count == 1))).Returns(fileContent);
            var controller = new PlayerExportController(getter.Object, writer.Object, config.Object);
            var headerDictionary = new HeaderDictionary();
            var response = new Mock<HttpResponse>();
            response.SetupGet(r => r.Headers).Returns(headerDictionary);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(a => a.Response).Returns(response.Object);
            controller.ControllerContext = new ControllerContext() { HttpContext = httpContext.Object };
            var result = await controller.ExportPlayers();
            Assert.Equal("players.csv", ((FileContentResult) result).FileDownloadName);
            Assert.Equal(fileContent, ((FileContentResult) result).FileContents);
            getter.VerifyAll();
            writer.VerifyAll();
        }

        private static BaseballPlayer BuildTestPlayer(int id) => new BaseballPlayer { BhqId = id };

        private static List<BaseballPlayer> BuildTestPlayerList(int id) => new List<BaseballPlayer> { new BaseballPlayer { BhqId = id } };
    }
}