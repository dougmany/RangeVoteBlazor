using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using RangeVote.Common;

namespace RangeVote.Server.Data
{
    public class DatabaseRepository : IRepository
    {
        private static MySqlConnection GetMySqlConnection()
        {
            var connectionString = new MySqlConnectionStringBuilder("Server=localhost;Database=rangevote;Uid=rangevote;Pwd=3fd29g24g;SslMode=none");

            var connection = new MySqlConnection(connectionString.ConnectionString);
            connection.Open();
            return connection;
        }

        public Ballot GetBallot(Guid guid)
        {
            using (var conn = GetMySqlConnection())
            {
                Ballot ballot = new Ballot { Id = guid };
                ballot.Candidates = conn.Query<Candidate>("SELECT * FROM candidate WHERE Guid = @guid;", new { guid = guid }).ToArray();
                if (ballot.Candidates.Count() > 0)
                {
                    return ballot;
                }
            }
            return new Ballot { Id = guid, Candidates = DefaultCandidates };
        }

        public void PutBallot(Ballot ballot)
        {
            using (var conn = GetMySqlConnection())
            {
                conn.Execute(@"DELETE FROM candidate WHERE Guid = @guid;", new { guid = ballot.Id });

                var data = ballot.Candidates.Select(c => new DBCandidate { Guid = ballot.Id.ToString(), Name = c.Name, Score = c.Score });

                String query = "INSERT INTO candidate (Guid, Name, Score) Values (@Guid, @Name, @Score)";
                conn.Execute(query, data);
            }
        }

        private Candidate[] DefaultCandidates = new Candidate[]
        {
            new Candidate{ Name = "Alaska", Score = 50 },
            new Candidate{ Name = "Big Basin area with local attractions",    Score =  50  },
            new Candidate{ Name = "Beach resort around Monterey",    Score =  50  },
            new Candidate{ Name = "Train trip",    Score =  50  },
            new Candidate{ Name = "Yellowstone National Park",    Score =  50  },
            new Candidate{ Name = "Kuai",    Score =  50  },
            new Candidate{ Name = "Mesa Verde National Park, Colorado - ancient cliff houses",    Score =  50  },
            new Candidate{ Name = "Banff-Lake Louise",    Score =  50  },
            new Candidate{ Name = "Vancouver Island",    Score =  50  },
            new Candidate{ Name = "Cancun",    Score =  50  },
            new Candidate{ Name = "Nova Vallarta",    Score =  50  }
        };
    }
}
