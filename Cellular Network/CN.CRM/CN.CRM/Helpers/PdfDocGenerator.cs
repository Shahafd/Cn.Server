using CN.Common.Configs;
using CN.Common.Contracts.IServices;
using CN.Common.Models.TempModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.CRM.Helpers
{
    public class PdfDocGenerator : IDocGenerator
    {
        ClientBill clientBill;
        public object Generate(ClientBill clientBill)
        {
            this.clientBill = clientBill;
            Document doc = new Document(PageSize.A4);
            string fileName = clientBill.ClientName + clientBill.yearAndMonth.Month + "-" + clientBill.yearAndMonth.Year + "-" + clientBill.Recepits.Count + ".pdf";
            string folder = (clientBill.ClientName);
            var output = new FileStream(MainConfigs.BaseSaveAdrressForPdf + folder + @"\" + fileName, FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, output);
            doc.Open();
            PdfPTable table1 = createBillConstTable(clientBill);
            doc.Add(table1);
            for (int i = 0; i < clientBill.Recepits.Count; i++)
            {
                PdfPTable table2 = createLineTable(clientBill.Recepits[i]);
                doc.Add(table2);
            }
            doc.Close();
            return doc;
        }

        private PdfPTable createBillConstTable(ClientBill clientBill)
        {
            /////////////const data/////////////////////
            PdfPTable table = new PdfPTable(2);
            //table1.DefaultCell.Border = 0;
            table.WidthPercentage = 100;

            PdfPCell cell11 = new PdfPCell();
            cell11.Colspan = 1;
            cell11.AddElement(new Paragraph("Client Name: " + clientBill.ClientName, new Font()));
            cell11.AddElement(new Paragraph("Date: " + clientBill.yearAndMonth.Month + "/" + clientBill.yearAndMonth.Year, new Font()));
            cell11.VerticalAlignment = Element.ALIGN_LEFT;

            PdfPCell cell12 = new PdfPCell();
            cell12.AddElement(new Paragraph("Total Price: " + clientBill.TotalBill, new Font()));
            cell12.VerticalAlignment = Element.ALIGN_CENTER;

            table.AddCell(cell11);
            table.AddCell(cell12);
            return table;
        }

        private PdfPTable createLineTable(Receipt receipt)
        {
            ///////////////////dynamic data//////////////////////////
            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;

            PdfPCell cell = new PdfPCell(new Phrase("Line Number:" + receipt.LineNumber));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            table.AddCell("price: " + receipt.PackagePrice + "$");
            cell = new PdfPCell(new Phrase("Package info:"));
            cell.Colspan = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Package"));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell);

            table.AddCell("Minute: " + receipt.UsedMinutes);  // "Row 4, Col 1"
            table.AddCell("Minute left: " + receipt.MinutesLeftInPackage);  // "Row 4, Col 2"
            table.AddCell("Package used: " + receipt.MinutesUsagePrecentage + "%");  // "Row 4, Col 3"

            table.AddCell("SMS: " + receipt.UsedSMS);  // "Row 5, Col 1"
            table.AddCell("SMS left: " + receipt.SMSLeftInPackage);  // "Row 5, Col 2"
            table.AddCell("Package used: " + receipt.SMSUsagePrecentage + "%");  // "Row 5, Col 3"

            cell = new PdfPCell(new Phrase("Package price: " + receipt.PackagePrice + "$"));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Out of Package"));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell);

            table.AddCell("Minute beyond package: " + receipt.MinutesBeyondPackage);  // "Row 8, Col 1"
            table.AddCell("price per minunte: " + receipt.PricePerMinute + "$");  // "Row 8, Col 2"
            table.AddCell("Total: " + receipt.TotalPaidMinutes);  // "Row 8, Col 3"

            table.AddCell("SMS beyond package: " + receipt.SMSBeyondPackage);  // "Row 9, Col 1"
            table.AddCell("price per SMS: " + receipt.PricePerSMS + "$");  // "Row 9, Col 2"
            table.AddCell("Total: " + receipt.TotalPaidSMS);  // "Row 9, Col 3"

            cell = new PdfPCell(new Phrase("Total:" + receipt.TotalPayment));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("  "));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }
    }
}
