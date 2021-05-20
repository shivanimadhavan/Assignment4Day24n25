using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SBClient.Models
{
    public partial class SB
    {
        [Key]
        public int CustomerId { get; set; }
        public int AccountNumber { get; set; }
        public String CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public float CustomerBalance { get; set; }
        public int CustomeId { get; internal set; }
    }
}
