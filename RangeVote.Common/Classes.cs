using System;

namespace RangeVote.Common
{
    public class Ballot
    {
        public Guid Id { get; set; }
        public Candidate[] Candidates { get; set; }
    }

    public class Candidate
    {
        public String Name { get; set; }
        public Double Score { get; set; }
    }
}
