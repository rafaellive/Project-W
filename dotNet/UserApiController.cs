using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Enums;
using Sabio.Models.Requests;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Sabio.Models.Requests.Users;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserApiController : BaseApiController
    {
        private IAuthenticationService<int> _authenticationService;
        private IUserService _service = null;
        private ITokenService _tokService = null;
        private IEmailService _eService = null;

        public UserApiController(IAuthenticationService<int> authService, ITokenService tokService,
            IUserService service, ILogger<UserApiController> logger, IEmailService eService)
            : base(logger)
        {
            _service = service;
            _tokService = tokService;
            _eService = eService;
            _authenticationService = authService;
        }

        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public ActionResult<SuccessResponse> ForgotPassword(UserForgotPasswordRequest model)
        {


            int iCode = 200;
            BaseResponse response = null;
            int userId = _service.GetIdByEmail(model.Email);

            try
            {
                Guid token = Guid.NewGuid();
                UserTokensAddRequest tokenRequest = new UserTokensAddRequest();
                tokenRequest.Token = token;
                tokenRequest.UserId = userId;
                tokenRequest.TokenType = (int)TokenType.NewUser;
                _tokService.AddToken(tokenRequest);

                _eService.ForgotPasswordEmail(model.Email, tokenRequest, userId);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                iCode = 500;
                Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(iCode, response);

        }

        [HttpPut("passwordUpdate/{token:guid}")]
        [AllowAnonymous]
        public ActionResult<SuccessResponse> UpdatePassword(UserPasswordUpdateRequest model)
        {

            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.PasswordUpdate(model);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }

        [HttpGet("")]
        [AllowAnonymous]
        public ActionResult<ItemsResponse<User>> GetAll()
        {
            List<User> list = _service.GetAllUser();

            ItemsResponse<User> response = new ItemsResponse<User>();
            response.Items = list;

            return Ok(response);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public ActionResult<ItemResponse<int>> Create(UserAddRequest model)
        {
            int iCode = 201;
            BaseResponse response = null;

            try
            {
                int id = _service.Add(model);
                if (id > 0)
                {
                    Guid token = Guid.NewGuid();
                    UserTokensAddRequest tokenRequest = new UserTokensAddRequest();
                    tokenRequest.Token = token;
                    tokenRequest.UserId = id;
                    tokenRequest.TokenType = (int)TokenType.NewUser;
                    _tokService.AddToken(tokenRequest);

                    _eService.CreateConfirmEmail(model, tokenRequest, id);
                }
                response = new ItemResponse<int>() { Item = id };
            }
            catch (Exception ex)
            {
                iCode = 500;
                Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }
            return StatusCode(iCode, response);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<ItemResponse<bool>> UserLogin(LoginRequest model)
        {
            int iCode = 201;
            BaseResponse response = null;
            try
            {
                Task<bool> isAuthenticated = _service.LogInAsync(model.Email, model.Password);
                if (isAuthenticated.Result == true)
                {
                    response = new SuccessResponse();
                } 
                else
                {
                    iCode = 401;
                    response = new ErrorResponse("Invalid Credentials");
                }
               
            }
            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse("Invalid Credentials");
            }
            return StatusCode(iCode, response);
        }

        [HttpGet("logout")]
        public ActionResult<ItemResponse<bool>> UserLogout()
        {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                _authenticationService.LogOutAsync();
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse("Error logging out");
            }
            return StatusCode(iCode, response);
        }


        [HttpPost("facebookAuth")]
        [AllowAnonymous]
        public ActionResult<ItemResponse<bool>> FacebookAuth(ExternalLoginRequest model)
        {
            int iCode = 201;
            BaseResponse response = null;
            try
            {
                Task<bool> isAuthenticated = _service.FacebookAuthAsync(model.accessToken);
                if (isAuthenticated.Result == true)
                {
                    response = new SuccessResponse();
                }
                else
                {
                    iCode = 401;
                    response = new ErrorResponse("Invalid Credentials");
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse("Invalid Credentials");
            }
            return StatusCode(iCode, response);
        }


        [HttpPost("googleAuth")]
        [AllowAnonymous]
        public ActionResult<ItemResponse<bool>> GoogleAuth(ExternalLoginRequest model)
        {
            int iCode = 201;
            BaseResponse response = null;
            try
            {
                Task<bool> isAuthenticated = _service.GoogleAuthAsync(model.accessToken);
                if (isAuthenticated.Result == true)
                {
                    response = new SuccessResponse();
                }
                else
                {
                    iCode = 401;
                    response = new ErrorResponse("Invalid Credentials");
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse("Invalid Credentials");
            }
            return StatusCode(iCode, response);
        }

        [HttpGet("profile")]
        public ActionResult<ItemResponse<UserDetail>> GetUserProfile()
        {
            int userId = 0;
            int iCode = 200;
            BaseResponse response = null;
            UserDetail user = null;

            try
            {
                userId = _authenticationService.GetCurrentUserId();
                user = _service.GetProfile(userId);
                if (user == null)
                {
                    iCode = 404;
                    response = new ErrorResponse($"Resource Not Found");
                }
                else
                {
                    response = new ItemResponse<UserDetail> { Item = user };
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response =  new ErrorResponse($"Generic Error: {ex.Message}");
            }
            return StatusCode(iCode, response);
        }


        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<User>> Get(int id)
        {
            try
            {
                User user = _service.GetProfile(id);

                ItemResponse<User> response = new ItemResponse<User>();
                response.Item = user;

                if (response.Item == null)
                {
                    return NotFound404(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                return base.StatusCode(500, new ErrorResponse($"Generic Error: {ex.Message}"));
            }
        }

        [HttpGet("confirm/{token:guid}")]
        [AllowAnonymous]
        public ActionResult<SuccessResponse> Get(Guid token)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                _service.UpdateConfirm(token);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(code, response);
        }
    }
}
