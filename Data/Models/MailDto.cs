using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Data.Models
{
    public class MailDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string MailSubject { get; set; }
        public string MailText { get; set; }
        public string SenderEmail { get; set; }
        public string ArriveEmail { get; set; }
    }
}
