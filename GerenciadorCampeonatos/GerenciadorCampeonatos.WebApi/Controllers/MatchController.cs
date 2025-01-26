using GerenciadorCampeonatos.Domain.Interfaces.Services;
using GerenciadorCampeonatos.Domain.Models.MatchModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCampeonatos.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class MatchController : ControllerBase
{
    private readonly IMatchService _matchService;

    public MatchController(IMatchService MatchService)
    {
        _matchService = MatchService;
    }

    /// <summary>
    /// Create a new Match
    /// </summary>
    /// <param name="MatchModel">Object containing the information of the Match to be created</param>
    /// <returns>The Match created or validation errors</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] IncludeMatchModel MatchModel)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdMatch = await _matchService.Create(MatchModel);

            if (createdMatch == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating Match.");

            return CreatedAtAction(nameof(GetById), new { id = createdMatch.Id }, createdMatch);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Returns a Match by ID
    /// </summary>
    /// <param name="id">Match Id</param>
    /// <returns>The Match found or error 404 if it does not exist</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var Match = await _matchService.GetById(id);

            if (Match == null)
                return NotFound($"Match with ID {id} not found");

            return Ok(Match);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Returns all Matchs
    /// </summary>
    /// <returns>All Matchs presents on database</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var Matchs = await _matchService.GetAll();
            return Ok(Matchs);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Update a Match
    /// </summary>
    /// <param name="id">Match to be updated</param>
    /// <param name="updatedMatch">Updated Match</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMatchModel updatedMatch)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _matchService.Update(id, updatedMatch);
            if (!success)
                return NotFound(new { Message = "Match not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }
}
