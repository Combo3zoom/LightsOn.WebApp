using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LightsOn.WebApp.Views.Pages;

public class PdfService
{
    private readonly HttpClient _httpClient;

    public PdfService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<byte[]> GeneratePdfAsync(HeaderInfo headerInfo, List<Product> products)
    {
        using (PdfDocument document = new PdfDocument())
        {
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            
            byte[] fontData = await _httpClient.GetByteArrayAsync("font/Arial.ttf");
            using (MemoryStream fontStream = new MemoryStream(fontData))
            {
                PdfFont cyrillicFont = new PdfTrueTypeFont(fontStream, 12);
                PdfFont boldFont = new PdfTrueTypeFont(fontStream, 12, PdfFontStyle.Bold);

                float y = 0;
                
                graphics.DrawString($"Рахунок на оплату № 1 від {headerInfo.Date}.", boldFont, PdfBrushes.Black, new PointF(0, y));
                y += 20;
                
                graphics.DrawString("Постачальник:", boldFont, PdfBrushes.Black, new PointF(0, y));
                graphics.DrawString($"Товариство з обмеженою відповідальністю науково-виробниче підприємство {headerInfo.FullName}", cyrillicFont, PdfBrushes.Black, new PointF(80, y));
                y += 20;
                graphics.DrawString($"Р/р {headerInfo.AccountNumber}, {headerInfo.BankName}, м. Львів, МФО {headerInfo.MFO}", cyrillicFont, PdfBrushes.Black, new PointF(80, y));
                y += 20;
                graphics.DrawString($"Україна, 08432, Львівська обл., Львівський р-н, село Зимна вода, вул.Чорних Запорожців, будинок 8, тел.: (044) 234-74-65, доб. 181,", cyrillicFont, PdfBrushes.Black, new PointF(80, y));
                y += 20;
                graphics.DrawString($"код за ЄДРПОУ {headerInfo.EDRPOU}, ІПН {headerInfo.IPN}, не є платником ПДВ, № свід. 100322677,", cyrillicFont, PdfBrushes.Black, new PointF(80, y));
                y += 20;
                graphics.DrawString($"Є платником податку на прибуток на загальних підставах", cyrillicFont, PdfBrushes.Black, new PointF(80, y));
                y += 30;
                
                graphics.DrawString("Покупець:", boldFont, PdfBrushes.Black, new PointF(0, y));
                graphics.DrawString("Товариство з обмеженою відповідальністю \"СофтСерв Діджітал\"", cyrillicFont, PdfBrushes.Black, new PointF(80, y));
                y += 20;
                graphics.DrawString("Тел.: 0973112497", cyrillicFont, PdfBrushes.Black, new PointF(80, y));
                y += 30;
                
                graphics.DrawString("Договір:", boldFont, PdfBrushes.Black, new PointF(0, y));
                graphics.DrawString("№ 086-ТО від 18.11.2023", cyrillicFont, PdfBrushes.Black, new PointF(80, y));
                y += 30;
                
                PdfGrid pdfGrid = new PdfGrid();
                pdfGrid.DataSource = products;
                
                pdfGrid.Style.Font = cyrillicFont;
                
                pdfGrid.Columns[0].Width = 30;
                pdfGrid.Columns[1].Width = 200; 
                pdfGrid.Columns[2].Width = 70;
                pdfGrid.Columns[3].Width = 70;
                pdfGrid.Columns[4].Width = 70;
                pdfGrid.Columns[5].Width = 70;
                
                PdfGridRow pdfGridHeader = pdfGrid.Headers[0];
                pdfGridHeader.Cells[0].Value = "№";
                pdfGridHeader.Cells[1].Value = "Товар";
                pdfGridHeader.Cells[2].Value = "Кількість";
                pdfGridHeader.Cells[3].Value = "Ціна без ПДВ";
                pdfGridHeader.Cells[4].Value = "Сума без ПДВ";

                // Draw the table
                PdfGridLayoutResult result = pdfGrid.Draw(page, new PointF(0, y));

                // Calculate totals
                decimal totalWithoutVAT = 0;
                foreach (var product in products)
                {
                    totalWithoutVAT += product.Total;
                }
                decimal totalVAT = totalWithoutVAT * 0.20m;
                decimal totalWithVAT = totalWithoutVAT + totalVAT;

                y = result.Bounds.Bottom + 20;

                // Draw totals
                graphics.DrawString($"Разом: {totalWithoutVAT:C2}", boldFont, PdfBrushes.Black, new PointF(page.GetClientSize().Width - 200, y));
                y += 20;
                graphics.DrawString($"Сума ПДВ: {totalVAT:C2}", boldFont, PdfBrushes.Black, new PointF(page.GetClientSize().Width - 200, y));
                y += 20;
                graphics.DrawString($"Усього з ПДВ: {totalWithVAT:C2}", boldFont, PdfBrushes.Black, new PointF(page.GetClientSize().Width - 200, y));

                y += 40;

                // Draw additional text below the table
                graphics.DrawString($"Всього найменувань {products.Count}, на суму {totalWithVAT:C2} грн", cyrillicFont, PdfBrushes.Black, new PointF(0, y));
                y += 20;

                using (MemoryStream stream = new MemoryStream())
                {
                    document.Save(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
