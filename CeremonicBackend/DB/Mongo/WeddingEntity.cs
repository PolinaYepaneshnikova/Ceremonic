using System;
using System.Collections.Generic;

using MongoDB.Bson.Serialization.Attributes;

namespace CeremonicBackend.DB.Mongo
{
    public class WeddingEntity : JoinedToUserEntity
    {
        [BsonIgnore]
        public PersonEntity User1
        {
            get => GuestMap?.Guests?[0];
            set
            {
                SettingBeforeSetUser();
                SetUser(GuestMap.Guests[0], value);
            }
        }

        [BsonIgnore]
        public PersonEntity User2
        {
            get => GuestMap?.Guests?[1];
            set
            {
                SettingBeforeSetUser();
                SetUser(GuestMap.Guests[1], value);
            }
        }

        public string Geolocation { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }
        public RangeEntity GuestCountRange { get; set; }
        public GuestMapEntity GuestMap { get; set; }
        public object WeddingPlan { get; set; }
        public List<TeamRoleEntity> WeddingTeam { get; set; }

        public List<ProviderEntity> MyFavorites { get; set; }


        public RangeEntity ApproximateBudget { get; set; }
        public object Budget { get; set; }


        void SettingBeforeSetUser()
        {
            GuestMap ??= new GuestMapEntity();
            if (GuestMap.Guests is null || GuestMap.Guests.Count < 2)
            {
                GuestMap.Guests = new List<PersonEntity>()
                {
                    new PersonEntity(),
                    new PersonEntity(),
                };
            }
        }

        void SetUser(PersonEntity setPerson, PersonEntity value)
        {
            setPerson.FullName = value.FullName;
            setPerson.AvatarFileName = value.AvatarFileName;
            setPerson.Email = setPerson.Email;
            setPerson.PlusGuests = value.PlusGuests;
            setPerson.CategoryId = value.CategoryId;
            setPerson.WillCome = value.WillCome;
            setPerson.Notes = value.Notes;
        }
    }
}
