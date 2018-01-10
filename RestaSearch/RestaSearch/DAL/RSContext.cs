using Microsoft.AspNet.Identity.EntityFramework;
using RestaSearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace RestaSearch.DAL
{
	public class RSContext : IdentityDbContext<ApplicationUser>
	{
		public RSContext() : base("RSContext")
		{
		}
		static RSContext()
		{
			Database.SetInitializer<RSContext>(new RSInitializer());
		}
		public static RSContext Create()
		{
			return new RSContext();
		}


		public virtual DbSet<Lokal> Lokale { get; set; }
		public virtual DbSet<Kategoria> Kategorie { get; set; }
		//public virtual DbSet<LokalKategoria> LokaleKategorie { get; set; }
		public virtual DbSet<Miejscowosc> Miejscowosci { get; set; }
		public virtual DbSet<Region> Regiony { get; set; }
		public virtual DbSet<Panstwo> Panstwa { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// using System.Data.Entity.ModelConfiguration.Conventions;
			// Wyłącza konwencję, która automatycznie tworzy liczbę mnogą dla nazw tabel w bazie danych
			// Zamiast Kategorie zostałaby stworzona tabela o nazwie Kategories
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}

	}
}