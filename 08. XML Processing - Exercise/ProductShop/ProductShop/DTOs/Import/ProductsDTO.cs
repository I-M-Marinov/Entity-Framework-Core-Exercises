﻿using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Import
{
    using System.Xml.Serialization;

    [XmlType("Product")]
    public class ProductsDTO
    {
        [XmlElement("name")]

        public string Name { get; set; } = null!;

        [XmlElement("price")]

        public decimal Price { get; set; }

        [XmlElement("sellerId")]

        public int SellerId { get; set; }

        [XmlElement("buyerId")]
        public int? BuyerId { get; set; }
    }
}