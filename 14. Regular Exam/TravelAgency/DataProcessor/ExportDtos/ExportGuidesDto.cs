
using System.Xml.Serialization;
using TravelAgency.Data.Models;

namespace TravelAgency.DataProcessor.ExportDtos
{
    [XmlType(nameof(Guide))]
    public class ExportGuidesDto
    {
        [XmlElement(nameof(FullName))]
        public string FullName { get; set; } = null!;

        [XmlArray(nameof(TourPackages))]
        [XmlArrayItem(nameof(TourPackage))]
        public ExportTourPackageDto[] TourPackages { get; set; }

    }
}
