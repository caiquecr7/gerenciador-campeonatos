using GerenciadorCampeonatos.Domain.Interfaces.Services;
using GerenciadorCampeonatos.Domain.Requests.TeamRequests;
using GerenciadorCampeonatos.Domain.Results.TeamResults;
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
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new team", Description = "Creates a new team with the specified details.")]
    [SwaggerResponse(StatusCodes.Status201Created, "The team was created successfully.", typeof(TeamResult))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The request data is invalid.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "An error occurred while creating the team.")]
    public async Task<IActionResult> Create([FromBody] IncludeTeamRequest teamModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdTeam = await _teamService.Create(teamModel);

        if (createdTeam == null)
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating team.");

        return CreatedAtAction(nameof(GetById), new { id = createdTeam.Id }, createdTeam);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a team by ID", Description = "Retrieves the details of a team using its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "The team was retrieved successfully.", typeof(TeamResult))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The team with the specified ID was not found.")]
    public async Task<IActionResult> GetById(int id)
    {
        var team = await _teamService.GetById(id);

        if (team == null)
            return NotFound($"Team with ID {id} not found");

        return Ok(team);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Search for teams", Description = "Searches for teams based on filters, sorting, and pagination.")]
    [SwaggerResponse(StatusCodes.Status200OK, "The search results were retrieved successfully.", typeof(IEnumerable<TeamResult>))]
    public async Task<IActionResult> Search([FromQuery] SearchTeamRequest searchRequest)
    {
        var result = await _teamService.Search(searchRequest);
        Response.AddPagedResultHeaders(result);
        return Ok(result.Data.ToArray());
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a team", Description = "Updates the details of a team with the specified ID.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The team was updated successfully.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The team with the specified ID was not found.")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTeamRequest updatedTeam)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _teamService.Update(id, updatedTeam);
        if (!success)
            return NotFound(new { Message = "Team not found" });

        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete a team", Description = "Deletes a team with the specified ID.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The team was deleted successfully.")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _teamService.Delete(id);
        if (!success)
            return NotFound(new { Message = "Team not found" });

        return NoContent();
    }
}
