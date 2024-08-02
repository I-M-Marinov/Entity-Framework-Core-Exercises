using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions.Impl;

namespace VaporStore.DataProcessor.ExportDto
{
    public class ExportGenresDto
    {
        public int Id { get; set; }

        public string Genre { get; set; }

        public ExportGameDto[] Games { get; set; }

        public int TotalPlayers { get; set; }

    }
}
