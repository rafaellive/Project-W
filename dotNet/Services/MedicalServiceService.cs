using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Provider;
using Sabio.Models.Requests.Locations;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sabio.Services
{
    public class MedicalServiceService : IMedicalServiceService
    {
        IDataProvider _data = null;
        ILocationMapperService _locationMapper = null;
        IUserProfileMapperService _userProfileMapper = null;
        public MedicalServiceService(IDataProvider data, ILocationMapperService locationMapper, IUserProfileMapperService userProfileMapper)
        {
            _data = data;
            _locationMapper = locationMapper;
            _userProfileMapper = userProfileMapper;
        }
        public List<MedicalService> GetAllByKeyword(string keyword)
        {
            string procName = "[dbo].[Services_SelectBy_Keyword]";
            List<MedicalService> serviceList = null;

            _data.ExecuteCmd(
                procName,
                inputParamMapper: delegate (SqlParameterCollection paramsCollection)
                {
                    paramsCollection.AddWithValue("@keyword", keyword);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    MedicalService medicalService = new MedicalService();
                    medicalService = MapService(reader);

                    if (serviceList == null)
                    {
                        serviceList = new List<MedicalService>();
                    }

                    serviceList.Add(medicalService);
                });
            return serviceList;
        }
        public List<MedicalServiceLocation> GetServiceLocation(ProviderServiceLocationGetRequest model)
        {
            MedicalServiceLocation serviceLocation = null;
            Location location = null;
            List<MedicalServiceLocation> list = null;
            List<ProviderMedicalService> serviceList = null;
            UserProfile userProfile = null;

            string procName = "[dbo].[Services_Search_ByGeo_V2]";
            _data.ExecuteCmd(procName, delegate (SqlParameterCollection col)
            {

                col.AddWithValue("@latitude", model.Latitude);
                col.AddWithValue("@longitude", model.Longitude);
                col.AddWithValue("@radius", model.Radius);
                col.AddWithValue("@serviceQuery", model.ServiceQuery);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;

                serviceLocation = new MedicalServiceLocation();
                serviceLocation.Id = reader.GetSafeInt32(startingIndex++);
                location = _locationMapper.MapALocation(reader, ref startingIndex);
                serviceLocation.location = location;

                serviceLocation.ProviderId = reader.GetSafeInt32(startingIndex++);
                userProfile = _userProfileMapper.MapUserProfile(reader, ref startingIndex);
                serviceLocation.UserProfile = userProfile;

                serviceList = new List<ProviderMedicalService>();
                serviceLocation.Services = serviceList;
                string servicesString = reader.GetSafeString(startingIndex++);
                if (!string.IsNullOrEmpty(servicesString))
                {
                    serviceLocation.Services = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProviderMedicalService>>(servicesString);
                }

                if (list == null)
                {
                    list = new List<MedicalServiceLocation>();
                }
                list.Add(serviceLocation);

            }, returnParameters: null);

            return list;
        }

        #region Get support methods

        private static MedicalService MapService(IDataReader reader)
        {
            MedicalService medicalService = new MedicalService();

            int startingIndex = 0;

            medicalService.Id = reader.GetSafeInt32(startingIndex++);
            medicalService.Name = reader.GetSafeString(startingIndex++);
            medicalService.Cpt4Code = reader.GetSafeString(startingIndex++);

            return medicalService;
        }

        #endregion
    }
}
