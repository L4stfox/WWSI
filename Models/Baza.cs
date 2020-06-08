using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Zaginieni.Models;

namespace Zaginieni.DAL
{
	public class Baza : IdentityDbContext<ApplicationUser>
	{
		public Baza() : base("name=ZaginieniContext")
		{

		}

		public static Baza Create()
		{
			return new Baza();
		}

		public DbSet<Info> Info { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}