using System.ComponentModel;
using HotelMotorApi.Interfaces;
using HotelMotorShared.Dtos.OrderDTOs;
using QuestPDF.Fluent;

namespace HotelMotorApi.Services
{
    public class PdfService : IPdfService
    {
        public byte[] GeneratePdf(List<OrderDTO> orders)
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("Listado de Órdenes").FontSize(20).Bold().AlignCenter();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("ID").Bold();
                            header.Cell().Text("Creación").Bold();
                            header.Cell().Text("Vencimiento").Bold();
                            header.Cell().Text("Descripción").Bold();
                            header.Cell().Text("Estado").Bold();
                            header.Cell().Text("Total").Bold();
                        });

                        foreach (var order in orders)
                        {
                            table.Cell().Text(order.Id.ToString());
                            table.Cell().Text(order.CreatedAt.ToShortDateString());
                            table.Cell().Text(order.DueDate.ToShortDateString());
                            table.Cell().Text(order.Summary);
                            table.Cell().Text(order.Status);
                            table.Cell().Text($"${order.TotalAmount:F2}");
                        }
                    });

                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Generado el ").SemiBold();
                        txt.Span(DateTime.Now.ToString("g"));
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}
