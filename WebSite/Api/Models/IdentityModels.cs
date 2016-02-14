using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace DajLapu.Api.Models
{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser
	{
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
			// Add custom user claims here
			return userIdentity;
		}

		[MaxLength(30)]
		public string FirstName { get; set; }

		[MaxLength(30)]
		public string LastName { get; set; }

		[MaxLength(30)]
		public string MiddleName { get; set; }

		[MaxLength(100)]
		public string TimeZoneId { get; set; }

		[MaxLength(2)]
		public string LanguageCode { get; set; }

		public int? СityId { get; set; }

		[Required]
		public DateTime JoinDate { get; set; }
	}

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
			Configuration.ProxyCreationEnabled = false;
			Configuration.LazyLoadingEnabled = false;
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}