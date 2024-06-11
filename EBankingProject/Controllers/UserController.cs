using EBankingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBankingProject.Controllers
{
    public class UserController : Controller
    {
        EbankingEntities12 db = new EbankingEntities12();
        // GET: User
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult AccountInfo(int id)
        {
           
            var query = db.Accounts.Where(x => x.CustomerId == id).SingleOrDefault();
            return View(query);
        }

        public ActionResult Account()
        {
            Account acc = new Account();
            var Acc = acc.AccountNO;

            return View();
        }


        public ActionResult ChangeofAddress()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult ChangeofAddress(ChangeofAddress address)
        {
            db.ChangeofAddresses.Add(address);
            var a = db.SaveChanges();

            if (a > 0)
            {
                Session["ChangeofAddress"] = "Your request has been sent successfully!";
                ModelState.Clear();
            }
            else
            {
                Session["ChangeofAddress"] = "Your request has not been sent!";
            }

            ModelState.Clear();
            return View();
        }

        public ActionResult RequestforChequeBook()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RequestforChequeBook(RequestofChequeBook cheqbook)
        {
            db.RequestofChequeBooks.Add(cheqbook);
            var a = db.SaveChanges();

            if (a > 0)
            {
                Session["ReqforChequebook"] = "Your request has been sent successfully!";
                ModelState.Clear();
            }
            else
            {
                Session["ReqforChequebook"] = "Your request has not been sent!";
            }
            return View();
        }

        public ActionResult StopPaymentofCheque()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StopPaymentofCheque(StopPaymentofCheque stop)
        {
            db.StopPaymentofCheques.Add(stop);
            var a = db.SaveChanges();

            if(a > 0)
            {
                Session["StopPayment"] = "Your request has been sent successfully!";
                ModelState.Clear();
            }
            else
            {
                Session["StopPayment"] = "Your request has not been sent!";
            }
            
            return View();
        }
       
        [HttpGet]
            
        public ActionResult FundsTransfer(int id)
        {
            var query = db.Accounts.Where(x => x.CustomerId == id).SingleOrDefault();


            return View(query);
        }
        [HttpPost]
        public ActionResult FundsTransfer(Account Acc, int Accountnumber, DateTime transfDate,int transfAmount,int SenderAmount,int RecieverAmount)

        {

           
            Transaction funds = new Transaction();
            Acc.AccountNO = Accountnumber;
            funds.TransferDate = transfDate;
            funds.TransferAmount = transfAmount;
            funds.SenderAccountNum = SenderAmount;
            funds.RecieverAccountNum = RecieverAmount;

       
            var query = db.Accounts.Count(x => x.AccountNO == funds.SenderAccountNum);
            var query2 = db.Accounts.Count(x => x.AccountNO == funds.RecieverAccountNum);
            var query3 = db.Accounts.Count(x => x.Balance >= funds.TransferAmount);

           


            if (query == 1)
            {

                if (query3 == 3)
                {
                    if (query2 == 1)
                    {
                        
                        db.Transactions.Add(funds);
                       
                        var a = db.SaveChanges();
                        
                        if (a > 0)
                        {
                            Session["FundTransfer"] = "Your funds has been transferred successfully!";
                            ModelState.Clear();
                            RedirectToAction("FundsTransfer");
                        }
                    }
                    else
                    {
                        Session["FundTransfer"] = "Reciever Account number is incorrect";
                    }
                }
                else
                {
                    Session["FundTransfer"] = "You have Insufficient Amount for Transfer!";
                }
            }
            else
            {
                Session["FundTransfer"] = "Sender Account num is incorrect";
                ModelState.Clear();
            }
            
            if (query == 0 && query2 == 0)
            {
                Session["FundTransfer"] = "Both Account Number is incorrect";
                ModelState.Clear();
            }
            var query4 = db.Accounts.AsNoTracking().SingleOrDefault(x => x.AccountNO == funds.SenderAccountNum);

            var b = query4.Balance;
            var AccDate = query4.AccountDate;
            var AccType = query4.AccountType;
            var CustomerID = query4.CustomerId;

            var c = b - funds.TransferAmount;
            Acc.Balance = c;
            Acc.AccountDate = AccDate;
            Acc.AccountType = AccType;
            Acc.CustomerId = CustomerID;
            //Session["UpdatedBalance"] = Acc.Balance;

            //var query5 = db.Accounts.AsNoTracking().SingleOrDefault(x => x.AccountNO == funds.RecieverAccountNum);

            //var d = query5.Balance;
            //var AccountDate = query5.AccountDate;
            //var AccountType = query5.AccountType;
            //var CusID = query5.CustomerId;

            //var e = d + funds.TransferAmount;
            //Acc.Balance = e;
            //Acc.AccountDate = AccountDate;
            //Acc.AccountType = AccountType;
            //Acc.CustomerId = CusID;

            db.Entry(Acc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return View();
        }
    }
}