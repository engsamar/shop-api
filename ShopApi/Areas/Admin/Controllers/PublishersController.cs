using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopApi.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IRepository<Publisher> _PublisherRepository;

        public PublishersController(IRepository<Publisher> PublisherRepository)
        {
            _PublisherRepository = PublisherRepository;
        }

        // CRUD
        // GET / Publishers
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var Publishers = await _PublisherRepository.GetAsync();

            return Ok(Publishers);
        }

        // GET / Publishers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var Publisher = await _PublisherRepository.GetOneAsync(e => e.Id == id);

            if (Publisher is null)
                return NotFound();

            return Ok(Publisher);
        }

        // POST / Publishers
        [HttpPost]
        public async Task<IActionResult> Create(PublisherRequest PublisherRequest)
        {
            await _PublisherRepository.CreateAsync(PublisherRequest.Adapt<Publisher>());
            await _PublisherRepository.CommitAsync();

            return Ok(new
            {
                msg = "Add Publisher Successfully"
            });
        }

        // PUT / Publishers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PublisherRequest PublisherRequest)
        {
            var PublisherInDB = await _PublisherRepository.GetOneAsync(e => e.Id == id);

            if (PublisherInDB is null)
                return NotFound();

            PublisherInDB.Name = PublisherRequest.Name;
            PublisherInDB.Description = PublisherRequest.Description;
            PublisherInDB.Status = PublisherRequest.Status;

            await _PublisherRepository.CommitAsync();

            return Ok(new
            {
                msg = "Update Publisher Successfully"
            });
        }

        // DELETE / Publishers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var PublisherInDB = await _PublisherRepository.GetOneAsync(e => e.Id == id);

            if (PublisherInDB is null)
                return NotFound();
            
            _PublisherRepository.Delete(PublisherInDB);
            await _PublisherRepository.CommitAsync();

            return Ok(new
            {
                msg = "Delete Publisher Successfully"
            });
        }
    }
}
