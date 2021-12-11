using AurigainLoanERP.Services.ProductCategory;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoriesService _category;
        public ProductCategoryController(IProductCategoriesService category)
        {
            _category = category;
        }
        // GET api/ProductCategory/ProductCategories
        [HttpGet("[action]")]
        public async Task<ApiServiceResponseModel<List<DllProductCategoryModel>>> ProductCategories()
        {
            return await _category.ProductCategories();
        }

        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<List<ProductCategoryModel>>> GetList(IndexModel model)
        {
            return await _category.GetAllAsync(model);
        }
        // POST api/ProductCategory/AddUpdate
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> AddUpdate(ProductCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                return await _category.AddUpdateAsync(model);
            }
            else
            {
                ApiServiceResponseModel<string> obj = new ApiServiceResponseModel<string>();
                obj.Data = null;
                obj.IsSuccess = false;
                obj.Message = ResponseMessage.InvalidData;
                obj.Exception = ModelState.ErrorCount.ToString();
                return obj;
            }

        }

        // GET api/Qualification/GetById/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<ProductCategoryModel>> GetById(int id)
        {
            return await _category.GetById(id);

        }

        // DELETE api/ProductCategory/DeleteProductCategory/5
        [HttpDelete("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> DeleteProductCategory(int id)
        {
            return await _category.UpdateDeleteStatus(id);
        }
        // GET api/ProductCategory/ChangeActiveStatus/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> ChangeActiveStatus(int id)
        {
            return await _category.UpateActiveStatus(id);
        }

    }
}
