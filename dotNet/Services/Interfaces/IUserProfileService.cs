using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.UserProfiles;
using Sabio.Models.Requests.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sabio.Services.Interfaces
{
    public interface IUserProfileService
    {
        int Add(UserProfileAddRequest model, int userId);
        UserProfile Get(int id);
        Paged<UserProfile> GetPaginate(int pageIndex, int pageSize);
        void Update(UserProfileUpdateRequest model, int userId);

    }
}
