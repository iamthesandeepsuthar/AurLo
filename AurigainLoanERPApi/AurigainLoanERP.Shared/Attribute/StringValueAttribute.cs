﻿namespace AurigainLoanERP.Shared.Attribute
{
    public class StringValueAttribute : System.Attribute
    {
        public string StringValue { get; set; }

        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }
    }
}
