using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace DajLapu.Api.Services
{
	public class EmailService : IIdentityMessageService
	{
		public async Task SendAsync(IdentityMessage message)
		{
			MailMessage myMessage = new MailMessage() { Subject = message.Subject, IsBodyHtml = true, Body = message.Body };
			myMessage.To.Add(new MailAddress(message.Destination));
			myMessage.From = new MailAddress("dajlapu.by@gmail.com", "Daj lapu");

			SmtpClient smtpClient = new SmtpClient();
			smtpClient.Host = "smtp.gmail.com";
			smtpClient.Port = 587;
			smtpClient.EnableSsl = true;
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["emailService:Account"], ConfigurationManager.AppSettings["emailService:Password"]);

			smtpClient.Send(myMessage);
		}
	}
}