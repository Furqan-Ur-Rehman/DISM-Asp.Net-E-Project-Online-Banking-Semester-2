﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EBankingProject.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EbankingEntities12 : DbContext
    {
        public EbankingEntities12()
            : base("name=EbankingEntities12")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<BalanceEnquiry> BalanceEnquiries { get; set; }
        public virtual DbSet<branch> branches { get; set; }
        public virtual DbSet<ChangeofAddress> ChangeofAddresses { get; set; }
        public virtual DbSet<ChequeReq> ChequeReqs { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<RequestofChequeBook> RequestofChequeBooks { get; set; }
        public virtual DbSet<StopPaymentofCheque> StopPaymentofCheques { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
    }
}
