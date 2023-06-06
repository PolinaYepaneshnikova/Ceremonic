using System;
using System.Threading.Tasks;

using CeremonicBackend.DB.Mongo;
using CeremonicBackend.DB.Relational;
using CeremonicBackend.Exceptions;
using CeremonicBackend.Mappings;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.Services.Interfaces;
using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Services
{
    public class AgreementService : IAgreementService
    {
        protected IUnitOfWork _UoW { get; set; }
        protected IMessagingService _messagingService { get; set; }
        public AgreementService(IUnitOfWork uow, IMessagingService messagingService)
        {
            _UoW = uow;
            _messagingService = messagingService;
        }

        public async Task<AgreementApiModel> Create(string providerEmail, SendAgreementApiModel agreement)
        {
            AgreementEntity agreementEntity = await agreement.ToAgreementEntity(providerEmail, _UoW.UserRepository);
            agreementEntity = await _UoW.AgreementRepository.Add(agreementEntity);
            await _UoW.SaveChanges();

            SendMessageApiModel message = new SendMessageApiModel()
            {
                Text = $"AgreementId: {agreementEntity.Id}",
                DestinationUserId = agreement.ClientId,
            };

            await _messagingService.SendMessage(providerEmail, message, agreementEntity.DateTime);

            await _UoW.SaveChanges();

            return agreementEntity.ToAgreementApiModel();
        }

        public async Task<AgreementApiModel> Get(int id)
        {
            AgreementEntity agreement = await _UoW.AgreementRepository.GetById(id);

            if (agreement is null)
            {
                throw new NotFoundAppException($"agreement not found");
            }

            return agreement.ToAgreementApiModel();
        }

        public async Task<AgreementApiModel> Confirm(string userEmail, int id)
        {
            AgreementEntity agreement = await _UoW.AgreementRepository.GetById(id);

            if (agreement is null)
            {
                throw new NotFoundAppException($"agreement not found");
            }

            int userId = (await _UoW.UserRepository.GetByEmail(userEmail)).Id;

            if (agreement.ClientId != userId)
            {
                throw new AccessDeniedAppException($"user does not have access to this agreement");
            }

            if (agreement.ConfirmStatus == ConfirmStatus.NoInfo)
            {
                agreement.ConfirmStatus = ConfirmStatus.Yes;
                await _UoW.AgreementRepository.Update(agreement);
                await _UoW.SaveChanges();
            }

            return agreement.ToAgreementApiModel();
        }

        public async Task<AgreementApiModel> Cancel(string userEmail, int id)
        {
            AgreementEntity agreement = await _UoW.AgreementRepository.GetById(id);

            if (agreement is null)
            {
                throw new NotFoundAppException($"agreement not found");
            }

            int userId = (await _UoW.UserRepository.GetByEmail(userEmail)).Id;

            if (agreement.ClientId != userId && agreement.ProviderId != userId)
            {
                throw new AccessDeniedAppException($"user does not have access to this agreement");
            }

            agreement.ConfirmStatus = ConfirmStatus.No;
            await _UoW.AgreementRepository.Update(agreement);
            await _UoW.SaveChanges();

            return agreement.ToAgreementApiModel();
        }
    }
}