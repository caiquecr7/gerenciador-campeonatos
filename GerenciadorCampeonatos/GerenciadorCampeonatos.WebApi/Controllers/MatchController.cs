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
[SwaggerResponse(StatusCodes.Status500InternalServerError, "An internal server error occurred.")]
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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdMatch = await _matchService.Create(MatchModel);

        return CreatedAtAction(nameof(GetById), new { id = createdMatch.Id }, createdMatch);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a match by ID", Description = "Retrieves the details of a match using its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "The match was retrieved successfully.", typeof(MatchResult))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The match with the specified ID was not found.")]
    public async Task<IActionResult> GetById(int id)
    {
        var Match = await _matchService.GetById(id);

        if (Match == null)
            return NotFound($"Match with ID {id} not found");

        return Ok(Match);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all matches", Description = "Retrieves a list of all matches available in the database.")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of matches was retrieved successfully.", typeof(IEnumerable<MatchResult>))]
    public async Task<IActionResult> GetAll()
    {
        var Matchs = await _matchService.GetAll();
        return Ok(Matchs);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a match", Description = "Updates the details of a match with the specified ID.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The match was updated successfully.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The match with the specified ID was not found.")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMatchRequest updatedMatch)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _matchService.Update(id, updatedMatch);
        if (!success)
            return NotFound(new { Message = "Match not found" });

        return NoContent();
    }

    [HttpGet("search")]
    [SwaggerOperation(Summary = "Search matches", Description = "Searches for matches based on filters, sorting, and pagination.")]
    [SwaggerResponse(StatusCodes.Status200OK, "The search results were retrieved successfully.", typeof(IEnumerable<MatchResult>))]
    public async Task<IActionResult> Search([FromQuery] SearchMatchRequest searchRequest)
    {
        var result = await _matchService.Search(searchRequest);
        Response.AddPagedResultHeaders(result);
        return Ok(result.Data.ToArray());
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete a match", Description = "Deletes a match with the specified ID.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The match was deleted successfully.")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _matchService.Delete(id);
        if (!success)
            return NotFound(new { Message = "Match not found" });

        return NoContent();
    }
}
