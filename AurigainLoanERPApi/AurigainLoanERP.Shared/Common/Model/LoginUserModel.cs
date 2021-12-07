using System;
using System.Collections.Generic;
using System.Text;

namespace AurigainLoanERP.Shared.Common.Model
{
    public static class LoginUserModel
    {
        public static long? UserId { get; set; }
        public static string UserName { get; set; }
        public static int? RoleId { get; set; }
        public static string RoleName { get; set; }

        public static DateTime? LoginTime { get; set; }
    }
}
