using CN.Common.Contracts.IServices;
using CN.Common.Models.TempModels;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.CRM.Helpers
{
    public class ExcelDocGenerator : IDocGenerator
    {
        ClientBill clientBill;
        public object Generate(ClientBill clientBill)
        {
            this.clientBill = clientBill;
            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Worksheet1");
            var headerRow = new List<string[]>() { new string[] { "client name:", clientBill.ClientName, "Date:", clientBill.yearAndMonth.Month + "/" + clientBill.yearAndMonth.Month, "Total price:", clientBill.TotalBill.ToString() } };
            // Determine the header range (e.g. A1:D1)
            string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
            // Target a worksheet
            var worksheet = excel.Workbook.Worksheets["Worksheet1"];
            // Popular header row data
            worksheet.Cells[headerRange].LoadFromArrays(headerRow);
            for (int i = 0; i < clientBill.Recepits.Count; i++)
            {
                List<string[]> lineChunk = createLineChunk(clientBill.Recepits[i]);
                worksheet.Cells[3, 1 + (4 * i)].LoadFromArrays(lineChunk);
            }
            return excel;
        }

        private List<string[]> createLineChunk(Receipt receipt)
        {
            List<string[]> chunk = new List<string[]>() {
                new string[] { "Line number:", receipt.LineNumber },
                new string[] { "Package"},
                new string[] { "Minute", "Minute left", "Packeage usage" },
                new string[] { receipt.UsedMinutes.ToString(), receipt.MinutesLeftInPackage.ToString(), receipt.MinutesUsagePrecentage.ToString()+"%",},
                new string[] { "SMS", "SMS left in package", "Packge usage"},
                new string[] { receipt.UsedSMS.ToString(), receipt.SMSLeftInPackage.ToString(), receipt.SMSUsagePrecentage.ToString()},
                new string[] { " ", " ", " ", " ", " " },
                new string[] { "Out of Package" },
                new string[] { "Minute beyond package", "Price per minute", "Total"},
                new string[] { receipt.MinutesBeyondPackage.ToString(), receipt.PricePerMinute.ToString(),receipt.TotalPaidMinutes.ToString()},
                new string[] { "SMS beyond package", "Price per SMS", "Total"},
                new string[] { receipt.SMSBeyondPackage.ToString(), receipt.PricePerSMS.ToString(), receipt.TotalPaidSMS.ToString()},
                new string[] { " ", " ", " ", " ", " " },
                new string[] { "Total payment"},
                new string[] { receipt.TotalPayment.ToString()},
                new string[] { " ", " ", " ", " ", " " }


            };
            return chunk;
        }


    }
}
