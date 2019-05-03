using Microsoft.Extensions.Configuration;
using  MISA.SME.ReceiptAndPayment.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  MISA.SME.ReceiptAndPayment.Services
{
    public class PaymentService
    {
        public IMongoCollection<Payment> _payment;
        public PaymentService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MISASMEPayment"));
            //var database = client.GetDatabase("MISA_SME_Payment_Receipt");
            var database = client.GetDatabase("MISASMECloud");
            _payment = database.GetCollection<Payment>("Payment");
        }

        public List<Payment> Get()
        {
            return _payment.Find(payment => true).ToList();
        }

        public Payment Get(string id)
        {
            return _payment.Find(payment => payment.Id == id).FirstOrDefault();
        }

        public Payment Create(Payment payment)
        {
            try
            {
               _payment.InsertOneAsync(payment);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return payment;
        }

        public void Update(Payment paymentIn)
        {
            var docId = paymentIn.Id;
            var postedDate = paymentIn.PostedDate;
            _payment.ReplaceOne(payment => payment.Id == docId && payment.PostedDate==postedDate, paymentIn);
        }

        public void Remove(Payment paymentIn)
        {
            var postedDate = paymentIn.PostedDate;
            _payment.DeleteOne(payment => payment.Id == paymentIn.Id && payment.PostedDate==postedDate);
        }

        public void Remove(string id)
        {
            _payment.DeleteOne(payment => payment.Id == id);
        }

        public List<Payment> GetPaymentByCompany(string companyId, int page, int totalRecord)
        {            
			return _payment.Find(payment => payment.CompanyId == companyId).SortByDescending(payment => payment.ModifiedDate).Limit(totalRecord).Skip((page - 1) * totalRecord).ToList();
		}
		/// <summary>
		/// Hàm trả về tổng số phiếu chi của một công ty
		/// </summary>
		/// <param name="companyId"></param>
		/// <returns>tổng số phiếu chi</returns>
		/// Created by: nnanh - 19/1/2019
		public long TotalReceiptOfCompany(string companyId)
		{
			// edit kieu du lieu tra ve va ham return
			return _payment.CountDocuments(receipt => receipt.CompanyId == companyId);
		}
	}
}
