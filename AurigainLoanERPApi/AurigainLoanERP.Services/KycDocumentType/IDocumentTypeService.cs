using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.KycDocumentType
{
    public interface IDocumentTypeService
    {
        Task<ApiServiceResponseModel<List<DocumentTypeModel>>> GetAllAsync(IndexModel model);
        Task<ApiServiceResponseModel<DocumentTypeModel>> GetById(int id);
     
        Task<ApiServiceResponseModel<List<DDLDocumentTypeModel>>> GetDocumentType(bool? isKYC = null);
        Task<ApiServiceResponseModel<string>> AddUpdateAsync(DocumentTypeModel model);
        // Task<ApiServiceResponseModel<object>> CheckRoleExist(string name, int? id = null);      
        Task<ApiServiceResponseModel<object>> UpateActiveStatus(int id);
        Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(int id);
    }
}
