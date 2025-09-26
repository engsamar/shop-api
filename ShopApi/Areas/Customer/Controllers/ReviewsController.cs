

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace ShopApi.Areas.Customer.Controllers;
[Area(SD.CustomerArea)]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize]

public class ReviewsController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRepository<Review> _reviewRepository;

    public ReviewsController(UserManager<ApplicationUser> userManager, IRepository<Review> reviewRepository)
    {
        _userManager = userManager;
        _reviewRepository = reviewRepository;
    }

    [HttpPost("")]
    public async Task<IActionResult> AddReviews(ReviewRequest reviewRequest)
    {
        string msg = "Add Review To Product Successfully";
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
            return NotFound();

        var favourit = await _reviewRepository.GetOneAsync(e => e.ApplicationUserId == user.Id && e.ProductId == reviewRequest.ProductId);

        if (favourit is not null)
        {
            msg = "Already added review to this item";
        }
        else
        {
            await _reviewRepository.CreateAsync(new()
            {
                ApplicationUserId = user.Id,
                ProductId = reviewRequest.ProductId,
                Note =  reviewRequest.Note,
                Rate = reviewRequest.Rate
            });
            await _reviewRepository.CommitAsync();
        }
        
        return Ok(new
        {
            msg = msg
        });
    }
    
}