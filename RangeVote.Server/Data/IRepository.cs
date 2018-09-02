using RangeVote.Common;
using System;

namespace RangeVote.Server.Data
{
    public interface IRepository
    {
        Ballot GetBallot(Guid id);
        void PutBallot(Ballot ballot);
        Ballot GetResult();
    }
}
