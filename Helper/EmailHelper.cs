using Microsoft.Extensions.Configuration;
using SMTPConfifuration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SMTPConfifuration.Helper
{
    public class EmailHelper
    {
        private string _userName,_userPassword;
        private string _host;
        private int _port;
        private string _from;
        private string _alias;

        public EmailHelper(IConfiguration iConfiguration) {
            _host = iConfiguration.GetSection("SMTP").GetSection("Host").Value;
            _port = Convert.ToInt32(iConfiguration.GetSection("SMTP").GetSection("Port").Value);
            _from = iConfiguration.GetSection("SMTP").GetSection("From").Value;
            _alias = iConfiguration.GetSection("SMTP").GetSection("Alias").Value;
            _userName= iConfiguration.GetSection("SMTP").GetSection("UsernameEmail").Value;
            _userPassword= iConfiguration.GetSection("SMTP").GetSection("UsernamePassword").Value;
        }

        public void SendEmail(EmailModel emailModel) {
            try
            {
                using (SmtpClient client = new SmtpClient(_host))
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(_from, _alias);
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.To.Add(emailModel.To);
                    mailMessage.Body = emailModel.Message;
                    mailMessage.Subject = emailModel.Subject;
                    mailMessage.IsBodyHtml = emailModel.IsBodyHtml;
                    client.Port = _port;
                    client.Credentials = new NetworkCredential(_userName, _userPassword);
                    client.EnableSsl = true;

                //    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                //System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                //System.Security.Cryptography.X509Certificates.X509Chain chain,
                //System.Net.Security.SslPolicyErrors sslPolicyErrors)
                //    {
                //        return true;
                //    };

                    client.Send(mailMessage);                    
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
