
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Theatre.Data.Models;
using Theatre.Data.Models.Enums;

namespace Theatre.DataProcessor.ExportDto
{
    [XmlType(nameof(Play))]

    public class ExportPlaysDto
    {

        [XmlAttribute(nameof(Title))]
        public string Title { get; set; } = null!;

        [XmlAttribute(nameof(Duration))]
        public string Duration { get; set; }

        [XmlAttribute(nameof(Rating))]

        public float Rating { get; set; }

        [XmlAttribute(nameof(Genre))]
        public string Genre { get; set; }

        [XmlArray(nameof(Actors))]
        [XmlArrayItem("Actor")]

        public ExportCountryDto[] Actors { get; set; } = new ExportCountryDto[0];


    }
}
