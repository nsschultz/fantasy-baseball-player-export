using System.Collections.Generic;
using System.Text;
using FantasyBaseball.Common.Enums;
using FantasyBaseball.Common.Models;
using Xunit;

namespace FantasyBaseball.PlayerExportService.Services.UnitTests
{
    public class CsvFileWriterServiceTest
    {
        private const string HEADER = "..PLAYER ID,..FIRST NAME,..LAST NAME,..AGE,..TYPE,..POSITION(S),..TEAM,..STATUS,..LEAGUE #1,..LEAGUE #2,..D-RANK,..ADP,..HP,..% DRAFTED,..REL,..MM,B.YTD.AB,B.YTD.R,B.YTD.H,B.YTD.2B,B.YTD.3B,B.YTD.HR,B.YTD.RBI,B.YTD.BB,B.YTD.K,B.YTD.SB,B.YTD.CS,B.YTD.TB,B.YTD.AVG,B.YTD.OBP,B.YTD.SLG,B.YTD.OPS,B.YTD.CT%,B.YTD.PX,B.YTD.BB%,B.YTD.SPD,B.YTD.BPV,P.YTD.W,P.YTD.L,P.YTD.QS,P.YTD.SV,P.YTD.BS,P.YTD.HLD,P.YTD.IP,P.YTD.H,P.YTD.ER,P.YTD.HR,P.YTD.BB,P.YTD.K,P.YTD.F%,P.YTD.G%,P.YTD.ERA,P.YTD.WHIP,P.YTD.BABIP,P.YTD.S%,P.YTD.CMD,P.YTD.DOM,P.YTD.CON,P.YTD.G/F%,P.YTD.BPV,B.PROJ.AB,B.PROJ.R,B.PROJ.H,B.PROJ.2B,B.PROJ.3B,B.PROJ.HR,B.PROJ.RBI,B.PROJ.BB,B.PROJ.K,B.PROJ.SB,B.PROJ.CS,B.PROJ.TB,B.PROJ.AVG,B.PROJ.OBP,B.PROJ.SLG,B.PROJ.OPS,B.PROJ.CT%,B.PROJ.PX,B.PROJ.BB%,B.PROJ.SPD,B.PROJ.BPV,P.PROJ.W,P.PROJ.L,P.PROJ.QS,P.PROJ.SV,P.PROJ.BS,P.PROJ.HLD,P.PROJ.IP,P.PROJ.H,P.PROJ.ER,P.PROJ.HR,P.PROJ.BB,P.PROJ.K,P.PROJ.F%,P.PROJ.G%,P.PROJ.ERA,P.PROJ.WHIP,P.PROJ.BABIP,P.PROJ.S%,P.PROJ.CMD,P.PROJ.DOM,P.PROJ.CON,P.PROJ.G/F%,P.PROJ.BPV,B..AB,B..R,B..H,B..2B,B..3B,B..HR,B..RBI,B..BB,B..K,B..SB,B..CS,B..TB,B..AVG,B..OBP,B..SLG,B..OPS,B..CT%,B..PX,B..BB%,B..SPD,B..BPV,P..W,P..L,P..QS,P..SV,P..BS,P..HLD,P..IP,P..H,P..ER,P..HR,P..BB,P..K,P..F%,P..G%,P..ERA,P..WHIP,P..BABIP,P..S%,P..CMD,P..DOM,P..CON,P..G/F%,P..BPV";
        
        [Fact] public void WritePlayersEmpty() 
        {
            var expected = HEADER + "\n";
            Assert.Equal(expected, Encoding.ASCII.GetString(new CsvFileWriterService().WriteCsvData(new List<BaseballPlayer>())));
        }

        [Fact] public void WritePlayersNull() 
        {
            var expected = HEADER + "\n";
            Assert.Equal(expected, Encoding.ASCII.GetString(new CsvFileWriterService().WriteCsvData(null)));
        }

        [Fact] public void WritePlayersValid() 
        {
            var expected = HEADER + "\n";
            expected += "1,First-1,Last-1,1,B,Pos-1,Team-1,XX,A,A,1,9999,9999,0,0,1,300,75,96,24,6,12,48,30,60,9,3,0,0,0,0,0,0,100,0,61,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,200,50,64,16,4,8,32,20,40,6,2,0,0,0,0,0,0,225,0,96,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,500,125,160,40,10,20,80,50,100,15,5,280,0.32,0.38181818181818183,0.56,0.9418181818181819,0.8,150,0.09090909090909091,75,82.6818181818182,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0\n";
            expected += "2,First-2,Last-2,2,P,Pos-2,Team-2,XX,A,A,2,9999,9999,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,12,6,18,9,3,15,60,45,24,1,30,120,0.2,0.31,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,4,12,6,2,10,40,30,16,0,20,80,0.45,0.66,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,20,10,30,15,5,25,100,75,40,1,50,200,0.3,0.45,3.6,1.25,0.47435897435897434,0.6854838709677419,4,18,4.5,1.5,225.5\n";
            var players = new List<BaseballPlayer> { BuildPlayer(1, PlayerType.B), BuildPlayer(2, PlayerType.P) };
            Assert.Equal(expected, Encoding.ASCII.GetString(new CsvFileWriterService().WriteCsvData(players)));
        }

        private static BaseballPlayer BuildPlayer(int value, PlayerType type) =>
            new BaseballPlayer 
            {
                BhqId = value,
                FirstName = $"First-{value}",
                LastName = $"Last-{value}",
                Age = value,
                Type = type,
                Positions = $"Pos-{value}",
                Team = $"Team-{value}",
                Status = PlayerStatus.XX,
                League1 = LeagueStatus.A,
                DraftRank = value,
                MayberryMethod = value, 
                YearToDateBattingStats = PlayerType.B == type 
                    ? new BattingStats
                        {
                            AtBats = 300,
                            Runs = 75,
                            Hits = 96,
                            Doubles = 24,
                            Triples = 6,
                            HomeRuns = 12,
                            RunsBattedIn = 48,
                            BaseOnBalls = 30,
                            StrikeOuts = 60,
                            StolenBases = 9,
                            CaughtStealing = 3,
                            Power = 100,
                            Speed = 61
                        } 
                    : new BattingStats(),
                YearToDatePitchingStats = PlayerType.P == type 
                    ? new PitchingStats
                        {
                            Wins = 12,
                            Losses = 6,
                            QualityStarts = 18,
                            Saves = 9,
                            BlownSaves = 3,
                            Holds = 15,
                            InningsPitched = 60,
                            HitsAllowed = 45,
                            EarnedRuns = 24,
                            HomeRunsAllowed = 1,
                            BaseOnBallsAllowed = 30,
                            StrikeOuts = 120,
                            FlyBallRate = 0.2,
                            GroundBallRate = 0.31
                        } 
                    : new PitchingStats(),
                ProjectedBattingStats = PlayerType.B == type 
                    ? new BattingStats
                        {
                            AtBats = 200,
                            Runs = 50,
                            Hits = 64,
                            Doubles = 16,
                            Triples = 4,
                            HomeRuns = 8,
                            RunsBattedIn = 32,
                            BaseOnBalls = 20,
                            StrikeOuts = 40,
                            StolenBases = 6,
                            CaughtStealing = 2,
                            Power = 225,
                            Speed = 96
                        } 
                    : new BattingStats(),
                ProjectedPitchingStats = PlayerType.P == type 
                    ? new PitchingStats
                        {
                            Wins = 8,
                            Losses = 4,
                            QualityStarts = 12,
                            Saves = 6,
                            BlownSaves = 2,
                            Holds = 10,
                            InningsPitched = 40,
                            HitsAllowed = 30,
                            EarnedRuns = 16,
                            HomeRunsAllowed = 0,
                            BaseOnBallsAllowed = 20,
                            StrikeOuts = 80,
                            FlyBallRate = 0.45,
                            GroundBallRate = 0.66
                        } 
                    : new PitchingStats(),
                CombinedBattingStats = PlayerType.B == type 
                    ? new BattingStats
                        {
                            AtBats = 500,
                            Runs = 125,
                            Hits = 160,
                            Doubles = 40,
                            Triples = 10,
                            HomeRuns = 20,
                            RunsBattedIn = 80,
                            BaseOnBalls = 50,
                            StrikeOuts = 100,
                            StolenBases = 15,
                            CaughtStealing = 5,
                            TotalBases = 280,
                            BattingAverage = 0.32,
                            OnBasePercentage = 210d / 550,
                            SluggingPercentage = 0.56,
                            OnBasePlusSlugging = 210d / 550 + .56,
                            ContractRate = 0.8,
                            Power = 150,
                            WalkRate = 50d / 550,
                            Speed = 75,
                            BasePerformanceValue = 82.6818181818182
                        } 
                    : new BattingStats(),
                CombinedPitchingStats = PlayerType.P == type 
                    ? new PitchingStats
                        {
                            Wins = 20,
                            Losses = 10,
                            QualityStarts = 30,
                            Saves = 15,
                            BlownSaves = 5,
                            Holds = 25,
                            InningsPitched = 100,
                            HitsAllowed = 75,
                            EarnedRuns = 40,
                            HomeRunsAllowed = 1,
                            BaseOnBallsAllowed = 50,
                            StrikeOuts = 200,
                            FlyBallRate = .30,
                            GroundBallRate = .45,
                            EarnedRunAverage = 3.6,
                            WalksAndHitsPerInningPitched = 1.25,
                            BattingAverageOnBallsInPlay = 74 / 156d,
                            StrandRate = 85 / 124d,
                            Command = 4,
                            Dominance = 18,
                            Control = 4.5,
                            GroundBallToFlyBallRate = 1.5,
                            BasePerformanceValue = 225.5
                        } 
                    : new PitchingStats(),
            };
    }
}