using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DajLapu.Api.Controllers
{
	public class BaseApiController : ApiController
	{
		private ApplicationUserManager _AppUserManager = null;

		protected ApplicationUserManager AppUserManager
		{
			get
			{
				return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
		}

		public BaseApiController()
		{
		}

		protected IHttpActionResult GetErrorResult(IdentityResult result)
		{
			if (result == null)
			{
				return InternalServerError();
			}

			if (!result.Succeeded)
			{
				if (result.Errors != null)
				{
					foreach (string error in result.Errors)
					{
						ModelState.AddModelError("", error);
					}
				}

				if (ModelState.IsValid)
				{
					// No ModelState errors are available to send, so just return an empty BadRequest.
					return BadRequest();
				}

				return BadRequest(ModelState);
			}

			return null;
		}
	}
}
