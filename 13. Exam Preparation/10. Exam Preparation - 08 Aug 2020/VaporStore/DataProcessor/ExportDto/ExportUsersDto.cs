
using System.Xml.Serialization;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor.ExportDto
{
    [XmlType(nameof(User))]
    public class ExportUsersDto
    {
        [XmlAttribute("username")]
        public string Username { get; set; }

        [XmlArray("Purchases")]
        [XmlArrayItem("Purchase")]

        public ExportPurchasesDto[] Purchases { get; set; }

        [XmlElement(nameof(TotalSpent))]
        public decimal TotalSpent { get; set; }
    }
}
