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
using  MISA.SME.ReceiptAndPayment.Models;
using  MISA.SME.ReceiptAndPayment.Services;
using MongoDB.Driver;

namespace  MISA.SME.ReceiptAndPayment.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ReceiptController : ControllerBase
    {
        private readonly ReceiptService _receiptService;
        //private readonly IConfiguration _configuration;
        protected IMongoCollection<Receipt> _receiptCollection;
        protected IMongoCollection<Company> _companyCollection;

        public ReceiptController(ReceiptService receiptService)
        {
            _receiptService = receiptService;
            _receiptCollection = _receiptService._receipt;
        }		
		/// <summary>
		/// Hàm trả về tất phiếu thu của công ty theo phân trang
		/// </summary>
		/// <param name="page">trang muốn hiển thị dữ liệu</param>
		/// <param name="totalRecord">Tổng số bản ghi trong trang đó</param>
		/// <returns>danh sách bản ghi tương ứng</returns>
        // GET api/values
        [HttpGet]
        public ActionResult<List<Receipt>> GetReceiptByCompany([FromHeader] string page, [FromHeader] string totalRecord) // edit new
        {
            string header = Request.Headers["Authorization"];
            string token = header.Split(' ')[1];
            var jwtToken = new JwtSecurityToken(token);
            // Lấy userName trong token
            string companyID = jwtToken.Claims.FirstOrDefault(x => x.Type == "company_id").Value;
			//var totalReceiptOfCompany = _receiptService.TotalReceiptOfCompany(companyID);
			return _receiptService.GetReceiptByCompany(companyID, Int32.Parse(page), Int32.Parse(totalRecord));
			//return Ok(new { Code = 200, Message = "Cập nhật thành công!", Success = true, Data = receipts ,TotalReceipt = totalReceiptOfCompany });
		}
		/// <summary>
		/// Hàm trả về tổng số phiếu thu của 1 công ty
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
			var totalReceiptOfCompany = _receiptService.TotalReceiptOfCompany(companyID);
			return Ok(new { Code = 200, Message = "Lấy tổng số phiếu thu thành công", Success = true, TotalReceipt = totalReceiptOfCompany });
		}

		//GET api/values/5
		[HttpGet("{id}", Name ="GetR")]
        public ActionResult<Receipt> GetId(string id)
        {
            var receipt = _receiptService.Get(id);

            if (receipt == null)
            {
                return NotFound();
            }

            return receipt;
        }

        //POST api/values
        [HttpPost]
        public ActionResult<Receipt> Create(Receipt receipt)
        {
            _receiptService.Create(receipt);
            try
            {
                return CreatedAtRoute("GetR", new { id = receipt.Id.ToString() }, receipt);
            }catch(Exception ex)
            {
                return BadRequest(new { Code = 401, Message = "Có lỗi!", Status = false });
            }
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult UpdateReceipt(Receipt receiptIn)
        {
            var receipt = _receiptService.Get(receiptIn.Id);

            if (receipt == null)
            {
                return NotFound(new { Code = 404, Message = "Object Id không tồn tại!", Success = false });
            }

            _receiptService.Update(receiptIn);

			return Ok(new { Code = 200, Message = "Cập nhật thành công!", Success = true, Data = receiptIn });
        }

        //Xóa Receipt theo object id
        // DELETE api/values/5
        [HttpDelete]
        public IActionResult Delete([FromHeader] string id)
        {
            var receipt = _receiptService.Get(id);
            if (receipt == null)
            {
                return NotFound(new { Code = 404, Message = "Object Id không tồn tại!", Success = false });
            }
            _receiptService.Remove(receipt);
            return Ok(new { Code = 200, Message = "Xóa thành công!", Success = true });
        }

		//public IActionResult GetTotalReceiptOfCompany(string companyId)
		//{
		//	return _receiptService.TotalReceiptOfCompany(companyId);
		//}		
    }	
}
