using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
	public class UserViewModel
	{
		[StringLength(30), Required(ErrorMessage = "Name cannot be empty")]
		public string Name { get; set; }

		[StringLength(30), Required(ErrorMessage = "Surname cannot be empty")]
		public string Surname { get; set; }

		[StringLength(30), EmailAddress, Required(ErrorMessage = "Email cannot be empty")]
		public string Email { get; set; }

		[StringLength(30), Phone, Required(ErrorMessage = "Phone cannot be empty")]
		public string Phone { get; set; }
	}
}
