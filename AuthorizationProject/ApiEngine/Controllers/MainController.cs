using BusinessEngine.Commands;
using DTO.RoleModels;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace ApiEngine.Controllers
{
	[Route("api/v1/main")]
	[ApiController]
	public class MainController : ControllerBase
	{
		private readonly IMediator _mediator;

		public MainController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("/roles/create")]
		public async Task<IActionResult> CreateRole([FromBody] CreateRoleDtoModel createRoleDtoModel)
		{
			var commandToCreate = new CreateRoleCommand
			{
				RoleCode = createRoleDtoModel.RoleCode,
				Description = createRoleDtoModel.Description
			};
			
			var result = await _mediator.Send(commandToCreate);

			if (result != null)
			{
				return Ok(result);
			}
			
			return BadRequest("ошибка при создании новой роли");
		}
	}
}
