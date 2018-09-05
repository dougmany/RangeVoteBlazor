using Microsoft.AspNetCore.Mvc;
using RangeVote.Server.Data;
using RangeVote.Common;
using System;

namespace RangeVote.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoterController : ControllerBase
    {
        private readonly IRepository _repository;

        public VoterController(IRepository repository)
        {
            _repository = repository;
        }

        // GET api/Voter
        [HttpGet]
        public Int32 Get()
        {
            return _repository.GetVoters();
        }
    }
}


