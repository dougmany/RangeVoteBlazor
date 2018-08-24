using Microsoft.AspNetCore.Mvc;
using RangeVote.Server.Data;
using RangeVote.Common;
using System;

namespace RangeVote.Server.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IRepository _repository;

        public CandidateController(IRepository repository)
        {
            _repository = repository;
        }

        // GET api/Candidate/5
        [HttpGet("{id}")]
        public Ballot Get(Guid id)
        {
            return _repository.GetBallot(id);
        }

        // PUT api/Candidate/
        [HttpPut]
        public void Put(Ballot ballot)
        {
            _repository.PutBallot(ballot);
        }
    }
}


