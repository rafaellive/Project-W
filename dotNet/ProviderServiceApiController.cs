using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Services;
using Sabio.Web.Controllers;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using Sabio.Models.Domain.Provider;
using Sabio.Web.Models.Responses;
using Sabio.Models.Requests.ProviderDetails.Details;
using Sabio.Models;
using Sabio.Models.Requests.Locations;
using Microsoft.AspNetCore.Authorization;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/providers")]
    [ApiController]
    public class ProviderServiceApiController : BaseApiController
    {
        private IProviderServiceService _service = null;
        private IAuthenticationService<int> _authService = null;

        public ProviderServiceApiController(IProviderServiceService service
            , ILogger<ProviderServiceApiController> logger
            , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }

        [HttpGet("{providerId:int}/{scheduleId:int}")]
        public ActionResult<ItemsResponse<ProviderService>> GetAllByProviderIdAndScheduleId(int providerId, int scheduleId)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                List<ProviderService> servicesList = _service.GetAllByProviderIdAndScheduleId(providerId, scheduleId);

                if (servicesList == null)
                {
                    iCode = 400;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemsResponse<ProviderService> { Items = servicesList };
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


        [HttpGet("{providerId:int}/report")]
        public ActionResult<ItemsResponse<MedicalServiceCount>> GetMedicalServicesReport(int providerId, DateTime startDate, DateTime endDate)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {

                List<MedicalServiceCount> servicesList = _service.GetTopMedicalServicesByDate(providerId, startDate, endDate);

                if (servicesList == null)
                {
                    iCode = 400;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemsResponse<MedicalServiceCount> { Items = servicesList };
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

        [HttpGet("{providerId:int}/top")]
        public ActionResult<ItemsResponse<MedicalServiceCount>> GetTopMedicalServices(int providerId)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {

                List<MedicalServiceCount> topServicesList = _service.GetTopMedicalServicesByProviderId(providerId);

                if (topServicesList == null)
                {
                    iCode = 400;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemsResponse<MedicalServiceCount> { Items = topServicesList };
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

        [HttpGet("services")]
        public ActionResult<ItemsResponse<ProviderService>> Get(int providerId, int scheduleId, int pageIndex, int pageSize)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                Paged<ProviderService> page = _service.GetAllByProviderIdAndScheduleIdPaginated(providerId, scheduleId, pageIndex, pageSize);


                if (page == null)
                {
                    iCode = 400;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<ProviderService>> { Item = page };
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

        [HttpGet("services/search")]
        public ActionResult<ItemsResponse<ProviderService>> Search(int providerId, int scheduleId, int pageIndex, int pageSize, string keyword)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                Paged<ProviderService> page = _service.Search(providerId, scheduleId, pageIndex, pageSize, keyword);

                if (page == null)
                {
                    iCode = 400;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<ProviderService>> { Item = page };
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


        [HttpPost("services")]
        public ActionResult<ItemResponse<int>> Create(ProviderServiceAddRequest model)
        {
            int code = 201;
            BaseResponse response = null;

            try
            {
                int id = _service.Add(model);
                response = new ItemResponse<int> { Item = id };  // Service Id
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"ArgumentException Error: {ex.ToString() }");
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }

        [AllowAnonymous]
        [HttpPost("location")]
        public ActionResult<ItemResponse<List<MedicalProviderLocation>>> GetService(ProviderLocationGetRequest model)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {

                List<MedicalProviderLocation> list = _service.GetProviderLocation(model);

                if (list == null)
                {
                    code = 404;
                    response = new ErrorResponse("No providers found offering that service in the area specified");

                }
                else
                {
                    response = new ItemResponse<List<MedicalProviderLocation>> { Item = list };
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
