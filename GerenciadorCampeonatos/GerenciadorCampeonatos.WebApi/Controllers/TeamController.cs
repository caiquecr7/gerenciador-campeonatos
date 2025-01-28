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
[SwaggerResponse(StatusCodes.Status409Conflict, "A conflict occurred while processing the request.")]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "An internal server error occurred.")]
[SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "The service is unavailable.")]
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
    public async Task<IActionResult> Create([FromBody] IncludeTeamRequest teamModel)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTeam = await _teamService.Create(teamModel);

            if (createdTeam == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating team.");

            return CreatedAtAction(nameof(GetById), new { id = createdTeam.Id }, createdTeam);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a team by ID", Description = "Retrieves the details of a team using its unique ID.")]
    [SwaggerResponse(StatusCodes.Status200OK, "The team was retrieved successfully.", typeof(TeamResult))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The team with the specified ID was not found.")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var team = await _teamService.GetById(id);

            if (team == null)
                return NotFound($"Team with ID {id} not found");

            return Ok(team);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all teams", Description = "Retrieves a list of all teams available in the database.")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of teams was retrieved successfully.", typeof(IEnumerable<TeamResult>))]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var teams = await _teamService.GetAll();
            return Ok(teams);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Update a team
    /// </summary>
    /// <param name="id">Team to be updated</param>
    /// <param name="updatedTeam">Updated team</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a team", Description = "Updates the details of a team with the specified ID.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The team was updated successfully.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The team with the specified ID was not found.")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTeamRequest updatedTeam)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _teamService.Update(id, updatedTeam);
            if (!success)
                return NotFound(new { Message = "Team not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }

    [HttpGet("search")]
    [SwaggerOperation(Summary = "Search for teams", Description = "Searches for teams based on filters, sorting, and pagination.")]
    [SwaggerResponse(StatusCodes.Status200OK, "The search results were retrieved successfully.", typeof(IEnumerable<TeamResult>))]
    public async Task<IActionResult> Search([FromQuery] SearchTeamRequest searchRequest)
    {
        try
        {
            var result = await _teamService.Search(searchRequest);
            Response.AddPagedResultHeaders(result);
            return Ok(result.Data.ToArray());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
        }
    }
}
