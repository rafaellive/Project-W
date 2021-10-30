using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models.Domain;
using Sabio.Services;
using Sabio.Web.Models.Responses;
using Sabio.Web.Controllers;
using Sabio.Models;



namespace Sabio.Web.Api.Controllers
{
	[Route("api/AdminDash")]
	[ApiController]
	public class AdminDashApiController : BaseApiController
	{
		private IAdminDashService _service = null;
		private IAuthenticationService<int> _authService = null;
		public AdminDashApiController(IAdminDashService service
			, ILogger<AdminDashApiController> logger
			, IAuthenticationService<int> authService) : base(logger)
		{
			_service = service;
			_authService = authService;
		}

		[HttpGet("paginate")]
		public ActionResult<ItemResponse<Paged<UserDetail>>> GetAll(int pageIndex, int pageSize)
		{
			int iCode = 200;
			BaseResponse response = null;
			try
			{
				Paged<UserDetail> page = _service.GetAll(pageIndex, pageSize);
				if (page == null)
				{
					iCode = 404;
					response = new ErrorResponse("Application Resource not found.");
				}
				else
				{
					response = new ItemResponse<Paged<UserDetail>> { Item = page };
				}
			}
			catch (Exception ex)
			{
				iCode = 500;
				response = new ErrorResponse(ex.Message);
				base.Logger.LogError(ex.ToString());
			}
			return StatusCode(iCode, response);
		}


		[HttpGet("rolepaginate")]
		public ActionResult<ItemResponse<Paged<UserDetail>>> GetbyRole(int pageIndex, int pageSize, string role)
		{
			int iCode = 200;
			BaseResponse response = null;
			try
			{
				Paged<UserDetail> page = _service.GetbyRole(pageIndex, pageSize, role);
				if (page == null)
				{
					iCode = 404;
					response = new ErrorResponse("Application Resource not found.");
				}
				else
				{
					response = new ItemResponse<Paged<UserDetail>> { Item = page };
				}
			}
			catch (Exception ex)
			{
				iCode = 500;
				response = new ErrorResponse(ex.Message);
				base.Logger.LogError(ex.ToString());
			}
			return StatusCode(iCode, response);
		}


		[HttpGet("")]
		public ActionResult<ItemsResponse<UserRole>> GetAllRoles()
		{
			int iCode = 200;
			BaseResponse response = null;
			try
			{
				List<UserRole> list = _service.GetAllRoles();
				if (list == null)
				{
					iCode = 404;
					response = new ErrorResponse("Application Resource not found.");
				}
				else
				{
					response = new ItemsResponse<UserRole> { Items = list };
				}
			}
			catch (Exception ex)
			{
				iCode = 500;
				response = new ErrorResponse(ex.Message);
				base.Logger.LogError(ex.ToString());
			}
			return StatusCode(iCode, response);
		}
		[HttpPut("{id:int}")]
		public ActionResult<SuccessResponse> Update(UserStatusUpdateRequest model)
		{

			int iCode = 200;
			BaseResponse response = null;
			try
			{

				 _service.Update(model);

				response = new ItemResponse<int>() { Item = model.Id };
			}
			catch (Exception ex)
			{
				iCode = 500;
				response = new ErrorResponse(ex.Message);
			}
			return StatusCode(iCode, response);
		}
	} 
}
