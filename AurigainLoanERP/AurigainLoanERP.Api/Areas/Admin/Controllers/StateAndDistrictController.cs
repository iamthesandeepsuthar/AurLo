using AurigainLoanERP.Services.StateAndDistrict;
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
    public class StateAndDistrictController : ControllerBase
    {
        private readonly IStateAndDistrictService _serivce;
        public StateAndDistrictController(IStateAndDistrictService service)
        {
            _serivce =service;
        }
    }
}
