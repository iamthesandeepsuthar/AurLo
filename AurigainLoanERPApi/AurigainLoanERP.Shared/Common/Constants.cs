namespace AurigainLoanERP.Shared.Common
{
    public class Constants
    {
        public const string CULTURE_KEY = "Culture";
        public const string ALLOW_ALL_ORIGINS = "AllowAllOrigins";
        public const string SMTP_SERVER = "SMTP:Server";
        public const string SMTP_PORT = "SMTP:Port";
        public const string SMTP_USER = "SMTP:UserName";
        public const string SMTP_PASSWORD = "SMTP:Password";


        public const string SMS_AuthKey = "SMS:AuthKey";
        public const string SMS_SanderId = "SMS:SanderId";
        public const string SMS_EndPoint = "SMS:EndPoint";




        public const string JWT_Key = "Jwt:Key";
        public const string JWT_ISSUER = "Jwt:Issuer";

#if DEBUG
        public const string ALLOWED_HOSTS_KEY = "AllowedHost:Development";
#else
        public const string ALLOWED_HOSTS_KEY = "AllowedHost:Production";
#endif
#if DEBUG
        public const string CONNECTION_STRING = "ConnectionStrings:Development";
#else
        public const string CONNECTION_STRING = "ConnectionStrings:Production";        
#endif


    }
}
