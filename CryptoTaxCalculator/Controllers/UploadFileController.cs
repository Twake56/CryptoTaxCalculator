using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CryptoTaxCalculator.Parser;

namespace CryptoTaxCalculator.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class UploadFileController : ControllerBase
    {
        [HttpPost("[action]"), DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                string filePath = Path.Combine("./Uploads/", Path.GetFileName(file.FileName));
                if(!Directory.Exists("./Uploads"))
                {
                    Directory.CreateDirectory("./Uploads");
                }
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                CSVParser parser = new CSVParser(filePath);
                List<Transaction> transactions = parser.Transactions;
                Calculator calculator = new Calculator(Calculator.CalculateType.FIFO);
                calculator.Calculate(transactions);
                return StatusCode(200);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}