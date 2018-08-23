using Microsoft.AspNetCore.Mvc;
using RangeVote.Server.Data;
using RangeVote.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // GET api/values/5
        [HttpGet("{id}")]
        public Candidate[] Get(Guid id)
        {
            return _repository.GetCandidate(id).ToArray();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] IEnumerable<Candidate> candidate)
        {
            _repository.PutCandidate(id, candidate);
        }
    }
}


