using Microsoft.Extensions.Configuration;
using MISA.SME.ReceiptAndPayment.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.SME.ReceiptAndPayment.Services
{
    public class ReceiptService
    {
        public readonly IMongoCollection<Receipt> _receipt;
        public ReceiptService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MISASMEPayment"));
            //var database = client.GetDatabase("MISA_SME_Payment_Receipt");
            var database = client.GetDatabase("MISASMECloud");
            _receipt = database.GetCollection<Receipt>("ReceiptCollection");
        }

        public List<Receipt> Get()
        {
            return _receipt.Find(receipt => true).ToList();
        }

        public Receipt Get(string id)
        {
            return _receipt.Find(receipt => receipt.Id == id).FirstOrDefault();
        }

        public Receipt Create(Receipt receipt)
        {
            try
            {
                _receipt.InsertOneAsync(receipt);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return receipt;
        }

        public void Update(Receipt receiptIn)
        {
            var docId = receiptIn.Id;
            var refNo = receiptIn.RefNo;
            _receipt.ReplaceOne(receipt => receipt.Id == docId && receipt.RefNo==refNo, receiptIn);
        }

        public void Remove(Receipt receiptIn)
        {
            var refNo = receiptIn.RefNo;
            _receipt.DeleteOne(receipt => receipt.Id == receiptIn.Id && receipt.RefNo==refNo);
        }

        public void Remove(string id)
        {
            _receipt.DeleteOne(receipt => receipt.Id == id);
        }

        public List<Receipt> GetReceiptByCompany(string companyId, int page, int totalRecord)
        {
			//toTalRecord = _receipt.Find(receipt => receipt.CompanyId == companyId).ToList().Count();
			return _receipt.Find(receipt => receipt.CompanyId == companyId).SortByDescending(receipt => receipt.ModifiedDate).Limit(totalRecord).Skip((page - 1) * totalRecord).ToList();
        }

		/// <summary>
		/// Hàm trả về tổng số phiếu thu của một công ty
		/// </summary>
		/// <param name="companyId"></param>
		/// <returns>tổng số phiếu thu</returns>
		/// Created by: nnanh - 18/1/2019
		public long TotalReceiptOfCompany(string companyId)
		{
			// edit kieu du lieu tra ve va ham return
			return _receipt.CountDocuments(receipt => receipt.CompanyId == companyId);
		}		
    }
}
