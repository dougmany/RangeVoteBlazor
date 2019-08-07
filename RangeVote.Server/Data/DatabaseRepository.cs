using System;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using RangeVote.Common;

namespace RangeVote.Server.Data
{
    public class DatabaseRepository : IRepository
    {
        private readonly ConfigData _config;
        private static MySqlConnection _sqlConnection;
        private static Candidate[] DefaultCandidates;

        public DatabaseRepository(IOptions<ConfigData> config)
        {
            _config = config.Value;

            var connectionString = new MySqlConnectionStringBuilder(_config.ConnectionString);

            var connection = new MySqlConnection(connectionString.ConnectionString);
            connection.Open();
            _sqlConnection = connection;

            DefaultCandidates = new Candidate[]
            {
                new Candidate{ Name = "Alaska", Score = 50, ElectionID = _config.ElectionID },
                new Candidate{ Name = "Big Basin area with local attractions", Score = 50, ElectionID = _config.ElectionID },
                new Candidate{ Name = "Beach resort around Monterey", Score = 50, ElectionID = _config.ElectionID },
                new Candidate{ Name = "Train trip", Score = 50, ElectionID = _config.ElectionID },
                new Candidate{ Name = "Yellowstone National Park", Score = 50, ElectionID = _config.ElectionID },
                new Candidate{ Name = "Kuai", Score = 50, ElectionID = _config.ElectionID },
                new Candidate{ Name = "Mesa Verde National Park, Colorado - ancient cliff houses", Score = 50, ElectionID = _config.ElectionID },
                new Candidate{ Name = "Banff-Lake Louise", Score = 50, ElectionID = _config.ElectionID },
                new Candidate{ Name = "Vancouver Island", Score = 50, ElectionID = _config.ElectionID  },
                new Candidate{ Name = "Cancun", Score = 50, ElectionID = _config.ElectionID },
                new Candidate{ Name = "Nova Vallarta", Score = 50, ElectionID = _config.ElectionID  }
            };

            DefaultCandidates = new Candidate[]
            {
                new Candidate
                {
                    Name = "Big Island (Hawaii)",
                    Description = "Revisit our favorite parts of the Big Island.",
                    Score = 50,
                    ElectionID = _config.ElectionID
                },
                new Candidate
                {
                    Name = "Kauai",
                    Description = "Stay at the Hyatt, Westin, Hilton, or Outrigger resorts with nice pools. Some on the beach, some in Princeville above the beach. Activities include: Boat trip Na Pali Coastline with snorkeling or a dinner cruise, Waimea Canyon, Hanalei Bay, Trails, Waterfalls, river float, National Botanical Garden, Kilauea Lighthouse (and bird preserve), and cute towns on the beach.",
                    Score = 50,
                    ElectionID = _config.ElectionID
                },
                new Candidate
                {
                    Name = "Maui",
                    Description = "Revisit our favorite parts of Maui with optional day trip to Lanai, snorkeling, Haleakala, goat farm, beaches, etc.  We could stay at a resort with a big kid friendly pool or at one of Mom and Dad’s places.",
                    Score = 50,
                    ElectionID = _config.ElectionID
                },
                new Candidate
                {
                    Name = "Glacier National Park",
                    Description = "Stay in the Glacier Park Lodge, Take the train here from Portland or Seattle, non stop flights from Oakland to Glacier are available.",
                    Score = 50,
                    ElectionID = _config.ElectionID
                },
                new Candidate
                {
                    Name = "Alaska Land",
                    Description = "Fly to Anchorage or Seward and enjoy the national parks up there.  These include (pick one)  Denali, Kenai Fjords, Katmai or Kodiak",
                    Score = 50,
                    ElectionID = _config.ElectionID
                },
                new Candidate
                {
                    Name = "Washington DC",
                    Description = "Museams, Monuments, Government",
                    Score = 50,
                    ElectionID = _config.ElectionID
                },
                new Candidate
                {
                    Name = "Monterey or Big Sur",
                    Description = "Beach, Monterey Aquarium, Santa Cruz Redwoods, Roaring Camp Railroad ",
                    Score = 50,
                    ElectionID = _config.ElectionID
                },
                new Candidate
                {
                    Name = "St Johns (Caribbean Island)",
                    Description = "National park and looks beautiful.  American Virgin Island",
                    Score = 50,
                    ElectionID = _config.ElectionID
                }
            };
        }

        public Ballot GetBallot(Guid guid)
        {
            using (var conn = _sqlConnection)
            {
                Ballot ballot = new Ballot { Id = guid };
                ballot.Candidates = conn.Query<Candidate>(
                    "SELECT * FROM candidate WHERE Guid = @guid AND ElectionID = @electionID;",
                    new { guid = guid, electionID = _config.ElectionID }
                ).ToArray();
                if (ballot.Candidates.Count() > 0)
                {
                    return ballot;
                }
            }
            return new Ballot { Id = guid, Candidates = DefaultCandidates };
        }

        public void PutBallot(Ballot ballot)
        {
            using (var conn = _sqlConnection)
            {
                conn.Execute(
                    @"DELETE FROM candidate WHERE Guid = @guid AND ElectionID = @electionID;",
                    new { guid = ballot.Id, electionID = _config.ElectionID }
                );

                var data = ballot.Candidates.Select(c => new DBCandidate
                {
                    Guid = ballot.Id.ToString(),
                    Name = c.Name,
                    Score = c.Score,
                    ElectionID = c.ElectionID,
                    Description = c.Description
                });

                String query = "INSERT INTO candidate (Guid, Name, Score, ElectionID, Description) Values (@Guid, @Name, @Score, @ElectionID, @Description)";
                conn.Execute(query, data);
            }
        }

        public Ballot GetResult()
        {
            using (var conn = _sqlConnection)
            {
                Ballot ballot = new Ballot { };
                ballot.Candidates = conn.Query<Candidate>(
                    @"SELECT Name, CAST(ROUND(SUM(Score)/COUNT(DISTINCT Guid))AS SIGNED) AS Score 
                    FROM candidate
                    WHERE ElectionID = @electionID
                    GROUP BY Name 
                    ORDER BY SUM(Score) DESC;",
                    new { electionID = _config.ElectionID }
                ).ToArray();
                if (ballot.Candidates.Count() > 0)
                {
                    return ballot;
                }
            }
            return new Ballot { Candidates = DefaultCandidates };
        }

        public int GetVoters()
        {
            var query = "SELECT COUNT(DISTINCT Guid) FROM candidate WHERE ElectionID = @electionID;";
            using (var conn = _sqlConnection)
            {
                return conn.Query<Int32>(query, new { electionID = _config.ElectionID }).FirstOrDefault();
            }
        }
    }

    public class ConfigData
    {
        public String ConnectionString { get; set; }
        public String ElectionID { get; set; }
    }
}
