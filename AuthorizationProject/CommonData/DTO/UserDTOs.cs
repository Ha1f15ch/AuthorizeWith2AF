namespace DTO
{
	public class UserDTOs
	{
		public CreateUserDto CreateUser { get; set; }
		public GetUserDto GetUser { get; set; }
	}

	public class CreateUserDto 
	{
		public string UserName { get; set; }
		public string UserEmail { get; set; }
		public string Password { get; set; }
	}

	public class GetUserDto 
	{ 
		public int Id {  get; set; }
		public string UserName { get; set; }
		public string UserEmail { get; set; }
	}
}
