using System.Globalization;
using HotelMotorApi.Modules.WordGenerator.Interfaces;
using HotelMotorShared.DTOs.ServiceDTOs;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace HotelMotorApi.Modules.WordGenerator.Services
{
    public class WordService : IWordService
    {
        public async Task<byte[]> GenerateFileWithServices(List<ServiceDto> services)
        {
            using var ms = new MemoryStream();
            using var doc = DocX.Create(ms);

            doc.InsertParagraph("Listado de Servicios")
                .FontSize(18)
                .Bold()
                .SpacingAfter(20);

            var table = doc.AddTable(services.Count + 1, 3);
            table.Design = TableDesign.ColorfulGridAccent1;

            table.Rows[0].Cells[0].Paragraphs[0].Append("Nombre").Bold();
            table.Rows[0].Cells[1].Paragraphs[0].Append("Descripción").Bold();
            table.Rows[0].Cells[2].Paragraphs[0].Append("Precio").Bold();

            for (int i = 0; i < services.Count; i++)
            {
                var s = services[i];
                table.Rows[i + 1].Cells[0].Paragraphs[0].Append(s.Name);
                table.Rows[i + 1].Cells[1].Paragraphs[0].Append(s.Description);
                table.Rows[i + 1].Cells[2].Paragraphs[0].Append(s.Price.ToString("C", CultureInfo.CurrentCulture));
            }

            doc.InsertTable(table);
            doc.Save();

            return await Task.FromResult(ms.ToArray());
        }
    }
}
