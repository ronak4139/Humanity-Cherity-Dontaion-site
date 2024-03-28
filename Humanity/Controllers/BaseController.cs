using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Humanity.Controllers
{
	public class BaseController : Controller
	{
		protected bool IsAdminAuthenticated()
		{
			return HttpContext.Request.Cookies.ContainsKey(".AspNetCore.Cookies");
		}
	}
}
