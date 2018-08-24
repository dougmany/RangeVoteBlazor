using RangeVote.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RangeVote.Server.Data
{
    public interface IRepository
    {
        Ballot GetBallot(Guid id);
        void PutBallot(Ballot ballot);
    }
}
