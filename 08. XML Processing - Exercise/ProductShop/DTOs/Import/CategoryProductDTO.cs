using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductShop.DTOs.Import
{

    using System.Xml.Serialization;

    [XmlType("CategoryProduct")]
    public class CategoryProductDTO
    {
        [XmlElement("CategoryId")]
        public int CategoryId { get; set; }

        [XmlElement("ProductId")]
        public int ProductId { get; set; }
    }
}
