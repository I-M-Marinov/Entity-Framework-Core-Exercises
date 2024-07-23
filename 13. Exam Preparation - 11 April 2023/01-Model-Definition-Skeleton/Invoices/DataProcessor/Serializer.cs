using Invoices.DataProcessor.ExportDto;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Newtonsoft.Json;

namespace Invoices.DataProcessor
{
    using Invoices.Data;
    using Newtonsoft.Json.Serialization;

    public class Serializer
    {
        public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
        {
            throw new NotImplementedException();
        }

        public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
        {

            ExportProductDto[] productsToExport = context.Products
                .Where(p => p.ProductsClients.Any())
                .Where(p => p.ProductsClients.Any(pc => pc.Client.Name.Length >= nameLength))
                .Select(p => new ExportProductDto()
                {
                    Name = p.Name,
                    Price = p.Price,
                    Category = p.CategoryType.ToString(),
                    Clients = p.ProductsClients
                        .Where(pc => pc.Client.Name.Length >= nameLength)
                        .Select(pc => new ExportProductClientsDto()
                        {
                            Name = pc.Client.Name,
                            NumberVat = pc.Client.NumberVat
                        })
                        .OrderBy(c => c.Name)
                        .ToArray()
                })
                .OrderByDescending(p => p.Clients.Length)
                .ThenBy(p => p.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(productsToExport, Formatting.Indented);
        }
    }
}