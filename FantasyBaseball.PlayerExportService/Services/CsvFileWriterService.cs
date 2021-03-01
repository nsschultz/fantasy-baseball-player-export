using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using FantasyBaseball.Common.Models;
using FantasyBaseball.PlayerExportService.CsvMaps;

namespace FantasyBaseball.PlayerExportService.Services
{
    /// <summary>Service for writing the CSV file.</summary>
    public class CsvFileWriterService : ICsvFileWriterService
    {
        private readonly CsvConfiguration _configuration = new CsvConfiguration(CultureInfo.CurrentCulture);

        /// <summary>Creates a new instance and configures the service.</summary>
        public CsvFileWriterService() => _configuration.RegisterClassMap<BaseballPlayerMap>();

        /// <summary>Reads in data from the given CSV file.</summary>
        /// <param name="players">All of the players to to write to the CSV.</param>
        /// <returns>A byte array of the file content.</returns>
        public byte[] WriteCsvData(List<BaseballPlayer> players)
        {
            players = players ?? new List<BaseballPlayer>();
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            using var csv = new CsvWriter(writer, _configuration); 
            csv.WriteRecords(players);
            writer.Flush();
            return stream.ToArray();
        }
    }
}