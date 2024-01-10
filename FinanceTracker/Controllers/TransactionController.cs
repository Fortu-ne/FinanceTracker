using AutoMapper;
using FinanceTracker.Data;
using FinanceTracker.Data.DbDataContext;
using FinanceTracker.Dto;
using FinanceTracker.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITransaction _transactionRep;
        private readonly DataContext _context;

        public TransactionController(ITransaction transactionRep, IMapper mapper, DataContext context)
        {
           _mapper = mapper;
            _transactionRep = transactionRep;
            _context = context;
        }


        ////[HttpGet]
        ////[ProducesResponseType(200)]
        ////public IActionResult Index()
        ////{
        ////    var list = _mapper.Map<Transaction>(_transactionRep.GetAll());

        ////    return Ok(list);

        ////}
        
        
        ////[HttpPost]
        ////[ProducesResponseType(200)]

        //public IActionResult Create(/*[FromQuery] Guid UserId*/, [FromBody] TransactionDto request)
        //{

        //    if (request == null)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    var transaction = _transactionRep.GetAll().Where(r => r.Name.Trim().ToLower() == request.Name.Trim().ToLower());

        //    if (transaction != null)
        //    {
        //        ModelState.AddModelError(" ", "transaction already exists");
        //        return StatusCode(422, ModelState);
        //    }


        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var userMapper = _mapper.Map<Transaction>(request);
            

        //    if (!_transactionRep.create(userMapper))
        //    {
        //        ModelState.AddModelError("", "Something went wrong while saving");
        //        return StatusCode(500, ModelState);
        //    }
        //    return Ok("Succesfully created");
        //}



        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<TransactionDto>))]
        [ProducesResponseType(400)]
        public IActionResult Index()
        {
            var results = _mapper.Map<ICollection<TransactionDto>>(_transactionRep.GetAll());

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
        public IActionResult Create(/*[FromQuery] Guid UserId,*/[FromBody] TransactionDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }


            var model = _transactionRep.GetAll().Where(r => r.Name.Trim().ToLower() == request.Name.Trim().ToLower()).FirstOrDefault();

            if (model != null)
            {
                ModelState.AddModelError(" ", "transaction already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var getAccount = _context.Accounts.Where(r => r.Id == request.AccountId).FirstOrDefault();

            var getCategory = _context.Categories.Where(r=>r.Id == request.CategoryId).FirstOrDefault(); 

            var mod = new Transaction
            {
                Name = request.Name,
                Amount = request.Amount,
                Date = request.Date,
                Account = getAccount,
                AccountId = request.AccountId,
                CategoryId = request.CategoryId,
                Category = getCategory
            };

            var transactionMapper = _mapper.Map<Transaction>(mod);


            if (!_transactionRep.create(transactionMapper))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);

            }

            return NoContent();
        }



        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(TransactionDto), 200)]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult Update(int id, [FromBody] TransactionDto request)
        {
            if (request == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var verifyTransaction = _transactionRep.find(id);

            if (!verifyTransaction)
            {
                ModelState.AddModelError(" ", "transaction not found");
                return StatusCode(404, ModelState);
            }

            //var transaction = _transactionRep.GetTransactionById(id);

            var getCategory = _context.Categories.Where(r=>r.Id == request.CategoryId).FirstOrDefault();
            var getAccount = _context.Accounts.Where(r=>r.Id == request.AccountId).FirstOrDefault();

            var transactionUpdate = _mapper.Map<Transaction>(new Transaction
            {
                Id = id,
                Name = request.Name,
                Amount = request.Amount,
                Date = request.Date,
                AccountId = request.AccountId,
                CategoryId = request.CategoryId,
                Account = getAccount,
                Category = getCategory,


            });

            if (!_transactionRep.update(transactionUpdate))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]

        public IActionResult GetTransaction(int id)
        {
            var model = _transactionRep.find(id);

            if (!model)
            {
                ModelState.AddModelError(" ", "transaction not found");
                return StatusCode(404, ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transaction = _mapper.Map<TransactionDto>(_transactionRep.GetTransactionById(id));



            return Ok(transaction);
        }


        [HttpDelete("{id:int}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult DeleteTransaction(int id)
        {
            var model = _transactionRep.GetTransactionById(id);

            if (model == null)
            {
                ModelState.AddModelError(" ", "transaction not found");
                return StatusCode(404, ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!_transactionRep.delete(model))
            {
                ModelState.AddModelError("", "An error occured, can't delete at the moment");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
