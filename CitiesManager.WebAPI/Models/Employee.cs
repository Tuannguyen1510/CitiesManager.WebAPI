using System.ComponentModel.DataAnnotations;

namespace CitiesManager.WebAPI.Models
{
	public class Employee
	{
		[Key]
		public Guid EmployeeId {  get; set; }
		[Required(ErrorMessage = "EmployeeName can not be blank")]
		public string EmployeeName { get; set; }
		public DateTime DateOfJoining { get; set; }
		public string? PhotoFileName { get; set; }

	}
}
