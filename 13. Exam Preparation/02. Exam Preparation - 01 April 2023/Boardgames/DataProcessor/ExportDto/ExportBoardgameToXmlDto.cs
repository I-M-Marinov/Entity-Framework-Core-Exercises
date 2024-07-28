
using System.Xml.Serialization;
using Boardgames.Data.Models;

namespace Boardgames.DataProcessor.ExportDto
{
    [XmlType(nameof(Boardgame))]
    public class ExportBoardgameToXmlDto
    {
        [XmlElement(nameof(BoardgameName))]
        public string BoardgameName { get; set; }

        [XmlElement(nameof(BoardgameYearPublished))]
        public int BoardgameYearPublished { get; set; }
    }
}
