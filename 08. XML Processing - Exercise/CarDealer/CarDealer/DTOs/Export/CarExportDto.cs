﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.DTOs.Export
{
    using System.Xml.Serialization;

    [XmlType("car")]
    public class CarExportDto
    {
        [XmlElement("make")] public string Make { get; set; }

        [XmlElement("model")] public string Model { get; set; }

        [XmlElement("traveled-distance")] public long TraveledDistance { get; set; }
    }
}
