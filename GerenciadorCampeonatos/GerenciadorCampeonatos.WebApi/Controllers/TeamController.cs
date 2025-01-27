using GerenciadorCampeonatos.Domain.Interfaces.Services;
using GerenciadorCampeonatos.Domain.Requests.TeamRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCampeonatos.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    /// <summary>
    /// Create a new team
    /// </summary>
    /// <param name="teamModel">Object containing the information of the team to be created</param>
    /// <returns>The team created or validation errors</returns>
    [HttpPost]
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

    /// <summary>
    /// Returns a team by ID
    /// </summary>
    /// <param name="id">Team Id</param>
    /// <returns>The team found or error 404 if it does not exist</returns>
    [HttpGet("{id}")]
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

    /// <summary>
    /// Returns all teams
    /// </summary>
    /// <returns>All teams presents on database</returns>
    [HttpGet]
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
}
