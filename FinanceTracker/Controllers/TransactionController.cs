using AutoMapper;
using FinanceTracker.Data;
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
    }
}
