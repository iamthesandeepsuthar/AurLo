using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.ProductCategory
{
    public interface IProductCategoriesService
    {
        Task<ApiServiceResponseModel<List<ProductCategoryModel>>> GetAllAsync(IndexModel model);
        Task<ApiServiceResponseModel<ProductCategoryModel>> GetById(int id);
        Task<ApiServiceResponseModel<List<DllProductCategoryModel>>> ProductCategories();
        Task<ApiServiceResponseModel<string>> AddUpdateAsync(ProductCategoryModel model);
        Task<ApiServiceResponseModel<object>> UpateActiveStatus(int id);
        Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(int id);
    }
}
