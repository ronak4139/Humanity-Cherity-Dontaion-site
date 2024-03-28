using Humanity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Humanity.Controllers
{
    public class UserControlller : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<UserControlller> _logger;


		public UserControlller(ILogger<UserControlller> logger, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _logger = logger;
		}
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View("Logout");
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult signup()
        {
            return View();
        }

        public IActionResult adminlogin()
        {
            return View();
        }

		//public string Email { get; set; }
		//public string Password { get; set; }

  //      [HttpPost]  
		//public IActionResult adminlogin()
  //      {
		//	SqlConnection con = new SqlConnection(@"Server=RONAK;Database=Humanity;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
		//	string query = @"
  //              SELECT Email,Password
  //              FROM dbo.Admin
  //              WHERE Email = @Email AND Password = @Password;
  //          ";

		//	using (SqlCommand command = new SqlCommand(query, con))
		//	{
		//		command.Parameters.AddWithValue("@Email", Email);
		//		command.Parameters.AddWithValue("@PasswordHash", Password);

		//		con.Open();
		//		SqlDataReader reader = command.ExecuteReader();

		//		if (reader.Read())
		//		{
		//			// Login successful, retrieve user information

		//			TempData["msg"] = "Welcome Admin to Admin Panal";
		//			return View("Admin/Index");
		//			// Do something with the retrieved user information
		//		}
		//		else
		//		{
		//			TempData["msg"] = "Admin Creditional wrong";
		//			// Login failed, username/email or password incorrect
		//			return RedirectToAction("adminlogin");
		//		}

		//	}
  //      }
        public IActionResult home()
        {
            return View();
        }
        public IActionResult aboutus()
        {
            return View();
        }
        public IActionResult blogsingle()
        {
            return View();
        }
        public IActionResult causessingle()
        {
            return View();
        }
        public IActionResult causes()
        {
            return View();
        }
        public IActionResult donationnow()
        {
            return View();
        }
        
        public IActionResult team()
        {
            return View();
        }
        public IActionResult volunteer()
        {
            return View();
        }
        public IActionResult admindashboard()
        {
            return View();
        }
        public IActionResult admincontactus()
        {
            return View();
        }
        public IActionResult adminvolunteer()
        {
            return View();
        }
        public IActionResult contactus()
        {
            return View();
        }

        [HttpPost]
        public IActionResult contactus(ContactusDataModel usr)
        {
            if (ModelState.IsValid)
            {
                ContactusDataModel usrObj = new ContactusDataModel();
                bool res = usrObj.insertcontactus(usr);
                if (res)
                {
                    TempData["msg"] = "Added successfully";
                    return RedirectToAction("contactus");
                }
                else
                {
                    TempData["msg"] = "Not Added. Something went wrong..!!";
                }
                return View();
            }
            else
            {
                // Collect all validation errors and concatenate them into a single message
                string errorMessage = string.Join("; ", ModelState.Values
                                                        .SelectMany(v => v.Errors)
                                                        .Select(e => e.ErrorMessage));

                // Set the error message in TempData
                TempData["msg"] = errorMessage;
                return View();
            }
        }

        [HttpPost]
        public IActionResult volunteer(VolunteerDataModel vlt)
        {
            if (ModelState.IsValid)
            {
                VolunteerDataModel usrObj = new VolunteerDataModel();
                bool res = usrObj.insertvolunteer(vlt);
                if (res)
                {
                    TempData["msg"] = "Added successfully";
                    return RedirectToAction("volunteer");
                }
                else
                {
                    TempData["msg"] = "Not Added. Something went wrong..!!";
                }
                return View();
            }
            else
            {
                // Collect all validation errors and concatenate them into a single message
                string errorMessage = string.Join("; ", ModelState.Values
                                                        .SelectMany(v => v.Errors)
                                                        .Select(e => e.ErrorMessage));

                // Set the error message in TempData
                TempData["msg"] = errorMessage;
                return View();
            }
        }

		[HttpPost]
		public async Task<IActionResult> adminlogin(AdminModel admin)
		{
			if (ModelState.IsValid)
			{
				AdminModel adminObj = new AdminModel();
				bool res = adminObj.getadmin(admin);
				if (res)
				{
					// Login successful, set the authentication cookie
					var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, admin.Email),
                // Add any additional claims as needed
            };

					var claimsIdentity = new ClaimsIdentity(
						claims, CookieAuthenticationDefaults.AuthenticationScheme);

					var authProperties = new AuthenticationProperties
					{
						// You can customize the cookie options here
						ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
						IsPersistent = true,
						AllowRefresh = true
					};

					await HttpContext.SignInAsync(
						CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(claimsIdentity),
						authProperties);

					//TempData["msg"] = "Welcome Admin in Admin Panel";
					return RedirectToAction("Index", "Admin");
				}
				else
				{
					TempData["msg"] = "Admin Credentials are wrong";
					return RedirectToAction("adminlogin");
				}
			}
			else
			{
				// Collect all validation errors and concatenate them into a single message
				string errorMessage = string.Join("; ", ModelState.Values
														.SelectMany(v => v.Errors)
														.Select(e => e.ErrorMessage));

				// Set the error message in TempData
				TempData["msg"] = errorMessage;
				return View();
			}
		}

		public IActionResult adminLogout()
		{
            // Sign out the user
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			// Clear the cookies
			Response.Cookies.Delete(".AspNetCore.Cookies");

			// Redirect to the home page or any other page
			return RedirectToAction("adminlogin", "UserControlller");
		}
        public IActionResult Success()
        {
            return View();
        }

	}
}