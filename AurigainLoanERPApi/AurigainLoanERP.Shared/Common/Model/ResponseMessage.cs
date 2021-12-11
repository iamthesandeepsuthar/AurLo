﻿namespace AurigainLoanERP.Shared.Common.Model
{
    public static class ResponseMessage
    {
        public const string Success = "Data retrived successfully...!";
        public const string Found = "Record found...!";
        public const string NotFound = "No record found...!";
        public const string Save = "Record Saved...!";
        public const string Update = "Record Updated...!";
        public const string InvalidData = "Invalid Data Pass...!";
        public const string UserExist = "User already mapped with mobile or email...!";
        public const string Fail = "Faild";
        public const string RecordAlreadyExist = "Record already exist, Please try with other !";
        public const string FileUpdated = "File sucessfully updated...!";

    }

    public class SentSMSResponseModel
    {
        public string error { get; set; }
        public long[] msg_id { get; set; }

    }

    public class SMSStatusResponseModel
    {
        public string status { get; set; }


    }

}
