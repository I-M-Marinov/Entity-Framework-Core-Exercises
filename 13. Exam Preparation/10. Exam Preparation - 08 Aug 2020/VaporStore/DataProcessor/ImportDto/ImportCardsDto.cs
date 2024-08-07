﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using static VaporStore.DataConstraints;

namespace VaporStore.DataProcessor.ImportDto
{
    public class ImportCardsDto
    {

        [Required]
        [RegularExpression(@"^\d{4} \d{4} \d{4} \d{4}$")]
        public string Number { get; set; } = null!;

        [Required]
        [RegularExpression(@"^\d{3}$")]
        public string CVC { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;
    }
}
