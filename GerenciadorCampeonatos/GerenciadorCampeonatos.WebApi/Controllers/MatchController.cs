using GerenciadorCampeonatos.Domain.Interfaces.Services;
using GerenciadorCampeonatos.Domain.Requests.MatchRequests;
using GerenciadorCampeonatos.Domain.Results.MatchResults;
using GerenciadorCampeonatos.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GerenciadorCampeonatos.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[SwaggerResponse(StatusCodes.Status400BadRequest, "The request is invalid.")]
[SwaggerResponse(StatusCodes.Status401Unauthorized, "Your token has expired or you have not entered one.")]
[SwaggerResponse(StatusCodes.Status409Conflict, "A conflict occurred while processing the request.")]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "An internal server error occurred.")]
[SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "The service is unavailable.")]
public class MatchController : ControllerBase
{
    private readonly IMatchService _matchService;

    public MatchController(IMatchService MatchService)
    {
        _matchService = MatchService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new match", Description = "Creates a new match with the specified details.")]
    [SwaggerResponse(StatusCodes.Status201Created, "The match was created successfully.", typeof(MatchResult))]
    public async Task<IActionResult> Create([FromBody] IncludeMatchRequest MatchModel)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdMatch = await _matchService.Create(MatchModel);

            return CreatedAtAction(nameof(GetById), new { id = createdMatch.Id }, createdMatch);
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
    [SwaggerOperation(Summary = "Get a match by ID", Description = "Retrieves the details of a match using its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "The match was retrieved successfully.", typeof(MatchResult))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The match with the specified ID was not found.")]
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

    [HttpGet]
    [SwaggerOperation(Summary = "Get all matches", Description = "Retrieves a list of all matches available in the database.")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of matches was retrieved successfully.", typeof(IEnumerable<MatchResult>))]
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

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a match", Description = "Updates the details of a match with the specified ID.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The match was updated successfully.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The match with the specified ID was not found.")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMatchRequest updatedMatch)
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
    [SwaggerOperation(Summary = "Search matches", Description = "Searches for matches based on filters, sorting, and pagination.")]
    [SwaggerResponse(StatusCodes.Status200OK, "The search results were retrieved successfully.", typeof(IEnumerable<MatchResult>))]
    public async Task<IActionResult> Search([FromQuery] SearchMatchRequest searchRequest)
    {
        try
        {
            var result = await _matchService.Search(searchRequest);
            Response.AddPagedResultHeaders(result);
            return Ok(result.Data.ToArray());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }
}
