using AppStoreConnect.Models.Filters.Certificates;
using AppStoreConnect.Models.Requests.Certificates;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppStoreConnect.Abstractions
{
    public interface ICertificates
    {
        Task<ApplicationResponse> CreateCertificate(CertificateCreateRequest payload);
        Task<ApplicationResponse> GetCertificate(string id, GetCertificateFilters? filters = null);
        Task<ApplicationResponse> ListCertificates(ListCertificatesFilters? filters = null);
        Task<ApplicationResponse> RevokeCertificate(string id);
    }
}
