using BusinessEngine.Commands.RoleCommand;
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
			
			return BadRequest("Ошибка при создании новой роли");
		}

		[HttpGet("/roles/get-all")]
		public async Task<IActionResult> GetAllRoles()
		{
			var commandGetAllRoles = new GetAllRolesCommand { };
			
			var result = await _mediator.Send(commandGetAllRoles);

			return result.ToArray().Length > 0 ? Ok(result) : Ok("Нет результатов");
		}

		[HttpDelete("/roles/{roleCode}/delete")]
		public async Task<IActionResult> DeleteRole(string roleCode)
		{
			var commandToDeleteRole = new DeleteRoleByCodeCommand {RoleCode = roleCode};
			
			var result = await _mediator.Send(commandToDeleteRole);

			return Ok(result ? "Успешно удалено" : "Удаление не выполнено успешно");
		}

		[HttpPut("roles/{roleCode}/update")]
		public async Task<IActionResult> UpdateRole(string roleCode, [FromBody] RoleForUpdateDtoModel roleForUpdate)
		{
			var commandToUpdateRole = new UpdateRoleCommand {RoleCode = roleCode, roleForUpdateDtoModel = roleForUpdate};
			
			var result = await _mediator.Send(commandToUpdateRole);
			
			if (result != null) return Ok(result);
			
			return BadRequest("При обновлении возникла ошибка");
		}
	}
}
