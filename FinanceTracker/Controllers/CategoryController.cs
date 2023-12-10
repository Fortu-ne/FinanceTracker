using AutoMapper;
using FinanceTracker.Data;
using FinanceTracker.Dto;
using FinanceTracker.Interface;
using FinanceTracker.Mapping;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinanceTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategory _category;
        private readonly IMapper _mapper;

        public CategoryController(ICategory category, IMapper mapper)
        {
             _category = category;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Index()
        {
            var results = _mapper.Map<List<Category>>(_category.GetAll());

            if(!ModelState.IsValid) {

                return BadRequest(ModelState);
            }
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create([FromBody] CategoryDto request)
        {

            if (request == null)
            {
                return BadRequest(ModelState);
            }


            var model = _category.GetAll().Where(r => r.Name.Trim().ToLower() == request.Name.Trim().ToLower()).FirstOrDefault();

            if (model != null)
            {
                ModelState.AddModelError(" ", "category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

       

            var categotyMapper = _mapper.Map<Category>(request);

            if (!_category.createCategory(categotyMapper))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]

        public IActionResult UpdateCatergory (int id, [FromBody] CategoryDto request )
        {
            if(request == null)
            {
                return BadRequest(ModelState) ;
            }

            var verifyCategory =_category.findCategory(id);

            if(!verifyCategory)
            {
                ModelState.AddModelError(" ", "category not found");
                return StatusCode(404, ModelState);
            }

            
            
            var updateCategory = _mapper.Map<Category>(new Category
            {
                Id = id,
                Name = request.Name,
            });

            if (updateCategory != null)
            {
                _category.updateCategory(updateCategory);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok("Succesffuly Updated");
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]

        public IActionResult GetCategory(int id)
        {
            var model = _category.findCategory(id);

            if (!model)
            {
                ModelState.AddModelError(" ", "category not found");
                return StatusCode(404, ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _mapper.Map<CategoryDto>(_category.GetCategoryById(id));



            return Ok(category);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]

        public IActionResult DelelteCategory(int id)
        {
            var model = _category.GetCategoryById(id);

            if (model == null)
            {
                ModelState.AddModelError(" ", "category not found");
                return StatusCode(404, ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if(model != null)
            {
                _category.deleteCategory(model);
            }
           

            return NoContent();
        }



    }
}
