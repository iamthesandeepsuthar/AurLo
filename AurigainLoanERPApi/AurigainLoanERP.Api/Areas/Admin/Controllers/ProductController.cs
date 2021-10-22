using AurigainLoanERP.Services.Product;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _product;
        public ProductController(IProductService product)
        {
            _product = product;
        }
        // GET api/Product/Products
        [HttpGet("[action]")]
        public async Task<ApiServiceResponseModel<List<DDLProductModel>>> Products()
        {
            return await _product.Products();
        }

        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<List<ProductModel>>> GetList(IndexModel model)
        {
            return await _product.GetAllAsync(model);
        }
        // POST api/Product/AddUpdate
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> AddUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                return await _product.AddUpdateAsync(model);
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

        // GET api/Product/GetById/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<ProductModel>> GetById(int id)
        {
            return await _product.GetById(id);

        }

        // DELETE api/Product/DeleteProduct/5
        [HttpDelete("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> DeleteProduct(int id)
        {
            return await _product.UpdateDeleteStatus(id);
        }
        // GET api/Product/ChangeActiveStatus/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> ChangeActiveStatus(int id)
        {
            return await _product.UpateActiveStatus(id);
        }
    }
}
