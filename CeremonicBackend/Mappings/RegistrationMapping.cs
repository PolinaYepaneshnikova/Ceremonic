using CeremonicBackend.WebApiModels;

namespace CeremonicBackend.Mappings
{
    public static class RegistrationMapping
    {
        public static RegistrationApiModel ToRegistrationApiModel(this GoogleRegistrationApiModel model)
            => new RegistrationApiModel()
            {
                Email = "Undefined",
                Password = "GoogleAPI",
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
    }
}
