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
            GrandFather = 9,
            [StringValue("Grand Mother")]
            GrandMother = 10,
        }

        public enum GenderEnum
        {
            [StringValue("Male")]
            Male = 1,
            [StringValue("Female")]
            Female = 2,
            [StringValue("Other")]
            Other = 3,

        }

        public enum ApiStatusCode
        {
            Ok = 200,
            RecordNotFound = 204,
            AlreadyExist = 205,
            NotFound = 404,
            UnAuthorized = 401,
            InternalServerError = 501,
            ServerException = 405,
            DataBaseTransactionFailed = 406,
            InvaildModel = 407
        }

        public enum UserRoleEnum
        {
            [StringValue("Super Admin")]
            SuperAdmin = 1,
            [StringValue("Admin (Web)")]
            Admin = 2,
            [StringValue("Zonal Manager")]
            ZonalManager = 3,
            [StringValue("Supervisor")]
            Supervisor = 4,
            [StringValue("Agent")]
            Agent = 5,
            [StringValue("Door Step Agent")]
            DoorStepAgent = 6,
        }
    }


}
