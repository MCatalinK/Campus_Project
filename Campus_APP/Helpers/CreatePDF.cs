using Campus_APP.ViewModels;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campus_APP.Helpers
{
    static public class CreatePDF
    {
        public static void GeneratePDF(StudentVM student,List<string> months,decimal total)
        {
            string noInvoice = ($"{student.Id}{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}");
            PdfWriter writer = new PdfWriter($"../../TaxTickets/{noInvoice}.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            
            Paragraph nrInvoice = new Paragraph($"Number:#{noInvoice}")
               .SetTextAlignment(TextAlignment.LEFT)
               .SetFontSize(28);
            document.Add(nrInvoice);

            Paragraph date = new Paragraph($"Date:{DateTime.Now.ToShortDateString()}")
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetFontSize(28);
            document.Add(date);

            Paragraph university = new Paragraph($"University: {student.University.Name} \nCampus: {student.Campus.Id}")
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetFontSize(28);
            document.Add(university);

            Paragraph room = new Paragraph($"Room: {student.CampusRoom.NoRoom}")
                  .SetTextAlignment(TextAlignment.LEFT)
                  .SetFontSize(28);
            document.Add(room);

            StringBuilder monthsPayed= new StringBuilder();
            foreach(string month in months)
            {
                monthsPayed.Append(month+",");
            }
            monthsPayed.Remove(monthsPayed.Length - 1, 1);

            Table table = new Table(2, true);
            Cell cell11 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("First Name:"))
                .SetFontSize(22);
            Cell cell12 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph($"{student.FirstName}"))
               .SetFontSize(22);

            Cell cell21 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Last Name"))
               .SetFontSize(22);
            Cell cell22 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph($"{student.LastName}"))
               .SetFontSize(22);

            Cell cell31 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Months"))
               .SetFontSize(22);
            Cell cell32 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph($"{monthsPayed}"))
               .SetFontSize(22);

            Cell cell41 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Total"))
               .SetFontSize(22);
            Cell cell42 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph($"{total}"))
               .SetFontSize(22);

            table.AddCell(cell11);
            table.AddCell(cell12);
            table.AddCell(cell21);
            table.AddCell(cell22);
            table.AddCell(cell31);
            table.AddCell(cell32);
            table.AddCell(cell41);
            table.AddCell(cell42);
            document.Add(table);


            document.Close();
        }
    }
}
