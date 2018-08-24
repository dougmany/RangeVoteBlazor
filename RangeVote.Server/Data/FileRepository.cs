using System;
using System.Collections.Generic;
using System.Linq;
using RangeVote.Common;

namespace RangeVote.Server.Data
{
    public class FileRepository : IRepository
    {
        private List<Candidate> _candidates = new List<Candidate>
        {
            new Candidate { Name = "Alaska", Score = 5 },
            new Candidate {    Name =  "Big Basin area with local attractions",    Score =  5  },
            new Candidate{    Name =  "Beach resort around Monterey",    Score =  5  },
            new Candidate{    Name =  "Train trip",    Score =  5  },
            new Candidate{    Name =  "Yellowstone National Park",    Score =  5  },
            new Candidate{    Name =  "Kuai",    Score =  5  },
            new Candidate{    Name =  "Mesa Verde National Park, Colorado - ancient cliff houses",    Score =  5  },
            new Candidate{    Name =  "Banff-Lake Louise",    Score =  5  },
            new Candidate{    Name =  "Vancouver Island",    Score =  5  },
            new Candidate{    Name =  "Cancun",    Score =  5  },
            new Candidate{    Name =  "Nova Vallarta",    Score =  5  }
        };

        public Ballot GetBallot(Guid id)
        {
            return new Ballot { Id = id, Candidates = _candidates.ToArray() };
        } 

        public void PutBallot(Ballot ballot)
        {
            _candidates = ballot.Candidates.ToList();
        }
    }
}
