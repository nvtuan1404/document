using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MISA.SME.ReceiptAndPayment.Models;
using MISA.SME.ReceiptAndPayment.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MISA.SME.ReceiptAndPayment.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        //private readonly IConfiguration _configuration;
        protected IMongoCollection<Payment> _paymentCollection;
        protected IMongoCollection<Company> _companyCollection;
        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
            _paymentCollection = _paymentService._payment;
            //_configuration = configuration;
        }

        [HttpGet]
        public ActionResult<List<Payment>> GetPaymentByCompany([FromHeader] string page, [FromHeader] string totalRecord)
        {
            string header = Request.Headers["Authorization"];
            string token = header.Split(' ')[1];
            var jwtToken = new JwtSecurityToken(token);
            // Lấy userName trong token
            string companyID = jwtToken.Claims.FirstOrDefault(x => x.Type == "company_id").Value;
            return _paymentService.GetPaymentByCompany(companyID, Int32.Parse(page), Int32.Parse(totalRecord));
        }

		/// <summary>
		/// Hàm trả về tổng số phiếu chi của 1 công ty
		/// </summary>
		/// <returns></returns>
		/// Created by: nnanh - 19/1/2018
		[HttpGet]
		public ActionResult<Receipt> GetTotalReceiptRecord()
		{
			string header = Request.Headers["Authorization"];
			string token = header.Split(' ')[1];
			var jwtToken = new JwtSecurityToken(token);
			// Lấy userName trong token
			string companyID = jwtToken.Claims.FirstOrDefault(x => x.Type == "company_id").Value;
			var totalPaymentOfCompany = _paymentService.TotalReceiptOfCompany(companyID);
			return Ok(new { Code = 200, Message = "Lấy tổng số phiếu chi thành công", Success = true, TotalReceipt = totalPaymentOfCompany });
		}

		[HttpGet("{id}", Name = "GetP")]
        public ActionResult<Payment> GetId(string id)
        {
            var payment = _paymentService.Get(id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        [HttpPost]
        public ActionResult<Payment> Create(Payment payment)
        {
            _paymentService.Create(payment);
            try
            {
                return CreatedAtRoute("GetP", new { id = payment.Id.ToString() }, payment);
            }
            catch (Exception ex)
            {
                return BadRequest(new {Code = 401, Message = "Có lỗi!", Status= false});
            }          
        }

        [HttpPut]
        public IActionResult UpdatePayment(Payment paymentIn)
        {
            var payment = _paymentService.Get(paymentIn.Id);

            if (payment == null)
            {
                return NotFound(new { Code = 404, Message = "Object Id không tồn tại!", Success = false });
            }

            _paymentService.Update(paymentIn);

            return Ok(new { Code = 200, Message = "Cập nhật thành công!", Success = true });
        }

        // Xóa Payment theo object id
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {

            var payment = _paymentService.Get(id);

            if (payment == null)
            {
                return NotFound(new { Code= 404, Message="Object Id không tồn tại!", Success= false});
            }

            _paymentService.Remove(payment);

            return Ok(new { Code = 200, Message = "Xóa thành công!", Success = true });
        }
    }
}