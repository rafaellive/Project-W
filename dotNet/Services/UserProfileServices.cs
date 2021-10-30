using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.UserProfiles;
using Sabio.Models.Requests.Users;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace Sabio.Services
{
    public class UserProfileServices : IUserProfileService, IUserProfileMapperService
    {
        IDataProvider _data = null;
        public UserProfileServices(IDataProvider data)
        {
            _data = data;
        }
        public int Add(UserProfileAddRequest model, int userId)
        {
            int id = 0;

            string procName = "[dbo].[UserProfiles_Insert]";
            _data.ExecuteNonQuery(procName,
            inputParamMapper: delegate (SqlParameterCollection collection)
            {
                AddUserProfileParams(model, collection);
                collection.AddWithValue("@UserId", userId);

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
        public UserProfile Get(int userId)
        {
            string procName = "[dbo].[UserProfiles_SelectBy_UserId]";
            UserProfile userprofile = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@UserId", userId);
            }, delegate (IDataReader reader, short set)
            {
                int index = 0;
                userprofile = MapUserProfile(reader, ref index);
            });
            return userprofile;
        }

        public Paged<UserProfile> GetPaginate(int pageIndex, int pageSize)
        {
            Paged<UserProfile> pagedList = null;
            List<UserProfile> list = null;
            int totalCount = 0;

            UserProfile userProfile = null;
            string procName = "[dbo].[UserProfiles_SelectAll]";

            _data.ExecuteCmd(
                procName, inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@pageIndex", pageIndex);
                    paramCollection.AddWithValue("@pageSize", pageSize);
                },
            singleRecordMapper: delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                userProfile = MapUserProfile(reader, ref startingIndex);
                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }
                if (list == null)
                {
                    list = new List<UserProfile>();
                }
                list.Add(userProfile);
            });
            if (list != null)
            {
                pagedList = new Paged<UserProfile>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public UserProfile MapUserProfile(IDataReader reader, ref int startingIndex)
        {
            UserProfile aUserProfile = new UserProfile();

            aUserProfile.Id = reader.GetSafeInt32(startingIndex++);
            aUserProfile.UserId = reader.GetSafeInt32(startingIndex++);
            aUserProfile.FirstName = reader.GetSafeString(startingIndex++);
            aUserProfile.LastName = reader.GetSafeString(startingIndex++);
            aUserProfile.Mi = reader.GetSafeString(startingIndex++);
            aUserProfile.AvatarUrl = reader.GetSafeString(startingIndex++);
            aUserProfile.PhoneNumber = reader.GetSafeString(startingIndex++);
            aUserProfile.DateCreated = reader.GetSafeDateTime(startingIndex++);
            aUserProfile.DateModified = reader.GetSafeDateTime(startingIndex++);
           
            return aUserProfile;
        }

        public void Update(UserProfileUpdateRequest model, int userId)
        {
            string procName = "[dbo].[UserProfiles_Update]";
            _data.ExecuteNonQuery(procName,
            inputParamMapper: delegate (SqlParameterCollection collection)
            {
                AddUserProfileParams(model, collection);
                collection.AddWithValue("@UserId", userId);
            },
            returnParameters: null);
            Console.WriteLine("update in service");
        }

        public static void AddUserProfileParams(UserProfileAddRequest model, SqlParameterCollection collection)
        {
            collection.AddWithValue("@FirstName", model.FirstName);
            collection.AddWithValue("@LastName", model.LastName);
            collection.AddWithValue("@Mi", model.Mi);
            collection.AddWithValue("@AvatarUrl", model.AvatarUrl);
            collection.AddWithValue("@PhoneNumber", model.PhoneNumber);
        }

    }
}
