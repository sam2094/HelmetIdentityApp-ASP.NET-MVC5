using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
	public class UserViewModelResult
	{
		public string Message { get; set; }
		public UserViewModel UserViewModel { get; set; }
	}
}
