using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Globitel.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        public ExcelController(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> GenerateActiveEmployeesReport()
        {
            string FilePath =await _serviceUnitOfWork.Excel.Value.GenerateActiveEmployeesReport();

            if (!string.IsNullOrEmpty(FilePath) && System.IO.File.Exists(FilePath))
            {
                var memory = new MemoryStream();
                using (var stream = new FileStream(FilePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, MimeTypes.GetMimeType(FilePath));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
