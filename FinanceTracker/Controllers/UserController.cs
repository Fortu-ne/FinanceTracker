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

        public IActionResult Create([FromBody] UserDto request)
        {

            if (request == null)
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

            if (!_userRep.createUser(userMapper))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");
        }


        [HttpPut("{id:Guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Update(Guid id,[FromBody] UserDto request)
        {

            if (request == null)
            {
                return BadRequest(ModelState);
            }

            var verifyUser = _userRep.findUser(id);

            if (!verifyUser)
            {
                ModelState.AddModelError(" ", "user not found");
                return StatusCode(404, ModelState);
            }



            var updateUser = _mapper.Map<User>(new User
            {
                Id = id,
                Name = request.Name,
                Surname = request.Surname,
                MonthlySalary = request.MonthlySalary,
                Email = request.Email,
            });

            if (updateUser != null)
            {
                _userRep.updateUser(updateUser);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok("Succesffuly Updated");

        }



        [HttpGet("{id:Guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]

        public IActionResult GetUser(Guid id)
        {
            var model = _userRep.findUser(id);

            if (!model)
            {
                ModelState.AddModelError(" ", "user not found");
                return StatusCode(404, ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<UserDto>(_userRep.GetUser(id));



            return Ok(user);
        }


        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]

        public IActionResult DelelteCategory(Guid id)
        {
            var model = _userRep.GetUser(id);

            if (model == null)
            {
                ModelState.AddModelError(" ", "user not found");
                return StatusCode(404, ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (model != null)
            {
                _userRep.deleteUser(model);
            }


            return NoContent();
        }



    }




}
