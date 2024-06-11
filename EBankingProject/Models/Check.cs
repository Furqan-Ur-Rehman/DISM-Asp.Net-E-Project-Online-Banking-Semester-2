using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBankingProject.Models
{
    public class Check
    {
        public Transaction Transaction { get; set; }
        public Account Account { get; set; }    
    }
}