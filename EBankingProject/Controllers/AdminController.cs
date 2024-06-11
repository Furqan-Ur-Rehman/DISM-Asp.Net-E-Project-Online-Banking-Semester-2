using EBankingProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBankingProject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        EbankingEntities12 db = new EbankingEntities12();

        // GET: Admin

        public ActionResult Index()
        {
            return View();

        }
        [HttpGet]
        public ActionResult AddBranch()
        {

            return View();

        }
        [HttpPost]
        public ActionResult AddBranch(branch b)
        {
            db.branches.Add(b);
            db.SaveChanges();
            ModelState.Clear();
            return View();

        }

        public ActionResult ListBranch()
        {
            var BranchList = db.branches.ToList();
            return View(BranchList);

        }

        public ActionResult AddAccountType()
        {

            return View();

        }
        [HttpPost]
        public ActionResult AddAccountType(AccountType account)
        {
            db.AccountTypes.Add(account);
            db.SaveChanges();
            ModelState.Clear();
            return View();

        }

        public ActionResult ListAccountType()
        {
            var listofAccountType = db.AccountTypes.ToList();
            return View(listofAccountType);

        }

        [HttpGet]
        public ActionResult AddCustomer()
        {
            List<branch> Branchlist = db.branches.ToList();
            ViewBag.BList = new SelectList(Branchlist, "BranchId", "BranchName");


            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer, HttpPostedFileBase uploadImg)
        {
            if (ModelState.IsValid)
            {
                List<branch> Branchlist = db.branches.ToList();
                ViewBag.BList = new SelectList(Branchlist, "BranchId", "BranchName");

                var filename = Path.GetFileName(uploadImg.FileName);
                uploadImg.SaveAs(Server.MapPath("~/img/" + filename));
                customer.Image = filename;
                customer.Role = "V";
                db.Customers.Add(customer);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("ListCustomer");
            }
            return View();
        }

        //Customer List
        public ActionResult ListCustomer()
        {
            var CustomeerList = db.Customers.ToList();
            return View(CustomeerList);

        }
        [HttpGet]
        public ActionResult CustomerEdit(int id)
        {
            var Edition = db.Customers.Find(id);
            Session["Image"] = Edition.Image;
            List<branch> Branchlist = db.branches.ToList();
            ViewBag.BList = new SelectList(Branchlist, "branchId", "branchName");
            return View(Edition);

        }
        [HttpPost]
        public ActionResult CustomerEdit(Customer customer, HttpPostedFileBase uploadImg)
        {
            if (ModelState.IsValid)
            {
                List<branch> Branchlist = db.branches.ToList();
                ViewBag.BList = new SelectList(Branchlist, "BranchId", "BranchName");

                if (uploadImg != null)
                {
                    string filename = Path.GetFileName(uploadImg.FileName);
                    //string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                    string extension = Path.GetExtension(uploadImg.FileName);

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {

                        if (uploadImg.ContentLength <= 1000000)
                        {
                            string path = Path.Combine(Server.MapPath("~/img/"), filename);
                            //uploadfile.SaveAs(Server.MapPath("~/Image/" + filename));
                            customer.Image = path;
                            uploadImg.SaveAs(path);
                            Session["image"] = uploadImg;
                            db.Entry(customer).State = System.Data.Entity.EntityState.Modified;

                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                TempData["EditMessage"] = "Data is updated/Edited Successfully!";
                                ModelState.Clear();
                                return RedirectToAction("ListCustomer");

                            }
                            else
                            {
                                TempData["EditMessage"] = "Data is Failed for updation / Edition, please try again!";
                            }

                        }
                        else
                        {
                            TempData["SizeMessage"] = "Image or Document size  from 'Choose File' must be less than or equal to 1 MB!";
                            TempData["InsertelseMessage"] = "Hence, Data you updated is not Saved, Please try again!";
                        }
                    }

                    else
                    {
                        TempData["ExtensionMessage"] = "Document format from 'Choose File', is not supported!";
                        TempData["InsertelseMessage"] = "Hence, Data you updated is not Saved, Please try again!";
                    }
                    return View("Edit");
                }
            }
            return View();

        }

        public ActionResult CustomerDelete(int id)
        {
            var Deletion = db.Customers.Find(id);
            db.Customers.Remove(Deletion);
            var del = db.SaveChanges();

            if (del > 0)
            {
                TempData["DeleteMessage"] = "Customer is Deleted Successfully!";
                return RedirectToAction("ListCustomer");
            }

            else
            {
                TempData["DeleteMessage"] = "Customer is not Deleted Successfully!";
            }
            return View();

        }
        //Details of Customers
        public ActionResult CustomerDetails(int id)
        {
            var Detail = db.Customers.Find(id);
            Session["Image"] = Detail.Image;
            return View(Detail);

        }
        [HttpGet]
        public ActionResult Account(int id)
        {
            var Edition = db.Customers.Find(id);
            Session["CustName"] = Edition.firstName;
            Session["Address"] = Edition.Address;

            List<Customer> Custlist = db.Customers.ToList();
            ViewBag.CustList = new SelectList(Custlist, "CustId", "EmailId");

            List<AccountType> AccTypelist = db.AccountTypes.ToList();
            ViewBag.AccList = new SelectList(AccTypelist, "id", "TypeOfAccount");

            return View();
        }
        [HttpPost]
        public ActionResult Account(Account Acc)
        {
            if (ModelState.IsValid)
            {

                List<Customer> Custlist = db.Customers.ToList();
                ViewBag.CustList = new SelectList(Custlist, "CustId", "EmailId");


                List<AccountType> AccTypelist = db.AccountTypes.ToList();
                ViewBag.AccList = new SelectList(AccTypelist, "id", "TypeOfAccount");


                db.Accounts.Add(Acc);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("ListAccount");
            }
            return View();
        }

        public ActionResult ListAccount()
        {
            var AccountList = db.Accounts.ToList();

            return View(AccountList);

        }

        [HttpGet]
        public ActionResult EditAccount(int id)
        {

            var EditAcc = db.Accounts.Find(id);
            Session["AccountNo"] = EditAcc.AccountNO;
            Session["Balance"] = EditAcc.Balance;
            List<Customer> Custlist = db.Customers.ToList();
            ViewBag.CustList = new SelectList(Custlist, "CustId", "EmailId");

            List<AccountType> AccTypelist = db.AccountTypes.ToList();
            ViewBag.AccList = new SelectList(AccTypelist, "id", "TypeOfAccount");
            return View(EditAcc);
        }
        [HttpPost]
        public ActionResult EditAccount(Account Acc)
        {
            List<Customer> Custlist = db.Customers.ToList();
            ViewBag.CustList = new SelectList(Custlist, "CustId", "EmailId");

            List<AccountType> AccTypelist = db.AccountTypes.ToList();
            ViewBag.AccList = new SelectList(AccTypelist, "id", "TypeOfAccount");

            db.Entry(Acc).State = System.Data.Entity.EntityState.Modified;

            int a = db.SaveChanges();
            if (a > 0)
            {
                TempData["EditMessage"] = "Data is updated/Edited Successfully!";
                ModelState.Clear();
                return RedirectToAction("ListAccount");

            }
            else
            {
                TempData["EditMessage"] = "Data is Failed for updation / Edition, please try again!";
            }

            return View();
        }

        public ActionResult DeleteAccount(int id)
        {
            var Deletion = db.Accounts.Find(id);

            db.Accounts.Remove(Deletion);
            var del = db.SaveChanges();

            if (del > 0)
            {
                TempData["DeleteMessage"] = "Account is Deleted Successfully!";
                return RedirectToAction("ListAccount");
            }

            else
            {
                TempData["DeleteMessage"] = "Account is not Deleted Successfully!";
            }
            return View();
        }

        public ActionResult FundsTransfer()
        {
            var list = db.Transactions.ToList();
            return View(list);
        }

        public ActionResult ChangeofAddress()
        {
            var list = db.ChangeofAddresses.ToList();
            return View(list);
        }


        public ActionResult RequestforChequeBook()
        {
            var list = db.RequestofChequeBooks.ToList();
            return View(list);
        }

        public ActionResult StopPaymentofCheque()
        {
            var list = db.StopPaymentofCheques.ToList();
            return View(list);
        }
        [HttpGet]
        //public ActionResult Calculate()
        //{
        //    //var list = db.Accounts.ToList();
        //    //var query = db.Accounts.Where(x => x.AccountNO == id).SingleOrDefault();
        //    return View();
        //}

        //List<Calculation> li = new List<Calculation>();
        //[HttpPost]
        //public ActionResult Calculate(int id)
        //{
        //    //var list = db.Accounts.ToList();
        //    var query = db.Accounts.Where(x => x.AccountNO == id).SingleOrDefault();
        //    Calculation cal = new Calculation();
        //    cal.AccountNO = query.AccountNO;
        //    cal.AccountDate = (DateTime)query.AccountDate;
        //    cal.Balance = (decimal)query.Balance;
        //    cal.Debit = (decimal)query.Debit;
        //    cal.Credit = (decimal)query.Credit;
        //    cal.Description = query.Description;
        //    cal.DocId = (int)query.DocId;
        //    cal.Total = cal.Balance + cal.Credit;  /*10500 =  500 + 10000*/
        //    cal.Total -= cal.Debit;   /*10000  = 10500 - 5000*/

        //    if (Session["Calculations"] == null)
        //    {
        //        li.Add(cal);
        //        Session["Calculations"] = li;
        //    }

        //    else
        //    {

        //        List<Calculation> li2 = Session["Calculations"] as List<Calculation>;
        //        int flag = 0;

        //        foreach (var item in li2)
        //        {
        //            if (item.AccountNO == cal.AccountNO)
        //            {
        //                item.Total += cal.Total;
        //                //item.qty += c.qty;
        //                flag = 1;

        //            }

        //        }

        //        if (flag == 0)
        //        {
        //            li2.Add(cal);
        //        }
        //        Session["Calculations"] = li2;
        //    }


        //    if (Session["Cart"] != null)
        //    {
        //        int x = 0;
        //        List<Calculation> li2 = Session["Calculations"] as List<Calculation>;
        //        foreach (var item in li2)
        //        {
        //            ((void)(x += item.Total));
        //        }
        //        Session["total"] = x;
        //        Session["item_count"] = li2.Count();
        //    }
        //    return RedirectToAction("AccountStatement");
        //}


        public ActionResult AccountStatement1(int id)
        {
            //var list = db.Accounts.ToList();
            var Edition = db.Accounts.Find(id);
            

            return View(Edition);
        }

        [HttpPost]
        public ActionResult AccountStatement1(Account account)
        {
            //var list = db.Accounts.ToList();
            db.Entry(account).State = System.Data.Entity.EntityState.Modified;

            int a = db.SaveChanges();
            if (a > 0)
            {
                TempData["EditMessage"] = "Data is updated/Edited Successfully!";
                ModelState.Clear();
                return RedirectToAction("ListAccount");

            }
            else
            {
                TempData["EditMessage"] = "Data is Failed for updation / Edition, please try again!";
            }
            return View();

        }
        //[HttpPost]
        //public ActionResult AccountStatement(int id, int Bal)
        //{
        //    var query = db.Accounts.Where(x => x.AccountNO == id).SingleOrDefault();



        //    return View(query);
        //}



    }
}