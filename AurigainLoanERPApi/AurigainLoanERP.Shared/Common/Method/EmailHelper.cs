using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Shared.Common.Method
{
    public class EmailHelper
    {
        readonly SmtpClient _client;
        readonly IConfiguration _configuration;
        readonly string _fromEmail;
        private IHostingEnvironment _env;
        private IHostingEnvironment environment;

        public EmailHelper(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _fromEmail = _configuration[Constants.SMTP_USER];
            _env = env;
            _client = new SmtpClient
            {
                Host = _configuration[Constants.SMTP_SERVER],
                Port = int.Parse(_configuration[Constants.SMTP_PORT]),
                Credentials = new NetworkCredential(_configuration[Constants.SMTP_USER], _configuration[Constants.SMTP_PASSWORD]),
                EnableSsl = true
            };
        }

        //public EmailHelper(IHostingEnvironment environment)
        //{
        //    this.environment = environment;
        //}

        public async Task SendMail(string toEmail, string subject, string body, bool isBodyHtml = false)
        {
            try
            {
                var message = new MailMessage(_fromEmail, toEmail, subject, body);
                message.IsBodyHtml = isBodyHtml;
                await _client.SendMailAsync(message);
            }
            catch
            {
              
            }

        }

        public async Task SendHTMLBodyMail(string toEmail, string subject, string templatePath, Dictionary<string, string> replaceValues = null)
        {
            try
            {
                string body = GetHtmlString(templatePath, replaceValues);
                var message = new MailMessage(_fromEmail, toEmail, subject, body);
                message.IsBodyHtml = true;
                await _client.SendMailAsync(message);
            }
            catch
            {

            }

        }

        public string GetHtmlString(string templatePath, Dictionary<string, string> replaceValues=null)
        {
            try
            {
                var pathToFile = _env.ContentRootPath + templatePath;
                var builder = new BodyBuilder();
                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {

                    builder.HtmlBody = SourceReader.ReadToEnd();

                }
              
                if (replaceValues != null)
                {
                    foreach (var item in replaceValues)
                    {
                        builder.HtmlBody= builder.HtmlBody.Replace(item.Key, item.Value);
                    }
                }

                return builder.HtmlBody;

            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
