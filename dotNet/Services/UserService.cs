using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.AppSettings;
using Sabio.Models.Domain;
using Sabio.Models.Requests;
using Sabio.Models.Requests.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Sabio.Services
{
    public class UserService : IUserService
    {
        private IAuthenticationService<int> _authenticationService;
        private IDataProvider _dataProvider;
        private ExternalAuthConfig _externalAuthConfig;
        private readonly HttpClient _httpClient;

        public UserService(IAuthenticationService<int> authSerice, IDataProvider dataProvider, IOptions<ExternalAuthConfig> externalAuthConfig)
        {
            _authenticationService = authSerice;
            _dataProvider = dataProvider;
            _externalAuthConfig = externalAuthConfig.Value;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_externalAuthConfig.Facebook.GraphDomain)
            };
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public int GetIdByEmail(string email)
        {
            string procName = "[dbo].[Users_Select_AuthData_By_Email]";
            int anId = 0;

            _dataProvider.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Email", email);

            }, delegate (IDataReader reader, short set)
            {

                anId = reader.GetSafeInt32(0);

            });
            return anId;
        }

        public List<User> GetAllUser()
        {
            List<User> list = null;
            string procName = "[dbo].[Users_SelectAllUsers]";

            _dataProvider.ExecuteCmd(procName, inputParamMapper: null
            , singleRecordMapper: delegate (IDataReader reader, short set)
            {
                User user = new User();

                int startingIndex = 0;

                user.Id = reader.GetSafeInt32(startingIndex++);
                user.Email = reader.GetSafeString(startingIndex++);

                if (list == null)
                {
                    list = new List<User>();
                }

                list.Add(user);

            });
            return list;
        }

        public void PasswordUpdate(UserPasswordUpdateRequest model)
        {

            string password = model.Password;
            string salt = BCrypt.BCryptHelper.GenerateSalt(10);
            string hashedPassword = BCrypt.BCryptHelper.HashPassword(password, salt);

            string procName = "[dbo].[UserTokens_Update_PasswordByToken]";
            _dataProvider.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Token", model.Token);
                    col.AddWithValue("@Password", hashedPassword);

                },
                returnParameters: null);
        }

        public int Add(UserAddRequest model)
        {
            int id = 0;

            string password = model.Password;
            string salt = BCrypt.BCryptHelper.GenerateSalt(10);
            string hashedPassword = BCrypt.BCryptHelper.HashPassword(password, salt);

            string procName = "[dbo].[Users_Insert_V7]";
            _dataProvider.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@Email", model.Email);
                    collection.AddWithValue("@Password", hashedPassword);
                    collection.AddWithValue("@Role", model.Role);

                    SqlParameter idOut = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    idOut.Direction = System.Data.ParameterDirection.Output;

                    collection.Add(idOut);
                },
                returnParameters: delegate (SqlParameterCollection returnCol)
                {
                    object oId = returnCol["@Id"].Value;
                    Int32.TryParse(oId.ToString(), out id);
                });
            return id;
        }

        public int AddWithFacebookProfile(ExternalUserResource model)
        {
            int id = 0;

            string password = model.Password;
            string salt = BCrypt.BCryptHelper.GenerateSalt(10);
            string hashedPassword = BCrypt.BCryptHelper.HashPassword(password, salt);

            string procName = "[dbo].[Users_Insert_V8]";
            _dataProvider.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@FirstName", model.FirstName);
                    collection.AddWithValue("@LastName", model.LastName);
                    collection.AddWithValue("@AvatarUrl", model.AvatarUrl);
                    collection.AddWithValue("@Email", model.Email);
                    collection.AddWithValue("@Password", hashedPassword);
                    collection.AddWithValue("@DateCreated", model.DateCreated);

                    SqlParameter idOut = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    idOut.Direction = System.Data.ParameterDirection.Output;

                    collection.Add(idOut);
                },
                returnParameters: delegate (SqlParameterCollection returnCol)
                {
                    object oId = returnCol["@Id"].Value;
                    Int32.TryParse(oId.ToString(), out id);
                });
            return id;
        }

        public int AddWithGoogleProfile(ExternalUserResource model)
        {
            int id = 0;

            string password = model.Password;
            string salt = BCrypt.BCryptHelper.GenerateSalt(10);
            string hashedPassword = BCrypt.BCryptHelper.HashPassword(password, salt);

            string procName = "[dbo].[Users_Insert_V8]";
            _dataProvider.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@FirstName", model.FirstName);
                    collection.AddWithValue("@LastName", model.LastName);
                    collection.AddWithValue("@AvatarUrl", model.AvatarUrl);
                    collection.AddWithValue("@Email", model.Email);
                    collection.AddWithValue("@Password", hashedPassword);
                    collection.AddWithValue("@DateCreated", model.DateCreated);

                    SqlParameter idOut = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    idOut.Direction = System.Data.ParameterDirection.Output;

                    collection.Add(idOut);
                },
                returnParameters: delegate (SqlParameterCollection returnCol)
                {
                    object oId = returnCol["@Id"].Value;
                    Int32.TryParse(oId.ToString(), out id);
                });
            return id;
        }

        public UserDetail GetProfile(int id)
        {
            string procName = "[dbo].[Users_Select_ById_V3]";
            UserDetail user = null;

            _dataProvider.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);
            }, delegate (IDataReader reader, short set)
            {
                user = MapUser(reader, out int index);
            }
            );
            return user;
        }

        public void UpdateConfirm(Guid token)
        {
            string procName = "[dbo].[UserTokens_SelectByToken]";
            _dataProvider.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Token", token);
                },
                returnParameters: null);
        }

        public void Update(UserUpdateRequest model)
        {
            string procName = "[dbo].[Users_UpdateBy_IsConfirmed_Status]";
            _dataProvider.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@IsConfirmed", model.IsConfirmed);
                    col.AddWithValue("@UserStatusId", model.UserStatusId);
                },
                returnParameters: null);
        }

        public async Task<bool> LogInAsync(string email, string password)
        {
            bool isSuccessful = false;
            UserBase user = null;
            string passwordFromDb = "";

            string procName = "[dbo].[Users_Select_AuthData_ByEmail_V3]";

            _dataProvider.ExecuteCmd(procName, delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@Email", email);
            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                passwordFromDb = reader.GetSafeString(startingIndex++);
                user = MapUser(reader, startingIndex);
            });

            isSuccessful = BCrypt.BCryptHelper.CheckPassword(password, passwordFromDb);

            if (isSuccessful)
            {
                IUserAuthData response = new UserBase
                {
                    Id = user.Id
                 ,
                    Name = user.Name
                 ,
                    Roles = user.Roles
                 ,
                    TenantId = "Welrus-00.1.2"
                };

                Claim fullName = new Claim("CustomClaim", "WelrusApp");
                await _authenticationService.LogInAsync(response, new Claim[] { fullName });
            }
            return isSuccessful;
        }

        public async Task<bool> FacebookAuthAsync(string token)
        {
            bool isSuccessful = false;
            var result = await GetAsync<dynamic>(token, "me", "fields=first_name,last_name,email,picture.width(100).height(100)");
            UserBase user = null;

            if (result == null)
            {
                throw new Exception("User from this token not exist");
            }

            string procName = "[dbo].[Users_Select_AuthData_ByEmail_V3]";
            string email = result.email;

            _dataProvider.ExecuteCmd(procName, delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@Email", email);
            }, delegate (IDataReader reader, short set)
            {
                user = MapUser(reader, 1);
                isSuccessful = true;
            });

            if (user == null)
            {
                ExternalUserResource newUser = new ExternalUserResource
                {
                    Email = email,
                    Password = _externalAuthConfig.DefaultPassword,
                    FirstName = result.first_name,
                    LastName = result.last_name,
                    AvatarUrl = result.picture.data.url,
                    DateCreated = DateTime.Now
                };
                int id = AddWithFacebookProfile(newUser);
                if (id > 0)
                {
                    isSuccessful = true;
                }
            }

            if (isSuccessful)
            {
                IUserAuthData response = new UserBase
                {
                    Id = user.Id
                 ,
                    Name = user.Name
                 ,
                    Roles = user.Roles
                 ,
                    TenantId = "Welrus-00.1.2"
                };

                Claim fullName = new Claim("CustomClaim", "WelrusApp");
                await _authenticationService.LogInAsync(response, new Claim[] { fullName });
            }

            return isSuccessful;
        }

        public async Task<bool> GoogleAuthAsync(string token)
        {
            bool isSuccessful = false;
            var result = await GetAsync<dynamic>(token, "me", "fields=first_name,last_name,email,picture.width(100).height(100)");
            UserBase user = null;

            if (result == null)
            {
                throw new Exception("User from this token not exist");
            }

            string procName = "[dbo].[Users_Select_AuthData_ByEmail_V3]";
            string email = result.email;

            _dataProvider.ExecuteCmd(procName, delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@Email", email);
            }, delegate (IDataReader reader, short set)
            {
                user = MapUser(reader, 1);
                isSuccessful = true;
            });

            if (user == null)
            {
               ExternalUserResource newUser = new ExternalUserResource
               {
                    Email = email,
                    Password = _externalAuthConfig.DefaultPassword,
                    FirstName = result.first_name,
                    LastName = result.last_name,
                    AvatarUrl = result.picture.data.url,
                    DateCreated = DateTime.Now
                };
                int id = AddWithGoogleProfile(newUser);
                if (id > 0)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        private async Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null)
        {
            var response = await _httpClient.GetAsync($"{endpoint}?access_token={accessToken}&{args}");
            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }

        private static UserBase MapUser(IDataReader reader, int startingIndex)
        {
            UserBase user = new UserBase();
            user.Id = reader.GetSafeInt32(startingIndex++);
            user.Name = reader.GetSafeString(startingIndex++);
            string rolesString = reader.GetSafeString(startingIndex++);
            if (!string.IsNullOrEmpty(rolesString))
            {
                List<UserRole> rolesList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserRole>>(rolesString);
                List<string> strRolesList = new List<string>();

                foreach (UserRole userRole in rolesList)
                {
                    strRolesList.Add(userRole.Name);
                }
                user.Roles = strRolesList.AsEnumerable();
            }
            return user;
        }

        private static UserDetail MapUser(IDataReader reader, out int startingIndex)
        {
            UserDetail aUser = new UserDetail();
            startingIndex = 0;

            aUser.Id = reader.GetSafeInt32(startingIndex++);
            aUser.Email = reader.GetSafeString(startingIndex++);
            aUser.IsConfirmed = reader.GetSafeBool(startingIndex++);
            aUser.UserStatusId = reader.GetSafeInt32(startingIndex++);
            aUser.UserStatus = reader.GetSafeString(startingIndex++);
            aUser.DateCreated = reader.GetSafeDateTime(startingIndex++);
            aUser.DateModified = reader.GetSafeDateTime(startingIndex++);

            string rolesString = reader.GetSafeString(startingIndex++);
            if (!string.IsNullOrEmpty(rolesString))
            {
                List<UserRole> rolesList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserRole>>(rolesString);

                foreach (UserRole userRole in rolesList)
                {
                    if (aUser.Roles == null)
                    {
                        aUser.Roles = new List<string>();
                    }
                    aUser.Roles.Add(userRole.Name);
                }
            }
            aUser.ProfileId = reader.GetSafeInt32(startingIndex++);
            aUser.FirstName = reader.GetSafeString(startingIndex++);
            aUser.LastName = reader.GetSafeString(startingIndex++);
            aUser.Mi = reader.GetSafeString(startingIndex++);
            aUser.AvatarUrl = reader.GetSafeString(startingIndex++);

            return aUser;
        }

        public async Task<bool> LogInTest(string email, string password, int id, string[] roles = null)
        {
            bool isSuccessful = false;
            var testRoles = new[] { "User", "Super", "Content Manager" };

            var allRoles = roles == null ? testRoles : testRoles.Concat(roles);

            IUserAuthData response = new UserBase
            {
                Id = id
                ,
                Name = email
                ,
                Roles = allRoles
                ,
                TenantId = "Acme Corp UId"
            };

            Claim fullName = new Claim("CustomClaim", "Sabio Bootcamp");
            await _authenticationService.LogInAsync(response, new Claim[] { fullName });

            return isSuccessful;
        }

        public int Create(object userModel)
        {
            //make sure the password column can hold long enough string. put it to 100 to be safe

            int userId = 0;
            string password = "Get from user model when you have a concreate class";
            string salt = BCrypt.BCryptHelper.GenerateSalt();
            string hashedPassword = BCrypt.BCryptHelper.HashPassword(password, "");

            //DB provider call to create user and get us a user id

            //be sure to store both salt and passwordHash
            //DO NOT STORE the original password value that the user passed us

            return userId;
        }

        /// <summary>
        /// Gets the Data call to get a give user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        private IUserAuthData Get(string email, string password)
        {
            //make sure the password column can hold long enough string. put it to 100 to be safe
            string passwordFromDb = "";
            UserBase user = null;

            //get user object from db;

            bool isValidCredentials = BCrypt.BCryptHelper.CheckPassword(password, passwordFromDb);

            return user;
        }

    }
}
