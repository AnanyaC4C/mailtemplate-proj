using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmailSendgrid.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailCon : ControllerBase
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly IConfiguration _configuration;
        public EmailCon(
            ISendGridClient sendGridClient,
            IConfiguration configuration)
        {
            _sendGridClient = sendGridClient;
            _configuration = configuration;
        }


        [HttpGet]
        [Route("send-text-mail")]
        public async Task<IActionResult> SendPlainTextEmail(string toEmail)
        {
            ///added comment
            string fromEmail = _configuration.GetSection("SendGridEmailSettings")
            .GetValue<string>("FromEmail");

            string fromName = _configuration.GetSection("SendGridEmailSettings")
            .GetValue<string>("FromName");

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromName),
                Subject = "Check Message",
                PlainTextContent = "Hello, have a good day :)!!!"
            };
            msg.AddTo(toEmail);
            var response = await _sendGridClient.SendEmailAsync(msg);
            string message = response.IsSuccessStatusCode ? "Email Send Successfully" :
            "Email Sending Failed";
            return Ok(message);
        }

        //git remote add origin https://github.com/AnanyaC4C/Email.git
    }
}
