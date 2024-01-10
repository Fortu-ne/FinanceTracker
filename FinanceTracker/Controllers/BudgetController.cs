using AutoMapper;
using FinanceTracker.Data;
using FinanceTracker.Data.DbDataContext;
using FinanceTracker.Dto;
using FinanceTracker.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private  readonly IBudget _budgetRep;

        private readonly DataContext _context;

        private readonly IUser _userRep;

        private readonly IMapper _mapper;

        public BudgetController(IBudget budget, DataContext context, IUser userRep, IMapper mapper)
        {
            _budgetRep = budget;
            _context = context;
            _userRep = userRep;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Budget>))]
        [ProducesResponseType(400)]
        public IActionResult Index()
        {
            var results = _mapper.Map<ICollection<BudgetDto>>(_budgetRep.GetBudgets());

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
        public IActionResult Create(/*[FromQuery] Guid UserId,*/[FromBody] BudgetDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }


            var model = _budgetRep.GetBudgets().Where(r => r.Name.Trim().ToLower() == request.Name.Trim().ToLower()).FirstOrDefault();

            if (model != null)
            {
                ModelState.AddModelError(" ", "budget already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var getUser = _context.Users.Where(r => r.Id == request.UserId).FirstOrDefault();

            var mod = new Budget
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Amount = request.Amount,
                User = getUser,
                UserId = request.UserId,
            };

            var budgetMapper = _mapper.Map<Budget>(mod);


            if (!_budgetRep.createBudget(budgetMapper))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);

            }

            return NoContent();
        }



        [HttpPut("{int:Guid}")]
        [ProducesResponseType(typeof(BudgetDto), 200)]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult Update(int id, [FromBody] BudgetDto request)
        {
            if (request == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var verifyBudget = _budgetRep.findBudget(id);

            if (!verifyBudget)
            {
                ModelState.AddModelError(" ", "budget not found");
                return StatusCode(404, ModelState);
            }


            var userUpdate = _context.Users.Where(r => r.Id == request.UserId).FirstOrDefault();

            var budgetUpdate = _mapper.Map<Budget>(new Budget
            {
                Id = id,
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                User = userUpdate,
                UserId = request.UserId,

            });

            if (budgetUpdate != null)
            {
                _budgetRep.updateBudget(budgetUpdate);
            }

            return NoContent();
        }



        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]

        public IActionResult GetBudget(int id)
        {
            var model = _budgetRep.findBudget(id);

            if (!model)
            {
                ModelState.AddModelError(" ", "budget not found");
                return StatusCode(404, ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var budget = _mapper.Map<BudgetDto>(_budgetRep.GetBudgetById(id));



            return Ok(budget);
        }


        [HttpDelete("{id:int}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult Delete(int id)
        {
            var model = _budgetRep.GetBudgetById(id);

            if (model == null)
            {
                ModelState.AddModelError(" ", "budget not found");
                return StatusCode(404, ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!_budgetRep.deleteBudget(model))
            {
                ModelState.AddModelError("", "An error occured, can't delete at the moment");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
