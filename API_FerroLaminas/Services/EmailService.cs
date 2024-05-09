using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API_FerroLaminas.Services
{
    public class EmailService
    {
        private const string GmailUsername = "laminasferro@gmail.com";
        private const string GmailPassword = "moph cpbj jzjx ieom";

        public async Task SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(GmailUsername, GmailPassword),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(GmailUsername),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Manejo de errores si falla el envío del correo
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
                throw;
            }
        }
    }
}
