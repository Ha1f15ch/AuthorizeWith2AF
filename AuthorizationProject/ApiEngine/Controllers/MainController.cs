using BusinessEngine.Commands;
using BusinessEngine.Handlers.BaseHandler;
using BusinessEngine.Services.Interfaces;
using DTO.RoleModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiEngine.Controllers
{
	[Route("api/v1/main")]
	[ApiController]
	public class MainController : ControllerBase
	{
		private readonly ICommandDispatcher _commandDispatcher;

		public MainController(ICommandDispatcher commandDispatcher)
		{
			_commandDispatcher = commandDispatcher;
		}

		[HttpPost("/roles/create")]
		public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand? command)
		{
			if (command == null)
				return BadRequest("Invalid request");

			var result = await _commandDispatcher.SendAsync<CreateRoleCommand, ResponseRoleDtoModel>(command);
			return Ok(result);
		}
	}
}
