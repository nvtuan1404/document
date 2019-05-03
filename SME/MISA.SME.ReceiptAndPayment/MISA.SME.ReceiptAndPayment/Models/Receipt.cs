using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  MISA.SME.ReceiptAndPayment.Models
{
    public class Receipt
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("receiptId")]
        public long ReceiptID { get; set; }

        [BsonElement("refTypeName")]
        public string RefTypeName { get; set; }

        [BsonElement("refDate")]
        public DateTime RefDate { get; set; }

        [BsonElement("postedDate")]
        public DateTime PostedDate { get; set; }

        [BsonElement("refNoFiance")]
        public string RefNoFiance { get; set; }

        [BsonElement("refNo")]
        public string RefNo { get; set; }

        [BsonElement("isPostedFinance")]
        public double IsPostedFinance { get; set; }

        [BsonElement("accountObjectId")]
        public string AccountObjectId { get; set; }

        [BsonElement("accountObjectName")]
        public string AccountObjectName { get; set; }

        [BsonElement("contactName")]
        public string ContactName { get; set; }

        [BsonElement("reasonTypeName")]
        public string ReasonTypeName { get; set; }

        [BsonElement("journalMemo")]
        public string JournalMemo { get; set; }

        [BsonElement("document")]
        public Int32 Document { get; set; }

        [BsonElement("currencyId")]
        public string CurrencyID { get; set; }

        [BsonElement("exchangeRate")]
        public double ExchangeRate { get; set; }

        [BsonElement("totalAmountOC")]
        public double TotalAmountOC { get; set; }

        [BsonElement("totalAmount")]
        public long TotalAmount { get; set; }

        [BsonElement("editVersion")]
        public string EditVersion { get; set; }

        [BsonElement("refOrder")]
        public string RefOrder { get; set; }

        [BsonElement("cashBookPostedDate")]
        public DateTime CashBookPostedDate { get; set; }

        [BsonElement("createBy")]
        public string CreateBy { get; set; }

        [BsonElement("modifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [BsonElement("modifiedBy")]
        public string ModifiedBy { get; set; }

        [BsonElement("receiptDetail")]
        public List<ReceiptDetail> ReceiptDetail { get; set; }

        [BsonElement("departmentName")]
        public string DepartmentName { get; set; }

        [BsonElement("companyId")]
        public string CompanyId { get; set; }
    }
}
