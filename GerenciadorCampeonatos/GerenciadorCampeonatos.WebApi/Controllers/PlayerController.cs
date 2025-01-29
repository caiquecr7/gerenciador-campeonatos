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
[SwaggerResponse(StatusCodes.Status500InternalServerError, "An internal server error occurred.")]
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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdPlayer = await _playerService.Create(playerModel);

        return CreatedAtAction(nameof(GetById), new { id = createdPlayer.Id }, createdPlayer);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a player by ID")]
    [SwaggerResponse(StatusCodes.Status200OK, "Player retrieved successfully", typeof(PlayerResult))]
    public async Task<IActionResult> GetById(int id)
    {
        var player = await _playerService.GetById(id);

        if (player == null)
            return NotFound($"Player with ID {id} not found");

        return Ok(player);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Search players with pagination, filtering, and sorting")]
    [SwaggerResponse(StatusCodes.Status200OK, "Search results retrieved successfully", typeof(IEnumerable<PlayerResult>))]
    public async Task<IActionResult> Search([FromQuery] SearchPlayerRequest searchRequest)
    {
        var result = await _playerService.Search(searchRequest);
        Response.AddPagedResultHeaders(result);
        return Ok(result.Data.ToArray());
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a player")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Player updated successfully")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePlayerRequest updatedPlayer)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _playerService.Update(id, updatedPlayer);
        if (!success)
            return NotFound(new { Message = "Player not found" });

        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete a player")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Player deleted successfully")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _playerService.Delete(id);
        if (!success)
            return NotFound(new { Message = "Player not found" });

        return NoContent();
    }
}
