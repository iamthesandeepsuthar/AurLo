using AurigainLoanERP.Services.Qualification;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualificationController : ControllerBase
    {
        private readonly IQualificationService _qualificationService;
        public QualificationController(IQualificationService qualificationService)
        {
            _qualificationService = qualificationService;
        }
        // GET api/Qualification/Qualifications
        [HttpGet("[action]")]
        public async Task<ApiServiceResponseModel<List<DDLQualificationModel>>> Qualifications()
        {
            return await _qualificationService.GetQualifications();

        }

        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<List<QualificationModel>>> GetList(IndexModel model)
        {
            return await _qualificationService.GetAllAsync(model);
        }
        // POST api/Qualification/SubmitQualification
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> SubmitQualification(QualificationModel model)
        {
            if (ModelState.IsValid)
            {
                return await _qualificationService.AddUpdateAsync(model);
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
            // DELETE api/StateAndDistrict/DeleteState/5
           
        }

        // GET api/Qualification/GetQualificationById/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<QualificationModel>> GetQualificationById(int id)
        {
            return await _qualificationService.GetById(id);

        }

        // DELETE api/Qualification/DeleteQualification/5
        [HttpDelete("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> DeleteQualification(int id)
        {
            return await _qualificationService.UpdateDeleteStatus(id);
        }
        // GET api/Qualification/ChangeActiveStatus/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> ChangeActiveStatus(int id)
        {
            return await _qualificationService.UpateActiveStatus(id);
        }

    }
}
