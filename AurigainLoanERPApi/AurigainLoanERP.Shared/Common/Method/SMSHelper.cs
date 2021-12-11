using AurigainLoanERP.Shared.Common.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AurigainLoanERP.Shared.Common.Method
{


    public class SMSHelper
    {
        readonly IConfiguration _configuration;
        private string _authKey, _senderId, _endPoint;

        public SMSHelper(IConfiguration configuration)
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
        public async Task<SentSMSResponseModel> SendSMS(string msg, string mobile)
        {
            SentSMSResponseModel smsResponseModel = new SentSMSResponseModel();

            try
            {
                string message = string.Format("Dear {0},{1} is the OTP for login into AurLo App.Do not share this for security purposes.Valid for 3 min.", "customer", msg);
                string responseData = string.Empty;
                string url = string.Concat(_endPoint, "sendhttp.php?authkey=", _authKey, "&mobiles=", mobile, "&message=", message, "&sender=", _senderId, "&type=", "1", "&route=", "2");

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = HttpMethod.Get.ToString(); // "GET"
                var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    responseData = await streamReader.ReadToEndAsync();
                    smsResponseModel = JsonConvert.DeserializeObject<SentSMSResponseModel>(responseData);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return smsResponseModel;

        }
        /// <summary>
        /// To send on multiple number
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public async Task<SentSMSResponseModel> SendSMS(string msg, string[] mobile)
        {
            SentSMSResponseModel smsResponseModel = new SentSMSResponseModel();
            try
            {
                string responseData = string.Empty;
                string url = string.Concat(_endPoint, "sendhttp.php?authkey=", _authKey, "&sender=", _senderId, "&type=", "2", "&route=", "2", "&mobiles=", string.Join(",", mobile), "&message=", msg);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = HttpMethod.Get.ToString();

                var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    responseData = await streamReader.ReadToEndAsync();
                    smsResponseModel = JsonConvert.DeserializeObject<SentSMSResponseModel>(responseData);
                }

            }
            catch (Exception)
            {

                throw;
            }
            return smsResponseModel;

        }

        public async Task<SMSStatusResponseModel> CheckSMSStatud(long msgId)
        {
            SMSStatusResponseModel response = new SMSStatusResponseModel();

            try
            {
                string responseData = string.Empty;
                string url = string.Concat(_endPoint, "reports.php?authkey=", _authKey, "&msg_id=", msgId);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = HttpMethod.Get.ToString(); // "GET"
                var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    responseData = await streamReader.ReadToEndAsync();
                    response = JsonConvert.DeserializeObject<SMSStatusResponseModel>(responseData);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;

        }
    }


}
