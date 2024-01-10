using AutoMapper;
using FinanceTracker.Data;
using FinanceTracker.Data.DbDataContext;
using FinanceTracker.Dto;
using FinanceTracker.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using static System.Collections.Specialized.BitVector32;

namespace FinanceTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly IAccounts accountRep; 

        private readonly DataContext _context;

        private readonly IUser _userRep;
  
        private readonly IMapper _mapper;

        public AccountController(IAccounts accountsRep, IMapper mapper, IUser userRep, DataContext context)
        {
            accountRep = accountsRep;
            _mapper = mapper;
            _userRep = userRep;
            _context = context;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Account>))]
        [ProducesResponseType(400)]
        public IActionResult Index()
        {
            var results = _mapper.Map<ICollection<Account>>(accountRep.GetAccounts());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(results);
        }

        [HttpPost("Create")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create(/*[FromQuery] Guid UserId,*/[FromBody]  AccountDto request)
        {
            if(request == null)
            {
                return BadRequest(ModelState);
            }


            var model  = accountRep.GetAccounts().Where(r=>r.AccountName.Trim().ToLower() == request.AccountName.Trim().ToLower()).FirstOrDefault();

            if (model != null)
            {
                ModelState.AddModelError(" ", "account already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var getUser = _context.Users.Where(r => r.Id == request.UserId).FirstOrDefault();

            var mod = new Account
            {
                AccountName = request.AccountName,
                AccountType = request.AccountType,
                Balance = request.Balance,
                User = getUser,
                UserId = request.UserId,
            };

            var accountMapper = _mapper.Map<Account>(mod);
      

            if (!accountRep.createAccount(request.UserId,accountMapper))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);

            }

            return NoContent();
        }



        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(AccountDto), 200)]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult Update(Guid id, [FromBody] AccountDto request)
        {
            if (request == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var verifyAccount = accountRep.findAccount(id);

            if (!verifyAccount)
            {
                ModelState.AddModelError(" ", "account not found");
                return StatusCode(404, ModelState);
            }

         
            var updateUser = _context.Users.Where(r=>r.Id == id).FirstOrDefault();

            var accountUpdate = _mapper.Map<Account>(new Account
            {
                Id = id,
                AccountName = request.AccountName,
                Balance = request.Balance,
                AccountType = request.AccountType,
                User =updateUser,
                UserId = request.UserId,
                
            });

            if (!accountRep.updateAccount(accountUpdate))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        [HttpGet("{id:Guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]

        public IActionResult GetAccount(Guid id)
        {
            var model = accountRep.findAccount(id);

            if (!model)
            {
                ModelState.AddModelError(" ", "account not found");
                return StatusCode(404, ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<AccountDto>(accountRep.GetAccountById(id));



            return Ok(user);
        }


        [HttpDelete("{id:Guid}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult DeleteAccount(Guid id)
        {
            var model = accountRep.GetAccountById(id);

            if (model == null)
            {
                ModelState.AddModelError(" ", "account not found");
                return StatusCode(404, ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!accountRep.deleteAccount(model))
            {
                ModelState.AddModelError("", "An error occured, can't delete at the moment");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



    }

}
