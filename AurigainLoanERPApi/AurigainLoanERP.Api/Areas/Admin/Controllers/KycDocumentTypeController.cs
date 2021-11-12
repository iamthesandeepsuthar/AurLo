using AurigainLoanERP.Services.KycDocumentType;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KycDocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeService _documentService;
        public KycDocumentTypeController(IDocumentTypeService documentService)
        {
            _documentService = documentService;
        }
        //POST api/KycDocumentType/GetList
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<List<DocumentTypeModel>>> GetList(IndexModel model)
        {
            return await _documentService.GetAllAsync(model);
        }
        // GET api/KycDocumentType/DocumentTypes
        [HttpGet("[action]/{isKYC?}")]
        public async Task<ApiServiceResponseModel<List<DDLDocumentTypeModel>>> DocumentTypes(bool? isKYC = null)
        {
            return await _documentService.GetDocumentType(isKYC); 
        }

        // POST api/KycDocumentType/SubmitDocumentType
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> SubmitDocumentType(DocumentTypeModel model)
        {
            if (ModelState.IsValid)
            {
                return await _documentService.AddUpdateAsync(model);
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

        // DELETE api/KycDocumentType/DeleteDocumentType/5
        [HttpDelete("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> DeleteDocumentType(int id)
        {
            return await _documentService.UpdateDeleteStatus(id);
        }

        // GET api/KycDocumentType/GetDocumentTypeById/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<DocumentTypeModel>> GetDocumentTypeById(int id)
        {
            return await _documentService.GetById(id);

        }
        // GET api/KycDocumentType/ChangeActiveStatus/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> ChangeActiveStatus(int id)
        {
            return await _documentService.UpateActiveStatus(id);
        }
    }
}
