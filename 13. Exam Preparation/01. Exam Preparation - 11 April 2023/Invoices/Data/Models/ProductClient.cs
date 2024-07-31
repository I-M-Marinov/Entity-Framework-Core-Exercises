using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Invoices.Data.Models
{
    public class ProductClient
    {
        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        [Required]
        public virtual Product Product { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        [Required]
        public virtual Client Client { get; set; } = null!;
    }
}
