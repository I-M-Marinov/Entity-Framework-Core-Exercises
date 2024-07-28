
using System.Xml.Serialization;
using Boardgames.Data.Models;

namespace Boardgames.DataProcessor.ExportDto
{
    [XmlType(nameof(Creator))]
    public class ExportCreatorDto
    {

        [XmlAttribute(nameof(BoardgamesCount))]
        public int BoardgamesCount { get; set; }

        [XmlElement(nameof(CreatorName))]
        public string CreatorName { get; set; } = null!;

        [XmlArray(nameof(Boardgames))]
        public ExportBoardgameToXmlDto[] Boardgames { get; set; } = null!;

    }
}
