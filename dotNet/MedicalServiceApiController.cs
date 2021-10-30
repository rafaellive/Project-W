using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models.Domain.Provider;
using Sabio.Models.Requests.Locations;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/medicalservices")]
    [ApiController]
    public class MedicalServiceApiController : BaseApiController
    {
        private IMedicalServiceService _service = null;
        private IAuthenticationService<int> _authService = null;

        public MedicalServiceApiController(IMedicalServiceService service
            , ILogger<MedicalServiceApiController> logger
            , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }

        [HttpGet]
        public ActionResult<ItemsResponse<MedicalService>> Get(string keyword)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                List<MedicalService> medicalServiceList = _service.GetAllByKeyword(keyword);

                if (medicalServiceList == null)
                {
                    iCode = 400;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemsResponse<MedicalService> { Items = medicalServiceList };
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(iCode, response);
        }
        [AllowAnonymous]
        [HttpPost("location")]
        public ActionResult<ItemResponse<List<MedicalServiceLocation>>> GetService(ProviderServiceLocationGetRequest model)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {

                List<MedicalServiceLocation> list = _service.GetServiceLocation(model);

                if (list == null)
                {
                    code = 404;
                    response = new ErrorResponse("No providers found offering that service in the area specified");

                }
                else
                {
                    response = new ItemResponse<List<MedicalServiceLocation>> { Item = list };
                }
            }
            catch (Exception exceptionResponse)
            {
                code = 500;
                response = new ErrorResponse(exceptionResponse.Message);
                base.Logger.LogError(exceptionResponse.ToString());
            }

            return StatusCode(code, response);

        }
    }
}
