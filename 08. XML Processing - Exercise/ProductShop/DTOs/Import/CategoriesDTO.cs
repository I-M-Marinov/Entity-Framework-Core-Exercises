using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductShop.DTOs.Import
{
    using System.Xml.Serialization;

    [XmlType("Category")]
    public class CategoriesDTO
    {
        [XmlElement("name")]
        public string Name { get; set; } = null!;

    }
}
