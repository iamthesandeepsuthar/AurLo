using AurigainLoanERP.Shared.Common.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace AurigainLoanERP.Shared.Common.Method
{


    public class SMSHelper
    {
        readonly IConfiguration _configuration;
        private string _authKey, _senderId, _endPoint;

        public SMSHelper(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _authKey = _configuration[Constants.SMS_AuthKey];
            _senderId = _configuration[Constants.SMS_SanderId];
            _endPoint = _configuration[Constants.SMS_EndPoint];
   
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
                string url = string.Concat(_endPoint, "?authkey=", _authKey, "&sender=", _senderId, "&type=", "2", "&route=", "2", "&mobiles=", mobile, "&message=", msg.Trim());

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = HttpMethod.Get.ToString(); // "GET"
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
                string url = string.Concat(_endPoint, "?authkey=", _authKey, "&sender=", _senderId, "&type=", "2", "&route=", "2", "&mobiles=", string.Join(",", mobile), "&message=", msg);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = HttpMethod.Get.ToString(); 

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
