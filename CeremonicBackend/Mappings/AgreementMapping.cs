using System;
using System.Threading.Tasks;

using CeremonicBackend.DB.Relational;
using CeremonicBackend.Repositories.Interfaces;
using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Mappings
{
    public static class AgreementMapping
    {
        public static AgreementApiModel ToAgreementApiModel(this AgreementEntity entity)
            => new AgreementApiModel()
            {
                Id = entity.Id,
                ProviderId = entity.ProviderId,
                ClientId = entity.ClientId,
                DateTime = entity.DateTime,
                Price = entity.Price,
                Location = entity.Location,
                Service = entity.Service,
                ConfirmStatus = entity.ConfirmStatus,
            };

        public static async Task<AgreementEntity> ToAgreementEntity(this SendAgreementApiModel model, string providerEmail, DateTime postedAt, IUserRepository userRepository)
            => new AgreementEntity()
            {
                ProviderId = (await userRepository.GetByEmail(providerEmail)).Id,
                ClientId = model.ClientId,
                DateTime = postedAt,
                Price = model.Price,
                Location = model.Location,
                Service = model.Service,
                ConfirmStatus = ConfirmStatus.NoInfo,
            };
    }
}
