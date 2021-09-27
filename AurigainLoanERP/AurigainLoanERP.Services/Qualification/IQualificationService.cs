using AurigainLoanERP.Data.ContractModel;
using AurigainLoanERP.Shared.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.Qualification
{
    public interface IQualificationService
    {
        Task<ApiServiceResponseModel<List<QualificationModel>>> GetAllAsync(IndexModel model);
        Task<ApiServiceResponseModel<QualificationModel>> GetById(int id);
        Task<ApiServiceResponseModel<List<DDLQualificationModel>>> GetQualifications();
        Task<ApiServiceResponseModel<string>> AddUpdateAsync(QualificationModel model);
        // Task<ApiServiceResponseModel<object>> CheckRoleExist(string name, int? id = null);      
        Task<ApiServiceResponseModel<object>> UpateActiveStatus(int id);
        Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(int id);

    }
}
