using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Shared.Common
{
   public  class Constants
    {
        public const string CULTURE_KEY = "Culture";
        public const string ALLOW_ALL_ORIGINS = "AllowAllOrigins";
#if DEBUG
        public const string ALLOWED_HOSTS_KEY = "AllowedHost:Development";
#else
        public const string ALLOWED_HOSTS_KEY = "AllowedHost:Production";
#endif
    }
}
