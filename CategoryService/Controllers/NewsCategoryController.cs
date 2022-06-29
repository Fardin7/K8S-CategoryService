using AutoMapper;
using CategoryService.Data;
using CategoryService.NewsClient;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using CategoryService.Dtos;

namespace CategoryService.Controllers
{
    [Route("api/NewsCategory/")]
    [ApiController]
    public class NewsCategoryController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IClientUpdate _newsServiceUpdate;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public NewsCategoryController(IRepository repository, IClientUpdate newsServiceUpdate, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _newsServiceUpdate = newsServiceUpdate;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _repository.GetById(id);

            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _repository.Get();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewsCategoryCreateDto newsCategoryCreate)
        {
            var categoriy = await _repository.Add(newsCategoryCreate);

          //  await _publishEndpoint.Publish(_mapper.Map<NewsCategoryCreate>(categoriy));

            if (categoriy != null)
            {
                return CreatedAtAction(nameof(Get), new { categoriy.Id }, categoriy);
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> Update(NewsCategoryCreateDto newsCategoryCreate)
        {
            var categoriy = await _repository.GetById(newsCategoryCreate.Id);
            if (categoriy == null)
            {
                categoriy = await _repository.Add(newsCategoryCreate);

               // await _publishEndpoint.Publish(_mapper.Map<NewsCategoryCreate>(newsCategoryCreate));
            }
            else
            {
                categoriy = await _repository.Update(newsCategoryCreate);
               // await _publishEndpoint.Publish(_mapper.Map<NewsCategoryUpdate>(newsCategoryCreate));
            }

            return CreatedAtAction(nameof(Get), new { categoriy.Id }, categoriy);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var categoriy = await _repository.GetById(id);

            if (categoriy == null)
            {
                return NotFound();
            }

            _repository.Remove(categoriy);

           // await _publishEndpoint.Publish(_mapper.Map<NewsCategoryDelete>(categoriy));

            return NoContent();
        }
    }
}
