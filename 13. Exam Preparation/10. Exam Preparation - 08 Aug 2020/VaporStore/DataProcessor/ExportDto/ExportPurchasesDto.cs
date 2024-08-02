using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor.ExportDto
{
    [XmlType(nameof(Purchase))]
    public class ExportPurchasesDto
    {
        [XmlElement(nameof(Card))]
        public string Card { get; set; }

        [XmlElement(nameof(Cvc))]
        public string Cvc { get; set; }

        [XmlElement(nameof(Date))]
        public string Date { get; set; }

        [XmlElement(nameof(Game))]
        public ExportGameXmlDto Game { get; set; }

    }
}
