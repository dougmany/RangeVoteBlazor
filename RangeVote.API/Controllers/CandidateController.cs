using Microsoft.AspNetCore.Mvc;
using RangeVote.API.Data;
using RangeVote.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RangeVote.API.Controllers
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
        public IEnumerable<Candidate> Get(Guid id)
        {
            return _repository.GetCandidate(id);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] IEnumerable<Candidate> candidate)
        {
            _repository.PutCandidate(id, candidate);
        }
    }
}


