using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Provider;
using Sabio.Models.Requests.Locations;
using Sabio.Models.Requests.ProviderDetails.Details;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sabio.Services
{
    public class ProviderServiceService : IProviderServiceService
    {
        IDataProvider _data = null;
        ILocationMapperService _locationMapper = null;
        IUserProfileMapperService _userProfileMapper = null;

        public ProviderServiceService(IDataProvider data, ILocationMapperService locationMapper, IUserProfileMapperService userProfileMapper)
        {
            _data = data;
            _locationMapper = locationMapper;
            _userProfileMapper = userProfileMapper;
        }

        public List<ProviderService> GetAllByProviderIdAndScheduleId(int providerId, int scheduleId)
        {
            ProviderService providerService = new ProviderService();
            List<ProviderService> providerServiceList = null;

            string procName = "[dbo].[ProviderServices_Select_ByProviderIdAndScheduleIdV2]";

            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@ProviderId", providerId);
                parameterCollection.AddWithValue("@ScheduleId", scheduleId);
            },
            singleRecordMapper: delegate (IDataReader reader, short set)
            {
                providerService = MapProviderService(reader, out int index);

                if (providerServiceList == null)
                {
                    providerServiceList = new List<ProviderService>();
                }

                providerServiceList.Add(providerService);
            },
            returnParameters: null);

            return providerServiceList;
        }


        public Paged<ProviderService> GetAllByProviderIdAndScheduleIdPaginated(int providerId, int scheduleId, int pageIndex, int pageSize)
        {
            Paged<ProviderService> pagedList = null;
            List<ProviderService> providerServiceList = null;
            int totalCount = 0;


            string procName = "[dbo].[ProviderServices_SelectPaginated_ByProviderIdAndScheduleId]";

            _data.ExecuteCmd(
                procName,
                inputParamMapper: delegate (SqlParameterCollection paramsCollection)
                {
                    paramsCollection.AddWithValue("@ProviderId", providerId);
                    paramsCollection.AddWithValue("@ScheduleId", scheduleId);

                    paramsCollection.AddWithValue("@pageIndex", pageIndex);
                    paramsCollection.AddWithValue("@pageSize", pageSize);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    ProviderService providerService = new ProviderService();

                    providerService = MapProviderService(reader, out int index);

                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(index++);

                    }

                    if (providerServiceList == null)
                    {
                        providerServiceList = new List<ProviderService>();
                    }

                    providerServiceList.Add(providerService);
                });
            if (pagedList == null)
            {
                pagedList = new Paged<ProviderService>(providerServiceList, pageIndex, pageSize, totalCount);
            };
            return pagedList;
        }

        public Paged<ProviderService> Search(int providerId, int scheduleId,  int pageIndex, int pageSize, string keyword)
        {
            Paged<ProviderService> pagedList = null;
            List<ProviderService> providerServiceList = null;
            int totalCount = 0;


            string procName = "[dbo].[ProviderServices_SearchPaginated_ByProviderIdAndScheduleId]";

            _data.ExecuteCmd(procName, inputParamMapper:
                delegate (SqlParameterCollection paramsCollection)
                {
                    paramsCollection.AddWithValue("@ProviderId", providerId);
                    paramsCollection.AddWithValue("@ScheduleId", scheduleId);

                    paramsCollection.AddWithValue("@pageIndex", pageIndex);
                    paramsCollection.AddWithValue("@pageSize", pageSize);
                    paramsCollection.AddWithValue("@keyword", keyword);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    ProviderService providerService = new ProviderService();

                    providerService = MapProviderService(reader, out int index);
                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(index++);

                    }
                    if (providerServiceList == null)
                    {
                        providerServiceList = new List<ProviderService>();
                    }

                    providerServiceList.Add(providerService);
                });
            if (pagedList == null)
            {
                pagedList = new Paged<ProviderService>(providerServiceList, pageIndex, pageSize, totalCount);
            };
            return pagedList;
        }



        public int Add(ProviderServiceAddRequest model)
        {
            int id = 0;
            string procName = "[dbo].[ProviderServices_Insert_V2]";

            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection paramsCollection)
                {
                    AddCommonParams(model, paramsCollection);

                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;

                    paramsCollection.Add(idOut);
                },
                returnParameters: delegate (SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@Id"].Value;
                    int.TryParse(oId.ToString(), out id);
                }
            );
            return id;
        }

        public List<MedicalProviderLocation> GetProviderLocation(ProviderLocationGetRequest model)
        {
            MedicalProviderLocation providerLocation = null;
            Location location = null;
            List<MedicalProviderLocation> list = null;
            UserProfile userProfile = null;

            string procName = "[dbo].[Providers_Nearest_ByGeo]";
            _data.ExecuteCmd(procName, delegate (SqlParameterCollection col)
            {

                col.AddWithValue("@latitude", model.Latitude);
                col.AddWithValue("@longitude", model.Longitude);
                col.AddWithValue("@radius", model.Radius);


            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                providerLocation = new MedicalProviderLocation();

                providerLocation.Id = reader.GetSafeInt32(startingIndex++);

                location = _locationMapper.MapALocation(reader, ref startingIndex);
                providerLocation.Location = location;

                providerLocation.ProviderId = reader.GetSafeInt32(startingIndex++);

                userProfile = _userProfileMapper.MapUserProfile(reader, ref startingIndex);
                providerLocation.UserProfile = userProfile;

                if (list == null)
                {
                    list = new List<MedicalProviderLocation>();
                }
                list.Add(providerLocation);

            }, returnParameters: null);

            return list;
        }

        public List<MedicalServiceCount> GetTopMedicalServicesByDate(int providerId, DateTime startDate, DateTime endDate)
        {
            string procName = "[dbo].[ProviderAppointments_Select_By_ProviderId_And_Date]";
            List<MedicalServiceCount> list = null;
            MedicalServiceCount service = null;

            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection paramsCollection)
            {
                paramsCollection.AddWithValue("@ProviderId", providerId);
                paramsCollection.AddWithValue("@Start", startDate);
                paramsCollection.AddWithValue("@End", endDate);

            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                service = MapMedicalServiceCount(reader);

                if (list == null)
                {
                    list = new List<MedicalServiceCount>();
                }

                list.Add(service);
            });
            return list;
        }

        public List<MedicalServiceCount> GetTopMedicalServicesByProviderId(int providerId)
        {
            string procName = "[dbo].[ProviderAppointments_Report_Top_6_Services__Last_30_Days_V2]";
            List<MedicalServiceCount> list = null;
            MedicalServiceCount service = null;

            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection paramsCollection)
            {
                paramsCollection.AddWithValue("@ProviderId", providerId);
            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                service = MapMedicalServiceCount(reader);

                if (list == null)
                {
                    list = new List<MedicalServiceCount>();
                }

                list.Add(service);
            });
            return list;
        }

        #region Get support methods

        private static MedicalServiceCount MapMedicalServiceCount(IDataReader reader)
        {
            MedicalServiceCount service;
            int startingIndex = 0;
            service = new MedicalServiceCount();
            service.TotalCount = reader.GetSafeInt32(startingIndex++);
            service.Id = reader.GetSafeInt32(startingIndex++);
            service.Name = reader.GetSafeString(startingIndex++);
            service.Cpt4Code = reader.GetSafeString(startingIndex++);
            service.Price = reader.GetSafeDecimal(startingIndex++);
            return service;
        }

        private static ProviderService MapProviderService(IDataReader reader, out int startingIndex)
        {
            ProviderService providerService = new ProviderService();

            startingIndex = 0;
            providerService.Id = reader.GetSafeInt32(startingIndex++);
            providerService.ProviderId = reader.GetSafeInt32(startingIndex++);
            providerService.Price = reader.GetSafeDecimal(startingIndex++);

            providerService.MedicalService = new MedicalService();
            providerService.MedicalService.Id = reader.GetSafeInt32(startingIndex++);
            providerService.MedicalService.Name = reader.GetSafeString(startingIndex++);
            providerService.MedicalService.Cpt4Code = reader.GetSafeString(startingIndex++);

            providerService.MedicalServiceType = new MedicalServiceType();
            providerService.MedicalServiceType.Id = reader.GetSafeInt32(startingIndex++);
            providerService.MedicalServiceType.Name = reader.GetSafeString(startingIndex++);

            return providerService;
        }

        #endregion

        #region Add support methods

        private static void AddCommonParams(ProviderServiceAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@ProviderId", model.ProviderId);
            col.AddWithValue("@Price", model.Price);
            col.AddWithValue("@ServiceId", model.ServiceId);
            col.AddWithValue("@ServiceTypeId", model.ServiceTypeId);
            col.AddWithValue("@ScheduleId", model.ScheduleId);
        }


        #endregion

    }
}
