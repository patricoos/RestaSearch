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
		public async Task<ActionResult> Index(ManageMessageId? message)
		{
			var name = User.Identity.Name;
			// logger.Info("Admin główna | " + name);

			if (TempData["ViewData"] != null)
			{
				ViewData = (ViewDataDictionary)TempData["ViewData"];
			}

			if (User.IsInRole("Admin"))
				ViewBag.UserIsAdmin = true;
			else
				ViewBag.UserIsAdmin = false;

			var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
			if (user == null)
			{
				return View("Error");
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

		public ActionResult DodajLokal(int? lokalId, bool? potwierdzenie)
		{
			Lokal lokal;
			if (lokalId.HasValue)
			{
				ViewBag.EditMode = true;
				lokal = db.Lokale.Find(lokalId);
			}
			else
			{
				ViewBag.EditMode = false;
				lokal = new Lokal();
			}

			var result = new EditLokalViewModel();
			result.Kategorie = db.Kategorie.ToList();
			result.Miejscowosci = db.Miejscowosci.ToList();
			result.Lokal = lokal;
			result.Potwierdzenie = potwierdzenie;

			return View(result);
		}

		[HttpPost]

		public ActionResult DodajLokal(EditLokalViewModel model, HttpPostedFileBase file)
		{
			if (model.Lokal.LokalId > 0)
			{
				// modyfikacja produktu
				db.Entry(model.Lokal).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("DodajLokal", new { potwierdzenie = true });
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

						///////////////////////////////////////////////////////////////////////////////////////  Zmienic na true !! .Ukryty = false;

						model.Lokal.Ukryty = false;
						model.Lokal.Promowany = false;
						model.Lokal.Wyswietlenia = 0;

						db.Entry(model.Lokal).State = EntityState.Added;
						db.SaveChanges();

						return RedirectToAction("DodajLokal", new { potwierdzenie = true });
					}
					else
					{
						var kategorie = db.Kategorie.ToList();
						model.Kategorie = kategorie;
						var miejscowosci = db.Miejscowosci.ToList();
						model.Miejscowosci = miejscowosci;

						return View(model);
					}
				}
				else
				{
					ModelState.AddModelError("", "Nie wskazano pliku");
					var kategorie = db.Kategorie.ToList();
					model.Kategorie = kategorie;
					var miejscowosci = db.Miejscowosci.ToList();
					model.Miejscowosci = miejscowosci;
					return View(model);
				}
			}

		}

		[Authorize(Roles = "Admin")]
		public ActionResult UkryjLokal(int lokalId)
		{
			var lokal = db.Lokale.Find(lokalId);
			lokal.Ukryty = true;
			db.SaveChanges();

			return RedirectToAction("DodajLokal", new { potwierdzenie = true });
		}

		[Authorize(Roles = "Admin")]
		public ActionResult PokazLokal(int lokalId)
		{
			var lokal = db.Lokale.Find(lokalId);
			lokal.Ukryty = false;
			db.SaveChanges();

			return RedirectToAction("DodajLokal", new { potwierdzenie = true });
		}

		[Authorize(Roles = "Admin")]
		public ActionResult PromoLokal(int lokalId)
		{
			var lokal = db.Lokale.Find(lokalId);
			lokal.Promowany = true;
			db.SaveChanges();

			return RedirectToAction("DodajLokal", new { potwierdzenie = true });
		}
		[Authorize(Roles = "Admin")]
		public ActionResult NiePromoLokal(int lokalId)
		{
			var lokal = db.Lokale.Find(lokalId);
			lokal.Promowany = false;
			db.SaveChanges();

			return RedirectToAction("DodajLokal", new { potwierdzenie = true });
		}
	}
}