using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SBTransactions.Models
{
    public class SBTransact
    {

        [Key]
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public long AccountNumber { get; set; }
        public float Amount { get; set; }
        public string TransactionType { get; set; }
    }
}

