using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.Common.Method;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AurigainLoanERP.Shared.Enums.FixedValueEnums;

namespace AurigainLoanERP.Services.Product
{
    public class ProductService : BaseService, IProductService
    {
        public readonly IMapper _mapper;
        private AurigainContext _db;
        public ProductService(IMapper mapper, AurigainContext db)
        {
            this._mapper = mapper;
            _db = db;
        }
        public async Task<ApiServiceResponseModel<List<ProductModel>>> GetAllAsync(IndexModel model)
        {
            ApiServiceResponseModel<List<ProductModel>> objResponse = new ApiServiceResponseModel<List<ProductModel>>();
            try
            {
                var result = (from role in _db.Product 
                              join category in _db.ProductCategory 
                              on role.ProductCategoryId equals category.Id
                              where !role.IsDelete && (string.IsNullOrEmpty(model.Search) || role.Name.Contains(model.Search) || category.Name.Contains(model.Search))
                              orderby (model.OrderByAsc && model.OrderBy == "Name" ? role.Name : "") ascending
                              orderby (!model.OrderByAsc && model.OrderBy == "Name" ? role.Name : "") descending
                              orderby (model.OrderByAsc && model.OrderBy == "Name" ? category.Name : "") ascending
                              orderby (!model.OrderByAsc && model.OrderBy == "Name" ? category.Name : "") descending
                              select role);
                var data = await result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue).ToListAsync();
                objResponse.Data = _mapper.Map<List<ProductModel>>(data);

                if (result != null)
                {
                    return CreateResponse<List<ProductModel>>(objResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<ProductModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<List<ProductModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<List<DDLProductModel>>> Products()
        {
            try
            {
                var product = await _db.Product.Select(x => new DDLProductModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

                if (product.Count() > 0)
                {
                    return CreateResponse<List<DDLProductModel>>(product, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<DDLProductModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<List<DDLProductModel>>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<ProductModel>> GetById(int id)
        {
            try
            {
                var result = await _db.Product.Where(x => x.IsDelete == false && x.Id == id).Include(x=>x.ProductCategory).FirstOrDefaultAsync();
                if (result != null)
                {
                    return CreateResponse<ProductModel>(_mapper.Map<ProductModel>(result), ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<ProductModel>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<ProductModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateAsync(ProductModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var isExist = await _db.Product.Where(x => x.Name == model.Name && x.ProductCategoryId ==model.ProductCategoryId).FirstOrDefaultAsync();
                    if (isExist != null)
                    {
                        return CreateResponse<string>("", ResponseMessage.RecordAlreadyExist, false, ((int)ApiStatusCode.AlreadyExist), "", null);
                    }
                    var product = _mapper.Map<AurigainLoanERP.Data.Database.Product>(model);
                    product.CreatedDate = DateTime.Now;
                    var result = await _db.Product.AddAsync(product);
                }
                else
                {
                    var product = await _db.Product.FirstOrDefaultAsync(x => x.Id == model.Id);
                    product.Name = model.Name;
                    product.InterestRate = model.InterestRate;
                    product.InterestRateApplied = model.InterestRateApplied;
                    product.MaximumAmount = model.MaximumAmount;
                    product.MaximumTenure = model.MaximumTenure;
                    product.MinimumAmount = model.MinimumAmount;
                    product.MinimumTenure = model.MinimumTenure;
                    product.Notes = model.Notes;
                    product.ProcessingFee = model.ProcessingFee;
                    product.ProductCategoryId = model.ProductCategoryId;
                    product.ModifiedDate = DateTime.Now;
                    product.IsActive = model.IsActive;                  
                }
                await _db.SaveChangesAsync();
                return CreateResponse<string>(model.Name, model.Id > 0 ? ResponseMessage.Update : ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {
                return CreateResponse<string>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(int id)
        {
            try
            {
                var objRole = await _db.Product.FirstOrDefaultAsync(r => r.Id == id);
                objRole.IsDelete = !objRole.IsDelete;
                objRole.ModifiedDate = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse<object>(true, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));

            }
            catch (Exception)
            {

                return CreateResponse<object>(false, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException));

            }
        }
        public async Task<ApiServiceResponseModel<object>> UpateActiveStatus(int id)
        {
            try
            {
                var product = await _db.Product.FirstOrDefaultAsync(r => r.Id == id);
                product.IsActive = !product.IsActive;
                product.ModifiedDate = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse<object>(true, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception)
            {
                return CreateResponse<object>(false, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException));
            }


        }
    }
}
