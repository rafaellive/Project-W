using Sabio.Models.Domain.Provider;
using Sabio.Models.Requests.Appointments;
using Sabio.Models.Requests.ProviderDetails.Appointments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sabio.Services.Interfaces
{
    public interface IAppointmentService
    {
        List<Appointment> GetLast30DaysByProviderId(int providerId);
        List<int> AddBatch(List<AppointmentAddRequest> model);
        Appointment Get(int id);
        int Add(AppointmentAddRequest model);
        List<Appointment> GetAllByProviderId(int ProviderId);
        void Update(AppointmentUpdateRequest model);
        void UpdateAppointmentStatus(AppointmentUpdateStatusRequest model);
        void Delete(int id);
    }
}
