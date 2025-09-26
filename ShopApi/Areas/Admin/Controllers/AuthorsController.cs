using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopApi.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorsController(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // CRUD
        // GET / categories
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var categories = await _authorRepository.GetAsync();

            return Ok(categories);
        }

        // GET / categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorRepository.GetOneAsync(e => e.Id == id);

            if (author is null)
                return NotFound();

            return Ok(author);
        }

        // POST / categories
        [HttpPost]
        public async Task<IActionResult> Create(AuthorRequest authorRequest)
        {
            await _authorRepository.CreateAsync(authorRequest.Adapt<Author>());
            await _authorRepository.CommitAsync();

            return Ok(new
            {
                msg = "Add author Successfully"
            });
        }

        // PUT / categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AuthorRequest authorRequest)
        {
            var authorInDB = await _authorRepository.GetOneAsync(e => e.Id == id);

            if (authorInDB is null)
                return NotFound();

            authorInDB.Name = authorRequest.Name;
            authorInDB.Description = authorRequest.Description;
            authorInDB.Status = authorRequest.Status;

            await _authorRepository.CommitAsync();

            return Ok(new
            {
                msg = "Update author Successfully"
            });
        }

        // DELETE / categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var authorInDB = await _authorRepository.GetOneAsync(e => e.Id == id);

            if (authorInDB is null)
                return NotFound();
            
            _authorRepository.Delete(authorInDB);
            await _authorRepository.CommitAsync();

            return Ok(new
            {
                msg = "Delete author Successfully"
            });
        }
    }
}
