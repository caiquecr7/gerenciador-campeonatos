using GerenciadorCampeonatos.Domain.Interfaces.Services;
using GerenciadorCampeonatos.WebApi.Models.TeamModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCampeonatos.WebApi.Controllers
{
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
        public async Task<IActionResult> CreateTeam([FromBody] IncludeTeamModel teamModel)
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
            var team = await _teamService.GetById(id);

            if (team == null)
                return NotFound($"Team with ID {id} not found");

            return Ok(team);
        }

    }
}
