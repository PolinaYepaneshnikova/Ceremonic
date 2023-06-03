using System;

namespace CeremonicBackend.WebApiModels
{
    public class MessageApiModel
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public string Text { get; set; }

        public string ImageFileName { get; set; }

        public string FileName { get; set; }

        public DateTime PostedAt { get; set; }

        public bool NotViewed { get; set; }
    }
}
