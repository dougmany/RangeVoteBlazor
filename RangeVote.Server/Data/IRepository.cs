using RangeVote.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RangeVote.Server.Data
{
    public interface IRepository
    {
        IEnumerable<Candidate> GetCandidate(Guid id);
        void PutCandidate(Guid id, IEnumerable<Candidate> candidate);
    }
}
