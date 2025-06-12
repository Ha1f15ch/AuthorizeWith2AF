using BusinessEngine.Commands.RoleCommand;
using BusinessEngine.Commands.UserCommand;
using DTO.RoleModels;
using DTO.UserModels;
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

		[HttpDelete("/roles/{roleId}/delete")]
		public async Task<IActionResult> DeleteRole(int roleId)
		{
			var commandToDeleteRole = new DeleteRoleByCodeCommand {RoleId = roleId};
			
			var result = await _mediator.Send(commandToDeleteRole);

			return Ok(result ? "Успешно удалено" : "Удаление не выполнено успешно");
		}

		[HttpPut("/roles/{roleId}/update")]
		public async Task<IActionResult> UpdateRole(int roleId, [FromBody] RoleForUpdateDtoModel roleForUpdate)
		{
			var commandToUpdateRole = new UpdateRoleCommand {RoleId = roleId, roleForUpdateDtoModel = roleForUpdate};
			
			var result = await _mediator.Send(commandToUpdateRole);
			
			if (result != null) return Ok(result);
			
			return BadRequest("При обновлении возникла ошибка");
		}

		[HttpPost("/sign-up")]
		public async Task<IActionResult> Registration([FromBody] UserRegistrationDtoModel userRegistrationDtoModel)
		{
			var commandToRegistration = new UserRegistrationCommand
			{
				UserName = userRegistrationDtoModel.UserName,
				UserEmail = userRegistrationDtoModel.UserEmail,
				Password = userRegistrationDtoModel.Password
			};
			
			var resultRegistration = await _mediator.Send(commandToRegistration);
			
			return Ok("Вы зарегистрированы !!!");
		}
	}
}
