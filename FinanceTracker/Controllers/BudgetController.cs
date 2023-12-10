//using FinanceTracker.Data;
//using FinanceTracker.Interface;
//using Microsoft.AspNetCore.Mvc;

//namespace FinanceTracker.Controllers
//{

//    [Route("api/[controller]")]
//    [ApiController]
//    public class BudgetController : ControllerBase
//    {
//        private IBudget _budgetRep;

//        public BudgetController(IBudget budget)
//        {
//            _budgetRep = budget;       
//        }

//        [HttpGet]
//        [ProducesResponseType(200)]
//        public IActionResult GetAll()
//        {

//            var model = _budgetRep.GetBudgets();

//            if(!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            return Ok(model);
//        }

//        [HttpPost]
//        [ProducesResponseType(200)]
//        public IActionResult Create([FromBody] Budget request) {
        
//          if(request == null)
//            {
//                return BadRequest(ModelState);
//            }

//          var budget = _budgetRep.GetBudgets().Where(r=>r.Name.Trim().ToUpper() == request.Name.Trim().ToUpper()).FirstOrDefault();

//            if (budget != null)
//            {
//                ModelState.AddModelError(" ", "budget already exists");
//                return StatusCode(422, ModelState);
//            }

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            if(!_budgetRep.createBudget(budget))
//            {
//                ModelState.AddModelError("", "Something went wrong while saving");
//                return StatusCode(500, ModelState);
//            }

//            return Ok(budget);
//        }
//    }
//}
