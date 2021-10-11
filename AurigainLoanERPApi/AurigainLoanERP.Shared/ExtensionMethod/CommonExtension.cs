using AurigainLoanERP.Shared.Attribute;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AurigainLoanERP.Shared.ExtensionMethod
{
  public static  class CommonExtension
    {

        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }

    }
}
