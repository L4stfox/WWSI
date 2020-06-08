using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zaginieni.Models
{
	public class LoginViewModel
	{
		[Required]
		[Display(Name = "Adres e-mail")]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Hasło")]
		public string Password { get; set; }
	}

	public class RegisterViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Adres e-mail")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "Nieprawidłowe {0}. {0} musi zawierać minimum {2} znaki.", MinimumLength = 4)]
		[DataType(DataType.Password)]
		[Display(Name = "Hasło")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Potwierdź hasło")]
		[Compare("Password", ErrorMessage = "Hasła różnią się.")]
		public string ConfirmPassword { get; set; }
	}
}