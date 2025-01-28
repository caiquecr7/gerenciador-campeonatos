using GerenciadorCampeonatos.Domain.Interfaces.Services;
using GerenciadorCampeonatos.Domain.Requests.PlayerRequests;
using GerenciadorCampeonatos.Domain.Results.PlayerResults;
using GerenciadorCampeonatos.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GerenciadorCampeonatos.WebApi.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
[SwaggerResponse(StatusCodes.Status400BadRequest, "The request is invalid.")]
[SwaggerResponse(StatusCodes.Status401Unauthorized, "Your token has expired or you have not entered one.")]
[SwaggerResponse(StatusCodes.Status409Conflict, "A conflict occurred while processing the request.")]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "An internal server error occurred.")]
[SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "The service is unavailable.")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new player")]
    [SwaggerResponse(StatusCodes.Status201Created, "Player created successfully", typeof(PlayerResult))]
    public async Task<IActionResult> Create([FromBody] IncludePlayerRequest playerModel)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPlayer = await _playerService.Create(playerModel);

            return CreatedAtAction(nameof(GetById), new { id = createdPlayer.Id }, createdPlayer);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a player by ID")]
    [SwaggerResponse(StatusCodes.Status200OK, "Player retrieved successfully", typeof(PlayerResult))]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var player = await _playerService.GetById(id);

            if (player == null)
                return NotFound($"Player with ID {id} not found");

            return Ok(player);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all players")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of players retrieved successfully", typeof(IEnumerable<PlayerResult>))]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var players = await _playerService.GetAll();
            return Ok(players);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a player")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Player updated successfully")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePlayerRequest updatedPlayer)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _playerService.Update(id, updatedPlayer);
            if (!success)
                return NotFound(new { Message = "Player not found" });

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }

    [HttpGet("search")]
    [SwaggerOperation(Summary = "Search players with pagination, filtering, and sorting")]
    [SwaggerResponse(StatusCodes.Status200OK, "Search results retrieved successfully", typeof(IEnumerable<PlayerResult>))]
    public async Task<IActionResult> Search([FromQuery] SearchPlayerRequest searchRequest)
    {
        try
        {
            var result = await _playerService.Search(searchRequest);
            Response.AddPagedResultHeaders(result);
            return Ok(result.Data.ToArray());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }
}
