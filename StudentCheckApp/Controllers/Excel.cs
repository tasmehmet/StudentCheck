using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using StudentCheckApp.Data;
using StudentCheckApp.Models.DbModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCheckApp.Controllers
{
    public class Excel : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;

        public Excel(IHostingEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        public async Task<IActionResult> Export()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = string.Format("Yoklama-{0}.xlsx", DateTime.Now.ToString("dd.MM.yyyy"));
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            var students = _context.Students.ToList();

            List<CheckDay> checkDays = new List<CheckDay>();
            foreach (var item in students)
            {
                var dbCheckDays = _context.CheckDay.Where(x => x.Students.ID == item.ID).ToList();
                foreach (var item2 in dbCheckDays)
                {
                    item2.Students = item;
                    checkDays.Add(item2);
                }
            }
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {

                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Yoklama");
                IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("Adı");
                row.CreateCell(1).SetCellValue("Soyadı");
                row.CreateCell(2).SetCellValue("Tarih");
                row.CreateCell(3).SetCellValue("Geldi/Gelmedi");

                for (int i = 0; i < checkDays.Count ; i++)
                {
                    var list = checkDays[i];
                    row = excelSheet.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(list.Students.Name);
                    row.CreateCell(1).SetCellValue(list.Students.Surname);
                    row.CreateCell(2).SetCellValue(list.Date.ToString("dd.MM.yyyy"));
                    row.CreateCell(3).SetCellValue(list.Status ? "Geldi" : "Gelmedi");
                }

                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }
        public async Task<IActionResult> ExportHomework()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = string.Format("Ödev-{0}.xlsx", DateTime.Now.ToString("dd.MM.yyyy"));
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            var students = _context.Students.ToList();

            List<Homeworks> homeworks = new List<Homeworks>();
            foreach (var item in students)
            {
                var dbHomeworks =  _context.Homeworks.Where(x => x.Students.ID == item.ID).ToList();
                foreach (var item2 in dbHomeworks)
                {
                    item2.Students = item;
                    homeworks.Add(item2);
                }


            }
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {

                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("ödev");
                IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("Adı");
                row.CreateCell(1).SetCellValue("Soyadı");
                row.CreateCell(2).SetCellValue("Ödev İsmi");
                row.CreateCell(3).SetCellValue("Not");
                row.CreateCell(4).SetCellValue("Tarih");

                for (int i = 0; i < homeworks.Count; i++)
                {
                    var list = homeworks[i];
                    row = excelSheet.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(list.Students.Name);
                    row.CreateCell(1).SetCellValue(list.Students.Surname);
                    row.CreateCell(2).SetCellValue(list.Name);
                    row.CreateCell(3).SetCellValue(list.Note);
                    row.CreateCell(4).SetCellValue(list.CreateTime.Date.ToString("dd.MM.yyyy"));
                }

                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }
    }
}
