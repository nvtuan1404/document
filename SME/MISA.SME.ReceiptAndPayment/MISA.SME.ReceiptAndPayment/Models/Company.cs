using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  MISA.SME.ReceiptAndPayment.Models
{
    public class Company
    {
        public ObjectId Id { get; set; }

        [BsonElement("companyID")]
        public string CompanyID { get; set; }

        [BsonElement("payments")]
        public List<ObjectId> PaymentsObjectId { get; set; }
        [BsonElement("receipt")]
        public List<ObjectId> ReceiptObjectId { get; set; }
    }
}
