﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.DTOs
{
    public class CarInputDto
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public long TraveledDistance { get; set; }
        public List<int> PartsId { get; set; }
    }

}
