using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  MISA.SME.ReceiptAndPayment.Models
{
    public class ReceiptDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("reDetailID")]
        public long ReceiptDetailID { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("amountOC")]
        public long AmountOC { get; set; }
    }
}
