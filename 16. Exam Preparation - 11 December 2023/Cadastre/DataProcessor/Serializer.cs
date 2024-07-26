using System.Globalization;
using Cadastre.Data;
using Cadastre.DataProcessor.ExportDtos;
using Newtonsoft.Json;

namespace Cadastre.DataProcessor
{
    public class Serializer
    {
        public static string ExportPropertiesWithOwners(CadastreContext dbContext)
        {
            var isValidDate = DateTime.TryParse("01/01/2000", out DateTime date);

            var propertiesToExport = dbContext.Properties
                .Where(p => p.DateOfAcquisition >= date)
                .Select(p => new
                {
                    PropertyIdentifier = p.PropertyIdentifier,
                    Area = p.Area,
                    Address = p.Address,
                    DateOfAcquisition = p.DateOfAcquisition,
                    Owners = p.PropertiesCitizens
                        .Select(ps => new
                        {
                            LastName = ps.Citizen.LastName,
                            MaritalStatus = ps.Citizen.MaritalStatus
                        })
                        .OrderBy(ps => ps.LastName)
                        .ToArray()
                })
                .AsEnumerable()
                .OrderByDescending(ep => ep.DateOfAcquisition)
                .ThenBy(ep => ep.PropertyIdentifier)
                .Select(p => new ExportPropertyDto
                {
                    PropertyIdentifier = p.PropertyIdentifier,
                    Area = p.Area,
                    Address = p.Address,
                    DateOfAcquisition = p.DateOfAcquisition.ToString("dd/MM/yyyy"),
                    Owners = p.Owners
                        .Select(o => new ExportCitizenDto
                        {
                            LastName = o.LastName,
                            MaritalStatus = o.MaritalStatus.ToString()
                        })
                        .OrderBy(o => o.LastName)
                        .ToArray()
                })
                .ToArray();


            return JsonConvert.SerializeObject(propertiesToExport, Formatting.Indented);
        }

        public static string ExportFilteredPropertiesWithDistrict(CadastreContext dbContext)
        {
            throw new NotImplementedException();
        }
    }
}
