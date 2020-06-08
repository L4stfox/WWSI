using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zaginieni.Models
{
	[Table("Info")]
	public class Info
	{
		[Key]
		public int Id { get; set; }
		public string Imie { get; set; }
		public string Nazwisko { get; set; }
		public string Płeć { get; set; }
		public string Informacja { get; set; }
		public string Portret { get; set; }
	}
}