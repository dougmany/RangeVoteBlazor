﻿using System;
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
                    Description = "Stay in the Glacier Park Lodge, pristine forests, alpine meadows, rugged mountains, and spectacular lakes.  Take the train here from Portland or Seattle, fly back to from Oakland.",
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
                    Description = "Museums, Monuments, Government",
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

            DefaultCandidates = new Candidate[]
            {
                new Candidate
                {
                    Name = "Denali National Park",
                    Description = "6 million acres, one road, shuttle bus only. All visitors to Denali wishing to venture past Mile 15 into the heart of the park must ride a bus to Toklat River, Eielson Visitor Center, Wonder Lake, or Kantishna",
                    Score = 50,
                    ElectionID = _config.ElectionID
                },
                new Candidate
                {
                    Name = "Katmai National Park",
                    Description = "Fly to Kodiak Island from Anchorage by float plane to stay in all inclusive wilderness cabin.  Alternatively, we can stay on Kodiak Island exploring the island hiking, tide-pooling, history, or taking boat tours from there and staying at a local hotel. The Kodiak Wildlife Refuge.  Gray Whales seasonally feeding right offshore of 'Surfer's Beach, and there is the scenic adjacent 'Fossil Beach as well, beautiful interior mountains where you might have a chance to see bear early morning or late evening when a salmon run is not engaging bear at visible river sites. Wonderful rain forest interiors and ocean vistas (bring your binoculars). Birds and hiking trails.",
                    Score = 50,
                    ElectionID = _config.ElectionID
                },
                new Candidate
                {
                    Name = "Kenai National Park",
                    Description = "At the edge of the Kenai Peninsula lies a land where the ice age lingers. Nearly 40 glaciers flow from the Harding Icefield, Kenai Fjords' crowning feature. Wildlife thrives in icy waters and lush forests around this vast expanse of ice. Sugpiaq people relied on these resources to nurture a life entwined with the sea.  3.5 hours by train from Anchorage(great scenic train ride- includes a glacier).  In Seward, Stay in Harborside hotel with pool or Best Western hotel by the Marine Mammal Center. Town is almost walkable with shuttles around town and to Exit Glacier. Kinda like a little Lahaina with glaciers.  There are many glaciers here to hike around or on, and go on a boat tour to see tidewater glaciers. There are tons of Marine mammals including orca, humpbacks, otters, sea lions, etc. There is a Marine Center with a great display. Many day boat tours to view the park.",
                    Score = 50,
                    ElectionID = _config.ElectionID
                },
                new Candidate
                {
                    Name = "Poipu (South point of island)",
                    Description = "Poipu Beach in front has gentle waves and and clear water -- perfect for snorkeling or learning to surf.  Hyatt: top hotel with amazing pool, Kiahuna Plantation Resort OR Castle Kiahuna Plantation Beach Bungalows: Outrigger -cheaper, good pool with one water slide. The condos are large, if not luxe (no AC, unpredictable decor). Some say there is a pool, though small, has a waterslide and kiddie area. Others say no pool. Condos are spread out over grassy area.",
                    Score = 50,
                    ElectionID = _config.ElectionID
                },
                new Candidate
                {
                    Name = "Princeville (North point of island)",
                    Description = "Beautiful resort area with views and walking trails. Near Hanalei town - good spot.  Westin: top hotel with amazing pool - suites all have washers and dryers as well as kitchens with dishwashers, stovetops, and microwaves. The resort's four pools include a kids' pool. The beach, however, is a 10-minute walk down a steep path.",
                    Score = 50,
                    ElectionID = _config.ElectionID
                },
                new Candidate
                {
                    Name = "Kaapa (East side of island)",
                    Description = "Close to the airport. Narrow beach, not sure about the snorkeling, may be ok.  Waipouli Beach Resort - Outrigger: nice with good pool and waterslides",
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
