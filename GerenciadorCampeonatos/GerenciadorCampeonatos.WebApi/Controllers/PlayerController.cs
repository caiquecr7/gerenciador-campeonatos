using GerenciadorCampeonatos.Domain.Interfaces.Services;
using GerenciadorCampeonatos.Domain.Models.PlayerModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCampeonatos.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    /// <summary>
    /// Create a new player
    /// </summary>
    /// <param name="playerModel">Object containing the information of the player to be created</param>
    /// <returns>The player created or validation errors</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] IncludePlayerModel playerModel)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPlayer = await _playerService.Create(playerModel);

            if (createdPlayer == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating player.");

            return CreatedAtAction(nameof(GetById), new { id = createdPlayer.Id }, createdPlayer);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Returns a player by ID
    /// </summary>
    /// <param name="id">Player Id</param>
    /// <returns>The player found or error 404 if it does not exist</returns>
    [HttpGet("{id}")]
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

    /// <summary>
    /// Returns all players
    /// </summary>
    /// <returns>All players presents on database</returns>
    [HttpGet]
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

    /// <summary>
    /// Update a player
    /// </summary>
    /// <param name="id">Player to be updated</param>
    /// <param name="updatedPlayer">Updated player</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePlayerModel updatedPlayer)
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
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }
}
