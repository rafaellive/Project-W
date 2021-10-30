using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Provider;
using Sabio.Models.Requests.Appointments;
using Sabio.Models.Requests.ProviderDetails.Appointments;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace Sabio.Services.Providers
{
    public class AppointmentService : IAppointmentService
    {
        IDataProvider _data = null;

        public AppointmentService(IDataProvider data)
        {
            _data = data;
        }

        public Appointment Get(int id)
        {
            string procName = "[dbo].[ProviderAppointments_Select_ById_V2]";
            Appointment appointment = null;

            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection collection)
             {
                 collection.AddWithValue("@Id", id);
             }, singleRecordMapper: delegate (IDataReader reader, short set)
             {
                 appointment = MapAppointment(reader);

             }, returnParameters: null);
            return appointment;

        }
        public List<int> AddBatch(List<AppointmentAddRequest> model)
        {
            string procName = "[dbo].[ProviderAppointments_Insert_Batch]";
            List<int> list = null;
            int id = 0;
            DataTable paramValues = MapBatchAppointments(model);

            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {
                collection.AddWithValue("@BatchProviderAppointments", paramValues);
            },
            singleRecordMapper: delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                id = reader.GetSafeInt32(startingIndex++);
                if (list == null)
                {
                    list = new List<int>();
                }
                list.Add(id);

            });

            return list;
        }

        public int Add(AppointmentAddRequest model)
        {
            int id = 0;

            string procName = "[dbo].[ProviderAppointments_Insert_V2]";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {

                DataTable dt = CreateParamForProviderServices(model.ProviderServiceIds);
                collection.AddWithValue("@listOfProviderServices", dt);

                AddCommonParams(model, collection);

                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;

                collection.Add(idOut);

            }, returnParameters: delegate (SqlParameterCollection returnColection)
            {
                object oId = returnColection["@Id"].Value;

                int.TryParse(oId.ToString(), out id);
            });
            return id;
        }

        public List<Appointment> GetAllByProviderId(int providerId)
        {
            string procName = "[dbo].[ProviderAppointments_Select_By_ProviderId_V4]";
            List<Appointment> list = null;
            Appointment appointment = null;


            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {
                collection.AddWithValue("@ProviderId", providerId);
            }
           , singleRecordMapper: delegate (IDataReader reader, short set)
            {
                appointment = MapAppointment(reader);

                if (list == null)
                {
                    list = new List<Appointment>();
                }
                list.Add(appointment);
            },
            returnParameters: null
            );
            return list;
        }
        public List<Appointment> GetLast30DaysByProviderId(int providerId)
        {
            string storedProc = "[dbo].[ProviderAppointments_Report_Created_Last_30_DaysV3]";
            List<Appointment> list = null;
            Appointment appointment = null;
            _data.ExecuteCmd(storedProc, inputParamMapper: delegate (SqlParameterCollection collection)
            {
                collection.AddWithValue("@ProviderId", providerId);
            }, singleRecordMapper: delegate (IDataReader reader, short set)
             {
                 appointment = MapAppointment(reader);

                 if (list == null)
                 {
                     list = new List<Appointment>();
                 }
                 list.Add(appointment);
             },
            returnParameters: null
            );
            return list;
        }
        public void Update(AppointmentUpdateRequest model)
        {
            string procName = "[dbo].[ProviderAppointments_Update]";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {
                AddCommonParams(model, collection);
            },
            returnParameters: null);

        }

        public void UpdateAppointmentStatus(AppointmentUpdateStatusRequest model)
        {
            string procName = "[dbo].[ProviderAppointments_Update_V2]";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {
                collection.AddWithValue("@UserId", model.UserId);
                collection.AddWithValue("@IsConfirmed", model.IsConfirmed);
                collection.AddWithValue("@AppointmentStart", model.AppointmentStart);
                collection.AddWithValue("@AppointmentEnd", model.AppointmentEnd);
            },
            returnParameters: null);

        }
        public void Delete(int id)
        {
            string procName = "[dbo].[ProviderAppointments_Delete]";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection collection)
            {
                collection.AddWithValue("@Id", id);
            },
            returnParameters: null);
        }

        #region Get support methods
        private static Appointment MapAppointment(IDataReader reader)
        {
            Appointment appointment = new Appointment();
            int startingIndex = 0;
            appointment.Id = reader.GetSafeInt32(startingIndex++);
            appointment.ProviderId = reader.GetSafeInt32(startingIndex++);

            UserProfile userProfile = new UserProfile();
            appointment.UserProfile = userProfile;
            userProfile.Id = reader.GetSafeInt32(startingIndex++);
            userProfile.UserId = reader.GetSafeInt32(startingIndex++);
            userProfile.FirstName = reader.GetSafeString(startingIndex++);
            userProfile.Mi = reader.GetSafeString(startingIndex++);
            userProfile.LastName = reader.GetSafeString(startingIndex++);
            userProfile.AvatarUrl = reader.GetSafeString(startingIndex++);
            userProfile.DateCreated = reader.GetSafeDateTime(startingIndex++);
            userProfile.DateModified = reader.GetSafeDateTime(startingIndex++);

            ProviderService providerService = new ProviderService();
            appointment.providerService = providerService;
            providerService.Id = reader.GetSafeInt32(startingIndex++);
            providerService.ProviderId = reader.GetSafeInt32(startingIndex++);
            providerService.Price = reader.GetSafeDecimal(startingIndex++);

            MedicalService medicalService = new MedicalService();
            appointment.MedicalService = medicalService;
            medicalService.Id = reader.GetSafeInt32(startingIndex++);
            medicalService.Name = reader.GetSafeString(startingIndex++);
            medicalService.Cpt4Code = reader.GetSafeString(startingIndex++);

            MedicalServiceType medicalServiceType = new MedicalServiceType();
            appointment.MedicalServiceType = medicalServiceType;
            medicalServiceType.Id = reader.GetSafeInt32(startingIndex++);
            medicalServiceType.Name = reader.GetSafeString(startingIndex++);
            appointment.IsConfirmed = reader.GetSafeBool(startingIndex++);
            appointment.StartTime = reader.GetSafeDateTime(startingIndex++);
            appointment.DateCreated = reader.GetSafeDateTime(startingIndex++);
            appointment.DateModified = reader.GetSafeDateTime(startingIndex++);

            return appointment;
        }

        private DataTable MapBatchAppointments(List<AppointmentAddRequest> appointments)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProviderId", typeof(int));

            dt.Columns.Add("Status", typeof(bool));
            dt.Columns.Add("ProviderAppointment", typeof(DateTime));
            dt.Columns.Add("ServiceId", typeof(int));

            foreach (AppointmentAddRequest singleAppointment in appointments)
            {
                DataRow dr = dt.NewRow();
                int startingIndex = 0;

                dr.SetField(startingIndex++, singleAppointment.UserId);
                dr.SetField(startingIndex++, singleAppointment.IsConfirmed);
                dr.SetField(startingIndex++, singleAppointment.AppointmentStart);
                dr.SetField(startingIndex++, singleAppointment.AppointmentEnd);

                dt.Rows.Add(dr);
            }
            return dt;

        }
        #endregion

        #region Add support methods

        public DataTable CreateParamForProviderServices(List<int> providerServicesToMap)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ProviderServiceId", typeof(Int32));

            foreach (int singleProviderService in providerServicesToMap)
            {
                DataRow dr = dt.NewRow();
                int startingIndex = 0;

                dr.SetField(startingIndex++, singleProviderService);

                dt.Rows.Add(dr);
            }

            return dt;
        }

        private static void AddCommonParams(AppointmentAddRequest model, SqlParameterCollection collection)
        {
            collection.AddWithValue("@UserId", model.UserId);
            collection.AddWithValue("@IsConfirmed", model.IsConfirmed);
            collection.AddWithValue("@AppointmentStart", model.AppointmentStart);
            collection.AddWithValue("@AppointmentEnd", model.AppointmentEnd);
        } 
        #endregion
    }
}
