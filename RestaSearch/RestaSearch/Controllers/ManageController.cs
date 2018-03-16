using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RestaSearch.App_Start;
using RestaSearch.DAL;
using RestaSearch.Infrastructure;
using RestaSearch.Models;
using RestaSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RestaSearch.Controllers
{
	public class ManageController : Controller
	{
		private RSContext db = new RSContext();

		// GET: Manage
		public enum ManageMessageId
		{
			ChangePasswordSuccess,
			Error
		}
		private ApplicationUserManager _userManager;
		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}
		[Authorize]
		public async Task<ActionResult> Index(ManageMessageId? message)
		{
			//var name = User.Identity.Name;
			// logger.Info("Admin główna | " + name);

			//if (TempData["ViewData"] != null)
			//{
			//	ViewData = (ViewDataDictionary)TempData["ViewData"];
			//}

			//if (User.IsInRole("Admin"))
			//	ViewBag.UserIsAdmin = true;
			//else
			//	ViewBag.UserIsAdmin = false;

			var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
			if (user == null)
			{
				return View("Login", "Account");
			}

			var model = new ManageCredentialsViewModel
			{
				Message = message,
				DaneUzytkownika = user.DaneUzytkownika
			};

			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangeProfile([Bind(Prefix = "DaneUzytkownika")]DaneUzytkownika daneUzytkownika)
		{
			if (ModelState.IsValid)
			{
				var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
				user.DaneUzytkownika = daneUzytkownika;
				var result = await UserManager.UpdateAsync(user);

				AddErrors(result);
			}

			if (!ModelState.IsValid)
			{
				TempData["ViewData"] = ViewData;
				return RedirectToAction("Index");
			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")]ChangePasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				TempData["ViewData"] = ViewData;
				return RedirectToAction("Index");
			}

			var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
			if (result.Succeeded)
			{
				var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
				if (user != null)
				{
					await SignInAsync(user, isPersistent: false);
				}
				return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
			}
			AddErrors(result);

			if (!ModelState.IsValid)
			{
				TempData["ViewData"] = ViewData;
				return RedirectToAction("Index");
			}

			var message = ManageMessageId.ChangePasswordSuccess;
			return RedirectToAction("Index", new { Message = message });
		}

		private async Task SignInAsync(ApplicationUser user, bool isPersistent)
		{
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
			AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
		}


		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("password-error", error);
			}
		}

		[Authorize]
		public ActionResult DodajLokal(int? lokalId, bool? potwierdzenie)
		{
			Lokal lokal;
			var result = new EditLokalViewModel();
			if (lokalId.HasValue)
			{
				ViewBag.EditMode = true;
				lokal = db.Lokale.Find(lokalId);
				var kategorie = db.Kategorie.Where(x => x.LokalKategoria.Any(b => (lokalId == b.LokalId))).ToList();

				result.Kategorie1 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Rodzaj).ToList().Select(x => new KategoriaViewModel(x)).ToList();
				foreach (var k1 in result.Kategorie1){ foreach (var k2 in kategorie) { if (k1.Id==k2.KategoriaId) k1.Checked = true; }}
				result.Kategorie2 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Kuchnia).ToList().Select(x => new KategoriaViewModel(x)).ToList();
				foreach (var k1 in result.Kategorie2) { foreach (var k2 in kategorie) { if (k1.Id == k2.KategoriaId) k1.Checked = true; } }
				result.Kategorie3 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Danie).ToList().Select(x => new KategoriaViewModel(x)).ToList();
				foreach (var k1 in result.Kategorie3) { foreach (var k2 in kategorie) { if (k1.Id == k2.KategoriaId) k1.Checked = true; } }
				result.Kategorie4 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Inne).ToList().Select(x => new KategoriaViewModel(x)).ToList();
				foreach (var k1 in result.Kategorie4) { foreach (var k2 in kategorie) { if (k1.Id == k2.KategoriaId) k1.Checked = true; } }

			}
			else
			{
				ViewBag.EditMode = false;
				lokal = new Lokal();

				result.Kategorie1 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Rodzaj).ToList().Select(x => new KategoriaViewModel(x)).ToList();
				result.Kategorie2 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Kuchnia).ToList().Select(x => new KategoriaViewModel(x)).ToList();
				result.Kategorie3 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Danie).ToList().Select(x => new KategoriaViewModel(x)).ToList();
				result.Kategorie4 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Inne).ToList().Select(x => new KategoriaViewModel(x)).ToList();
			}
			//result.Miejscowosci = db.Miejscowosci.ToList();
			result.Lokal = lokal;
			result.Potwierdzenie = potwierdzenie;

			return View(result);

		}

		[HttpPost]
		[Authorize]
		public ActionResult DodajLokal(EditLokalViewModel model, HttpPostedFileBase file)
		{
			if (model.Lokal.LokalId > 0)
			{
				// modyfikacja produktu
				db.Entry(model.Lokal).State = EntityState.Modified;

				var CheckedKategorie = model.Kategorie1;
				CheckedKategorie.AddRange(model.Kategorie2);
				CheckedKategorie.AddRange(model.Kategorie3);
				CheckedKategorie.AddRange(model.Kategorie4);

				var lokalekategorie = db.LokaleKategorie.ToList();

				foreach (var lk in lokalekategorie)
				{
					foreach (var item in CheckedKategorie.Where(x => (x.Checked == false)))
					{
						if (lk.LokalId == model.Lokal.LokalId && item.Id == lk.KategoriaId)
						{
							LokalKategoria lokalUsun = db.LokaleKategorie.Find(lk.LokalKategoriaId);
							db.LokaleKategorie.Remove(lokalUsun);
							db.SaveChanges();
						}
					}
				}

				foreach (var item in CheckedKategorie.Where(x => x.Checked))
				{
					foreach (var lk in lokalekategorie)
					{
						if (item.Id == lk.KategoriaId && model.Lokal.LokalId == lk.LokalId)
							item.Checked = false;
					}
				}

				foreach (var item in CheckedKategorie.Where(x => x.Checked))
				{
					var lokalKategoria = new LokalKategoria();
					lokalKategoria.KategoriaId = item.Id;
					lokalKategoria.LokalId = model.Lokal.LokalId;
					db.LokaleKategorie.Add(lokalKategoria);
				}
				db.SaveChanges();
				return RedirectToAction("DodajLokal", new { lokalId = model.Lokal.LokalId, potwierdzenie = true });
			}
			else
			{
				// Sprawdzenie, czy użytkownik wybrał plik
				if (file != null && file.ContentLength > 0)
				{
					if (ModelState.IsValid)
					{
						// Generowanie pliku
						var fileExt = Path.GetExtension(file.FileName);
						var filename = Guid.NewGuid() + fileExt;

						var path = Path.Combine(Server.MapPath(AppConfig.ObrazkiLokaliFolderWzgledny), filename);
						file.SaveAs(path);

						model.Lokal.NazwaPlikuObrazka = filename;
						model.Lokal.DataDodania = DateTime.Now;

						model.Lokal.UserId = User.Identity.GetUserId();

						model.Lokal.Ukryty = true;
						model.Lokal.StatusLokalu = StatusLokalu.Nowy;
						model.Lokal.Wyswietlenia = 0;

						db.Lokale.Add(model.Lokal);

						var CheckedKategorie = model.Kategorie1;
						CheckedKategorie.AddRange(model.Kategorie2);
						CheckedKategorie.AddRange(model.Kategorie3);
						CheckedKategorie.AddRange(model.Kategorie4);

						foreach (var item in CheckedKategorie.Where(x => x.Checked))
						{
							var lokalKategoria = new LokalKategoria();
							lokalKategoria.KategoriaId = item.Id;
							lokalKategoria.LokalId = model.Lokal.LokalId;
							db.LokaleKategorie.Add(lokalKategoria);
						}


						db.SaveChanges();

						return RedirectToAction("DodajLokal", new { potwierdzenie = true });
					}
					else
					{

						//var kategorie = db.Kategorie.ToList();
						model.Kategorie1 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Rodzaj).ToList().Select(x => new KategoriaViewModel(x)).ToList();
						model.Kategorie2 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Kuchnia).ToList().Select(x => new KategoriaViewModel(x)).ToList();
						model.Kategorie3 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Danie).ToList().Select(x => new KategoriaViewModel(x)).ToList();
						model.Kategorie4 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Inne).ToList().Select(x => new KategoriaViewModel(x)).ToList();
						//var miejscowosci = db.Miejscowosci.ToList();
						//model.Miejscowosci = miejscowosci;

						return View(model);
					}
				}
				else
				{
					ModelState.AddModelError("", "Nie wskazano pliku");

					//var kategorie = db.Kategorie.ToList();
					model.Kategorie1 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Rodzaj).ToList().Select(x => new KategoriaViewModel(x)).ToList();
					model.Kategorie2 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Kuchnia).ToList().Select(x => new KategoriaViewModel(x)).ToList();
					model.Kategorie3 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Danie).ToList().Select(x => new KategoriaViewModel(x)).ToList();
					model.Kategorie4 = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Inne).ToList().Select(x => new KategoriaViewModel(x)).ToList();
					//var miejscowosci = db.Miejscowosci.ToList();
					//model.Miejscowosci = miejscowosci;
					return View(model);
				}
			}

		}
		[Authorize]
		public ActionResult ListaLokali()
		{
			//var name = User.Identity.Name;


			bool isAdmin = User.IsInRole("Admin");
			ViewBag.UserIsAdmin = isAdmin;

			List<Lokal> lokaleUzytkownika;

			// Dla administratora zwracamy wszystkie zamowienia
			if (isAdmin)
			{
				lokaleUzytkownika = db.Lokale.OrderByDescending(o => o.DataDodania).ToList();
			}
			else
			{
				var userId = User.Identity.GetUserId();
				lokaleUzytkownika = db.Lokale.Where(o => o.UserId == userId).OrderByDescending(o => o.DataDodania).ToList();
			}

			return View(lokaleUzytkownika);
		}
		[Authorize(Roles = "Admin")]
		public ActionResult ZmianaStanuLokalu(int lokalId, StatusLokalu status)
		{
			Lokal lokalDoModyfikacji = db.Lokale.Find(lokalId);
			lokalDoModyfikacji.StatusLokalu = status;
			db.SaveChanges();

			return RedirectToAction("ListaLokali");
		}

		[Authorize]
		public ActionResult UkryjLokal(int lokalId)
		{
			var lokal = db.Lokale.Find(lokalId);
			lokal.Ukryty = true;
			db.SaveChanges();

			return RedirectToAction("ListaLokali");
		}

		[Authorize]
		public ActionResult PokazLokal(int lokalId)
		{
			var lokal = db.Lokale.Find(lokalId);
			lokal.Ukryty = false;
			db.SaveChanges();

			return RedirectToAction("ListaLokali");
		}

		[Authorize(Roles = "Admin")]
		public ActionResult ListaKategorii()
		{
			IEnumerable<Kategoria> listakategorii = db.Kategorie.ToArray();
			return View(listakategorii);
		}

		//[HttpPost]
		//[Authorize(Roles = "Admin")]
		//public ActionResult DodajKategorie()
		//{
		//	var result = db.Kategorie.ToArray(); ;
		//	return View(result);
		//}

		[Authorize(Roles = "Admin")]
		public ActionResult DodajKategorie(int? kategoriaId, bool? potwierdzenie)
		{
			Kategoria kategoria;
			if (kategoriaId.HasValue)
			{
				ViewBag.EditMode = true;
				kategoria = db.Kategorie.Find(kategoriaId);
			}
			else
			{
				ViewBag.EditMode = false;
				kategoria = new Kategoria();
			}

			var result = new EditKategoriaViewModel();
			result.kategoria = kategoria;
			result.Potwierdzenie = potwierdzenie;

			return View(result);

		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult DodajKategorie(EditKategoriaViewModel model)
		{
			bool isAdmin = User.IsInRole("Admin");
			ViewBag.UserIsAdmin = isAdmin;

			if (model.kategoria.KategoriaId > 0)
			{
				// modyfikacja kategori
				db.Entry(model.kategoria).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("DodajKategorie", new { potwierdzenie = true });
			}
			else
			{
				if (ModelState.IsValid)
				{
					model.kategoria.Ukryty = true;
					db.Kategorie.Add(model.kategoria);
					db.SaveChanges();

					return RedirectToAction("DodajKategorie", new { kategoriaId = model.kategoria.KategoriaId, potwierdzenie = true });
				}
				else
				{
					return View(model);
				}
			}
		}
	}
}