using AurigainLoanERP.Shared.Attribute;
using DocumentFormat.OpenXml;


namespace AurigainLoanERP.Shared.Enums
{
    public class FixedValueEnums
    {

        public enum RelationshipEnum
        {
            [StringValue("Mother")]
            Mother = 1,
            [StringValue("Father")]
            Father = 2,
            [StringValue("Son")]
            Son = 3,
            [StringValue("Brother")]
            Brother = 4,
            [StringValue("Sister")]
            Sister = 5,
            [StringValue("Wife")]
            Wife = 6,
            [StringValue("Husdand")]
            Husdand = 7,
            [StringValue("Daughter")]
            Daughter = 8,
            [StringValue("Grand Father")]
            GrandFathe = 9,
            [StringValue("Grand Mother")]
            GrandMother = 10,
        }
    }


}
