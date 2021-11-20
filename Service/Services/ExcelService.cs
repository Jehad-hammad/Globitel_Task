using Repository.UnitOfWork;
using Service.Interfaces;
using System;
using Microsoft.AspNetCore.Hosting;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Domain.Models;
using Domain.DTO;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.IO;
using OfficeOpenXml;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Service.Services
{
    public class ExcelService : IExcelService
    {
        private IWebHostEnvironment hostingEnvironment;
        private UserManager<ApplicationUser> _userManager;
        private IMapper _mapper;
        public ExcelService(UserManager<ApplicationUser> userManager, IWebHostEnvironment _hostingEnvironment , IMapper mapper)
        {
            hostingEnvironment = _hostingEnvironment;
            _userManager = userManager;
            _mapper = mapper;
        }
       

        public async Task<string> GenerateActiveEmployeesReport()
        {
            #region Get Active Users
            List<ApplicationUser> users = await _userManager.Users.Where(x => x.UserRoles.Any(y => y.RoleId == (int)RoleTypes.Client) &&
                                                                              x.UserStatus == (int)UserStatus.Active)
                                                                  .OrderByDescending(x => x.Id)
                                                                  .ToListAsync();
            List<UserResponseDTO> listUser = _mapper.Map<List<UserResponseDTO>>(users);
            #endregion

            #region Excel Path
            string FilePath = Path.Combine(hostingEnvironment.WebRootPath + "\\Reports\\");

            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            string fileName = "Globitel Active Employees Report" + ".xlsx";
            string fullPath = Path.Combine(FilePath, fileName);
            #endregion

            #region create excel

            using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Active Employees " + DateTime.Now.Year);
                #region style
                ICellStyle CellStyle = workbook.CreateCellStyle();

                CellStyle.BorderLeft = BorderStyle.Thin;
                CellStyle.Alignment = HorizontalAlignment.Center;
                CellStyle.VerticalAlignment = VerticalAlignment.Center;
                CellStyle.FillPattern = FillPattern.SolidForeground;

                ICellStyle headerStyle = workbook.CreateCellStyle();
                headerStyle.CloneStyleFrom(CellStyle);
                ((XSSFCellStyle)headerStyle).SetFillForegroundColor(new XSSFColor(new byte[] { 254, 226, 184 }));
                #endregion

                IRow row = sheet.CreateRow(0);
                row.CreateCell(0).SetCellValue("Full Name");
                row.CreateCell(1).SetCellValue("EmailAddress");
                row.CreateCell(2).SetCellValue("Phone Number");
                row.CreateCell(3).SetCellValue("Age");
                row.CreateCell(4).SetCellValue("Gender");
                for(int i = 0; i <= 4; i++)
                {
                    row.Cells[i].CellStyle = headerStyle;
                    sheet.SetColumnWidth(i, 6000);

                }
                int rowCount = 1;

                foreach(UserResponseDTO user in listUser)
                {
                    IRow dataRow = sheet.CreateRow(rowCount);
                    dataRow.CreateCell(0).SetCellValue(user.FullName);
                    dataRow.CreateCell(1).SetCellValue(user.EmailAddress);
                    dataRow.CreateCell(2).SetCellValue(user.PhoneNumber);
                    dataRow.CreateCell(3).SetCellValue(user.Age.ToString());
                    dataRow.CreateCell(4).SetCellValue(user.Gender == 1 ? "Male" : "Female");
                    rowCount++;
                }
                workbook.Write(fs);
            }

            #endregion

            return fullPath;
        }
    }
}
