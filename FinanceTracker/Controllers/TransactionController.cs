using AutoMapper;
using FinanceTracker.Data;
using FinanceTracker.Dto;
using FinanceTracker.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITransaction _transactionRep;
        public TransactionController(ITransaction transactionRep, IMapper mapper)
        {
           _mapper = mapper;
            _transactionRep = transactionRep;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Index()
        {
            var list = _mapper.Map<Transaction>(_transactionRep.GetAll());

            return Ok(list);

        }
        
        
        [HttpPost]
        [ProducesResponseType(200)]

        public IActionResult Create([FromQuery] Guid UserId, [FromBody] TransactionDto request)
        {

            if (request == null)
            {
                return BadRequest(ModelState);
            }


            var transaction = _transactionRep.GetAll().Where(r => r.Name.Trim().ToLower() == request.Name.Trim().ToLower());

            if (transaction != null)
            {
                ModelState.AddModelError(" ", "transaction already exists");
                return StatusCode(422, ModelState);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userMapper = _mapper.Map<Transaction>(request);
            

            if (!_transactionRep.create(userMapper))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }
    }
}
