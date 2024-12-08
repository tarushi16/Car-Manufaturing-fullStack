using System;
using System.ComponentModel.DataAnnotations;


namespace Car_Manufacturing.Models
{
    public class Finance
    {
        [Key]
        public int? FinanceId { get; set; }

        [Required(ErrorMessage = "Transaction type is required.")]
        [MaxLength(50, ErrorMessage = "Transaction type can't exceed 50 characters.")]
        public string TransactionType { get; set; } // 'Expense', 'Income', etc.

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be non-negative.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [MaxLength(250, ErrorMessage = "Details can't exceed 250 characters.")]
        public string Details { get; set; }
    }
}

