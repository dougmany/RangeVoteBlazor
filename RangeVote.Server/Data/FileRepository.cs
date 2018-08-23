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

        public IEnumerable<Candidate> GetCandidate(Guid id)
        {
            return _candidates;
        }

        public void PutCandidate(Guid id, IEnumerable<Candidate> candidates)
        {
            _candidates = candidates.ToList();
        }
    }
}
