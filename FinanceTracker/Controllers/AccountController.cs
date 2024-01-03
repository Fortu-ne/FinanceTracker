using AutoMapper;
using FinanceTracker.Data;
using FinanceTracker.Data.DbDataContext;
using FinanceTracker.Dto;
using FinanceTracker.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly IAccounts accountRep;
  
        private readonly IMapper _mapper;

        public AccountController(IAccounts accountsRep, IMapper mapper)
        {
            accountRep = accountsRep;  
            _mapper = mapper;
        }


        [HttpGet]

        public IActionResult Index()
        {
            var results = _mapper.Map<List<Account>>(accountRep.GetAccounts());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(results);
        }


        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create([FromBody]  AccountDto request)
        {
            if(request == null)
            {
                return BadRequest();
            }


            var model  = accountRep.GetAccounts().Where(r=>r.AccountName.Trim().ToLower() == request.AccountName.Trim().ToLower()).FirstOrDefault();

            if (model != null)
            {
                ModelState.AddModelError(" ", "account already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var accountMapper = _mapper.Map<Account>(request);

            if (!accountRep.createAccount(accountMapper))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);

            }

            return NoContent();
        }
    }

}
