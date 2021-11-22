using AurigainLoanERP.Shared.Common.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AurigainLoanERP.Shared.Common.Method
{


    public class SMSHelper
    {
        readonly IConfiguration _configuration;
        private IHostingEnvironment _env;
        private string _authKey, _senderId, _endPoint;

        public SMSHelper(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _authKey = _configuration[Constants.SMS_AuthKey];
            _senderId = _configuration[Constants.SMS_SanderId];
            _endPoint = _configuration[Constants.SMS_EndPoint];
            _env = env;
        }
        /// <summary>
        /// Send SMS
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public string SendSMS(string msg, string mobile)
        {
            SMSResponseModel smsResponseModel = new SMSResponseModel();

            try
            {
                string responseData = string.Empty;
                string url = string.Concat(_endPoint, "?authkey=", _authKey, "&sender=", _senderId, "&type=", "1", "&route=", "2", "&mobiles=", mobile, "&message=", msg.Trim());

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    responseData = streamReader.ReadToEnd();
                    smsResponseModel = JsonConvert.DeserializeObject<SMSResponseModel>(responseData);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;

        }
        /// <summary>
        /// To send on multiple number
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public SMSResponseModel SendSMS(string msg, string[] mobile)
        {
            SMSResponseModel smsResponseModel = new SMSResponseModel();
            try
            {
                string responseData = string.Empty;
                string url = string.Concat(_endPoint, "?authkey=", _authKey, "&sender=", _senderId, "&type=", "1", "&route=", "2", "&mobiles=", string.Join(",", mobile), "&message=", msg);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    responseData = streamReader.ReadToEnd();
                    smsResponseModel = JsonConvert.DeserializeObject<SMSResponseModel>(responseData);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;

        }
    }

 
}
