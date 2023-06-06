using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Services.Interfaces
{
    public interface IAgreementService
    {
        public Task<AgreementApiModel> Create(string providerEmail, SendAgreementApiModel agreement);
        public Task<AgreementApiModel> Get(int id);
        public Task<AgreementApiModel> Confirm(string userEmail, int id);
        public Task<AgreementApiModel> Cancel(string userEmail, int id);
        public Task<List<ProviderApiModel>> MyProviders(string userEmail);
    }
}
