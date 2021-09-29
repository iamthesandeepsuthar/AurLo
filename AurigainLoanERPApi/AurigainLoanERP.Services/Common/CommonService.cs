using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.Common.Method;
using AurigainLoanERP.Shared.Common.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.Common
{
    public class CommonService : BaseService, ICommonService
    {
        public readonly IMapper _mapper;
        private AurigainContext _db;
        public CommonService(IMapper mapper, AurigainContext db)
        {
            this._mapper = mapper;
            _db = db;
        }

        public async Task<ApiServiceResponseModel<Dictionary<string, object>>> GetDropDown(string[] key)
        {
            Dictionary<string, object> objData = new Dictionary<string, object>();
            try
            {
                foreach (var item in key)
                {

                    switch (item)
                    {
                        case DropDownKey.ddlParentUserRole:

                            objData.Add(item, await GetUserRole(true));
                            break;

                        case DropDownKey.ddlUserRole:

                            objData.Add(item, await GetUserRole());
                            break;


                        default:
                            break;
                    }

                   

                }

                return CreateResponse(objData, ResponseMessage.Success, true);
            }
            catch (Exception ex)
            {

                return CreateResponse<Dictionary<string, object>>(null, ResponseMessage.Success, true, ex.Message.ToString());

            }

        }

        #region <<private Method>>

        /// <summary>
        /// Get User Role
        /// </summary>
        /// <param name="isParent">for getiing Parrent Role</param>
        /// <returns></returns>
        private async Task<object> GetUserRole(bool? isParent = false)
        {
            try
            {
                return await (from role in _db.UserRole where role.IsActive == true && !role.IsDelete && (isParent == false || role.ParentId == null) select role)
                     .Select(r => new { Text = r.Name, Value = r.Id })
                     .ToListAsync();
            }
            catch (Exception)
            {

                return null;
            }
        }
        #endregion
    }
}
