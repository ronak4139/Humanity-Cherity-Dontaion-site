//using Humanity.Data;
//using Humanity.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace Humanity.Controllers
//{
//	public class AdminController : Controller
//	{
//		UserModel empObj = new UserModel();
//		public IActionResult Index()
//		{
//			empObj = new UserModel();
//			List<UserModel> lst = empObj.getData();
//			return View(lst);
//		}

//		ContactusDataModel ctObj = new ContactusDataModel();
//		public IActionResult contactus()
//		{
//			ctObj = new ContactusDataModel();
//			List<ContactusDataModel> lst = ctObj.getDatacontactus();
//			return View(lst);
//		}

//		VolunteerDataModel vtObj = new VolunteerDataModel();
//		public IActionResult volunteer()
//		{
//			vtObj = new VolunteerDataModel();
//			List<VolunteerDataModel> lst = vtObj.getDatavolunteer();
//			return View(lst);
//		}

//	}
//}

	using Humanity.Data;
	using Humanity.Models;
	using Microsoft.AspNetCore.Mvc;

	namespace Humanity.Controllers
	{
		public class AdminController : BaseController
		{
			UserModel empObj = new UserModel();
			public IActionResult Index()
			{
				if (!IsAdminAuthenticated())
				{
					return RedirectToAction("AdminLogin", "UserControlller");
				}
				else
				{
					empObj = new UserModel();
					List<UserModel> lst = empObj.getData();
					return View(lst);
				}
			}

			ContactusDataModel ctObj = new ContactusDataModel();
			public IActionResult contactus()
			{
				if (!IsAdminAuthenticated())
				{
					return RedirectToAction("AdminLogin", "UserControlller");
				}

				ctObj = new ContactusDataModel();
				List<ContactusDataModel> lst = ctObj.getDatacontactus();
				return View(lst);
			}

			VolunteerDataModel vtObj = new VolunteerDataModel();
			public IActionResult volunteer()
			{
				if (!IsAdminAuthenticated())
				{
					return RedirectToAction("AdminLogin", "UserControlller");
				}

				vtObj = new VolunteerDataModel();
				List<VolunteerDataModel> lst = vtObj.getDatavolunteer();
				return View(lst);
			}
		}
	}

