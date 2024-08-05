using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace BlogApi.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime DateCreated { get; set; }
        public string Text { get; set; }

        // This property will calculate the EncryptedId dynamically

        public string EncryptedId { get; set; }

        //public string EncryptedId => EncryptionHelper.EncryptId(Id);
    }


    public class BlogPostResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public DateTime DateCreated { get; set; }
        public string Text { get; set; }
    }

}
