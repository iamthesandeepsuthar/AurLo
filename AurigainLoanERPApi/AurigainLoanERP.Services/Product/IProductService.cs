using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.Product
{
    public interface IProductService
    {
        Task<ApiServiceResponseModel<List<ProductModel>>> GetAllAsync(IndexModel model);
        Task<ApiServiceResponseModel<ProductModel>> GetById(int id);
        Task<ApiServiceResponseModel<List<DDLProductModel>>> Products();
        Task<ApiServiceResponseModel<List<DDLProductModel>>> ProductsByCategory(int id);
        Task<ApiServiceResponseModel<string>> AddUpdateAsync(ProductModel model);
        Task<ApiServiceResponseModel<object>> UpateActiveStatus(int id);
        Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(int id);
    }
}
