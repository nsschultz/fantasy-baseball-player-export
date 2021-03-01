using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using FantasyBaseball.Common.Models;
using FantasyBaseball.PlayerExportService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FantasyBaseball.PlayerExportService.Controllers
{
    /// <summary>Endpoint for retrieving player data.</summary>
    [Route("api/player/export")] [ApiController] public class PlayerExportController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICsvFileWriterService _writerService;
        private readonly IDataGetterService _getterService;

        /// <summary>Creates a new instance of the controller.</summary>
        /// <param name="getterService">Service for getting the data from other services.</param>
        /// <param name="writerService">The service for writting the CSV file.</param>
        /// <param name="configuration">The configuration for the application.</param>
        public PlayerExportController(IDataGetterService getterService, ICsvFileWriterService writerService, IConfiguration configuration)
        {
            _getterService = getterService;
            _writerService = writerService;
            _configuration = configuration;
        }

        /// <summary>Export the players as a CSV file.</summary>
        /// <returns>A CSV file containing the players.</returns>
        [HttpGet] public async Task<IActionResult> ExportPlayers()
        {
            var existingPlayers = await _getterService.GetData<PlayerCollection>($"{_configuration.GetValue<string>("ServiceUrls:PlayerService")}");
            var fileContent = _writerService.WriteCsvData(existingPlayers?.Players ?? new List<BaseballPlayer>());
            Response.Headers.Add("Content-Disposition", new ContentDisposition { FileName = "players.csv", Inline = false }.ToString());
            return File(fileContent, "application/octet-stream", "players.csv");
        }
    }
}