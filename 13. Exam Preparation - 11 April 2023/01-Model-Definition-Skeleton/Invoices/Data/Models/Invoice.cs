﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Invoices.Data.Models.Enums;
using static Invoices.Data.Models.DataConstraints;

namespace Invoices.Data.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(InvoiceNumberMaxValue)]
        public int Number { get; set; }

        [Required]
        public DateTime IssueDate  { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public decimal Amount { get; set; }
        [Required]
        public CurrencyType CurrencyType { get; set; }

        [Required]
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        [Required]
        public virtual Client Client { get; set; } = null!;

    }
}
