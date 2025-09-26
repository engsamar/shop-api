

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ShopApi.Areas.Customer.Controllers;

[Area(SD.CustomerArea)]
[Route("api/[area]/[controller]")]
[ApiController]
[Authorize]

public class FavouritesController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRepository<Favourit> _favouritRepository;

    public FavouritesController(UserManager<ApplicationUser> userManager, IRepository<Favourit> favouriteRepository)
    {
        _userManager = userManager;
        _favouritRepository = favouriteRepository;
    }

    [HttpPost("")]
    public async Task<IActionResult> AddToFavourite(FavouriteRequest favouriteRequest)
    {
        string msg = "Add Product To Favourites Successfully";
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
            return NotFound();

        var favourit = await _favouritRepository.GetOneAsync(e => e.ApplicationUserId == user.Id && e.ProductId == favouriteRequest.ProductId);

        if (favourit is not null)
        {
            //delete
            _favouritRepository.Delete(favourit);
            msg = "Remove Product From Favourites Successfully";
        }
        else
        {
            await _favouritRepository.CreateAsync(new()
            {
                ApplicationUserId = user.Id,
                ProductId = favouriteRequest.ProductId,
            });
        }

        await _favouritRepository.CommitAsync();
        
        return Ok(new
        {
            msg = msg
        });
    }
    
    [HttpGet("")] 
    public async Task<IActionResult> Index(string? code = null)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
            return NotFound();

        var favourites = await _favouritRepository.GetAsync(e => e.ApplicationUserId == user.Id, includes: [e => e.Product]);
        
        string msg = "";
   
        return Ok(new
        {
            favourites,
            msg
        });
    }
    
}