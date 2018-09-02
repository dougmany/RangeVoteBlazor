using Microsoft.AspNetCore.Mvc;
using RangeVote.Server.Data;
using RangeVote.Common;
using System;

namespace RangeVote.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IRepository _repository;

        public ResultController(IRepository repository)
        {
            _repository = repository;
        }

        // GET api/Result/5
        [HttpGet]
        public Ballot Get()
        {
            return _repository.GetResult();
        }
    }
}


