using Sabio.Models.Domain;
using Sabio.Models.Requests;
using Sabio.Models.Requests.Users;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sabio.Services
{
    public interface IUserService
    {
        int Create(object userModel);

        int Add(UserAddRequest model);
        List<User> GetAllUser();
        int GetIdByEmail(string email);

        void PasswordUpdate(UserPasswordUpdateRequest model);

        void UpdateConfirm(Guid token);

        UserDetail GetProfile(int id);

        void Update(UserUpdateRequest model);

        Task<bool> LogInAsync(string email, string password);

        Task<bool> FacebookAuthAsync(string token);
        Task<bool> GoogleAuthAsync(string token);

        Task<bool> LogInTest(string email, string password, int id, string[] roles = null);
    }
}
