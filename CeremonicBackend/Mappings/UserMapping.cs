using CeremonicBackend.DB.Relational;
using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Mappings
{
    public static class UserMapping
    {
        public static UserApiModel ToUserApiModel(this UserEntity entity)
            => new UserApiModel()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
            };
    }
}
