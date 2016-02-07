using System;
using System.Collections.Generic;
using System.Linq;
using DajLapu.Web.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DajLapu.Web.Startup))]

namespace DajLapu.Web
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}
