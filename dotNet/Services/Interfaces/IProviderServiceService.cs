using Sabio.Models;
using Sabio.Models.Domain.Provider;
using Sabio.Models.Requests.Locations;
using Sabio.Models.Requests.ProviderDetails.Details;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sabio.Services.Interfaces
{
    public interface IProviderServiceService
    {
        public List<ProviderService> GetAllByProviderIdAndScheduleId(int providerId, int scheduleId);
        public Paged<ProviderService> GetAllByProviderIdAndScheduleIdPaginated(int providerId, int scheduleId, int pageIndex, int pageSize);
        public Paged<ProviderService> Search(int providerId, int scheduleId, int pageIndex, int pageSize, string keyword);
        int Add(ProviderServiceAddRequest model);
        List<MedicalServiceCount> GetTopMedicalServicesByDate(int providerId, DateTime startDate, DateTime endDate);
        List<MedicalServiceCount> GetTopMedicalServicesByProviderId(int providerId);
        List<MedicalProviderLocation> GetProviderLocation(ProviderLocationGetRequest model);
    }
}
