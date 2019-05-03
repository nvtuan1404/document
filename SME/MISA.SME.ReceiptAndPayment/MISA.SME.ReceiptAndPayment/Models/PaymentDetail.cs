using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  MISA.SME.ReceiptAndPayment.Models
{
    public class PaymentDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("paymentDetailID")]
        public long PaymentDetailID { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("amountOC")]
        public long AmountOC { get; set; }
    }
}
