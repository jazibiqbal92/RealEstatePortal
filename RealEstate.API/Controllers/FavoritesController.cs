using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using RealEstate.Domain;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly IFavoriteService _favoriteService;

    public FavoritesController(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetFavorites()
    {
        try
        {
            // Double check token
            if (!(HttpContext.Items["userId"] is int userId))
                return Unauthorized(new { message = "User is not authenticated." });

            var favorites = await _favoriteService.GetByUserIdAsync(userId);

            if (favorites == null || !favorites.Any())
                return NotFound(new { message = "No favorites found." });

            return Ok(favorites);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "An error occurred while fetching favorites.",
                error = ex.Message
            });
        }
    }


    [HttpPost("{propertyId}")]
    public async Task<IActionResult> AddFavorite(int propertyId)
    {
        try
        {
            // Double check token
            if (!(HttpContext.Items["userId"] is int userId))
                return Unauthorized(new { message = "User is not authenticated." });

            FavoriteCreateDto dto = new FavoriteCreateDto() { UserId = userId, PropertyId = propertyId };
            var result = await _favoriteService.AddAsync(dto);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return CreatedAtAction(nameof(GetFavorites), null, result.Data);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while adding favorite.", error = ex.Message });
        }
    }

    [HttpDelete("{propertyId}")]
    public async Task<IActionResult> RemoveFavorite(int propertyId)
    {
        try
        {
            // Double check token
            if (!(HttpContext.Items["userId"] is int userId))
                return Unauthorized(new { message = "User is not authenticated." });
            await _favoriteService.RemoveAsync(userId, propertyId);

            return Ok(new { message = "Property removed from Favourites" });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while removing favorite.", error = ex.Message });
        }
    }
}
