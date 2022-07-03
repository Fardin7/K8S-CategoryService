using AutoMapper;
using CategoryService.Data;
using CategoryService.NewsClient;
using Microsoft.AspNetCore.Mvc;
using CategoryService.Contract;
using CategoryService.AsyncConnection;

namespace CategoryService.Controllers
{
    [Route("api/NewsCategory/")]
    [ApiController]
    public class NewsCategoryController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IClientUpdate _newsServiceUpdate;
        private readonly IMapper _mapper;
        private readonly INotification _notification;

        public NewsCategoryController(IRepository repository, IClientUpdate newsServiceUpdate, IMapper mapper
            ,INotification notification)
        {
            _repository = repository;
            _newsServiceUpdate = newsServiceUpdate;
            _mapper = mapper;
            _notification = notification;
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
        public async Task<ActionResult<NewsCategoryRead>> Add(NewsCategoryCreate newsCategoryCreate)
        {
            var categoriy = await _repository.Add(newsCategoryCreate);

            if (categoriy is null)
            {
                return BadRequest();
            }

            var NewsCategory = _mapper.Map<NewsCategoryCreate>(categoriy);

           // await _newsServiceUpdate.Notify(NewsCategory);

            await _notification.CreateNotify(NewsCategory);

            return CreatedAtAction(nameof(Get), new { categoriy.Id }, categoriy);
        }
        [HttpPut]
        public async Task<ActionResult<NewsCategoryRead>> Update(NewsCategoryCreate newsCategoryCreate)
        {
            var categoriy = await _repository.GetById(newsCategoryCreate.Id);
            if (categoriy == null)
            {
                categoriy = await _repository.Add(newsCategoryCreate);
            }
            else
            {
                categoriy = await _repository.Update(newsCategoryCreate);
            }

            return CreatedAtAction(nameof(Get), new { categoriy.Id }, categoriy);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoriy = await _repository.GetById(id);

            if (categoriy == null)
            {
                return NotFound();
            }

            _repository.Remove(categoriy);

            return NoContent();
        }
    }
}
