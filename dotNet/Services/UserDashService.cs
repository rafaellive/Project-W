using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Provider;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sabio.Services
{
    public class UserDashService : IUserDashService
    {
        IDataProvider _data = null;

        public UserDashService(IDataProvider data)
        {
            _data = data;
        }

        public List<UserAppointment> GetAppointments(int userId)
        {
            List<UserAppointment> userAppointmentList = null;

            string procName = "[dbo].[ProviderServices_Select_ByPatientId]";

            _data.ExecuteCmd(
                procName,
                inputParamMapper: delegate (SqlParameterCollection paramsCollection)
                {
                    paramsCollection.AddWithValue("@PatientId", userId);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    UserAppointment userAppointment = new UserAppointment();

                    userAppointment = MapAppointment(reader, out int index);

                    if (userAppointmentList == null)
                    {
                        userAppointmentList = new List<UserAppointment>();
                    }

                    userAppointmentList.Add(userAppointment);
                });

            return userAppointmentList;
        }

        private static UserAppointment MapAppointment(IDataReader reader, out int startingIndex)
        {
            UserAppointment userAppointment = new UserAppointment();
            startingIndex = 0;
            userAppointment.AppointmentId = reader.GetSafeInt32(startingIndex++);
            userAppointment.AppointmentStart = reader.GetSafeDateTime(startingIndex++);
            userAppointment.AppointmentEnd = reader.GetSafeDateTime(startingIndex++);

            string providerServicesString = reader.GetSafeString(startingIndex++);
            if (!string.IsNullOrEmpty(providerServicesString))
            {
                userAppointment.ProviderServices = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProviderService>>(providerServicesString);
                
            }

            return userAppointment;
        }
    }
}
