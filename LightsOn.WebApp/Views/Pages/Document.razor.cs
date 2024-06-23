using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Buttons;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace LightsOn.WebApp.Views.Pages
{
    public class HeaderInfo
    {
        public string FullName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string IPN { get; set; }
        public string MFO { get; set; }
        public string EDRPOU { get; set; }
        public DateTime Date { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Quantity * Price;
    }

    public partial class Document : ComponentBase
    {
        private HeaderInfo headerInfo = new HeaderInfo
        {
            FullName = "ТЗОВ LGenera",
            AccountNumber = "UA963354354500354023543345",
            BankName = "АТ «Кре́добанк»",
            IPN = "452324925432",
            MFO = "334505",
            EDRPOU = "1324534",
            Date = DateTime.Now
        };

        [Inject]
        private PdfService PdfService { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        private async Task GeneratePdf()
        {
            byte[] pdfBytes = await PdfService.GeneratePdfAsync(headerInfo, selectedProducts);
            var fileName = "Document.pdf";

            using (var stream = new MemoryStream(pdfBytes))
            {
                var base64 = Convert.ToBase64String(stream.ToArray());
                await JSRuntime.InvokeVoidAsync("downloadFile", fileName, base64);
            }
        }
    }
}
