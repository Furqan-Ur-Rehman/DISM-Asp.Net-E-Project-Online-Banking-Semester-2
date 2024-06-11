using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBankingProject.Models
{
    public class Calculation
    {
        public int AccountNO { get; set; }

        public string firstName { get; set; }

        public DateTime AccountDate { get; set; }

        public string Description { get; set; }

        public int DocId { get; set; }

        public decimal Balance{ get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public decimal  Total{ get; set; }


    }
}