using AutoMapper;
using FinanceTracker.Data;
using FinanceTracker.Dto;
using FinanceTracker.Interface;
using FinanceTracker.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userRep;
        private readonly IMapper _mapper;
        public UserController(IUser userRepository, IMapper mapper) { 
        
            _userRep = userRepository;
            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Index()
        {
            var users = _mapper.Map<List<User>>(_userRep.GetUsers());

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            return Ok(users);
        }


        [HttpPost]
        [ProducesResponseType(200)]

        public IActionResult Create([FromBody]  UserDto request)
        {

            if(request == null)
            {
                return BadRequest(ModelState); 
            }

            var user = _userRep.GetUsers().Where(r => r.Email.Trim().ToLower() == request.Email.Trim().ToLower() ||
            r.Username.Trim().ToLower() == request.Username.Trim().ToLower()).FirstOrDefault();


            if (user != null)
            {
                ModelState.AddModelError(" ", "user already exists");
                return StatusCode(422, ModelState);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userMapper = _mapper.Map<User>(request);

            if(!_userRep.createUser(userMapper))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }
    }
}
