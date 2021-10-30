using Sabio.Models.Domain.Provider;
using Sabio.Models.Requests.Locations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sabio.Services.Interfaces
{
    public interface IMedicalServiceService
    {
        public List<MedicalService> GetAllByKeyword(string keyword);
        List<MedicalServiceLocation> GetServiceLocation(ProviderServiceLocationGetRequest model);
    }
}
