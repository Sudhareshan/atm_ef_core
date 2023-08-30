using ATM_EfCore_CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ATM_EfCore_CodeFirst.Controllers
{
    public class ATMController : Controller
    {
        private readonly Bank_CustDbContext _bankDbContext;
        private string name="name";
       
        private readonly IHttpContextAccessor _contextAccessor;

        public ATMController(Bank_CustDbContext bankDbContext, IHttpContextAccessor contextAccessor)
        {

            _bankDbContext = bankDbContext;
            _contextAccessor = contextAccessor;
        }
        public IActionResult CreateCustomerInfo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCustomerInfo(Bank_CustomersModel em)
        {
            Bank_CustomersModel model = new Bank_CustomersModel();
            model.AccountNumber = em.AccountNumber;
            model.PinNumber=new Random().Next(1000,9999);
            model.CustomerName = em.CustomerName;
            model.MobileNumber = em.MobileNumber;
            model.Balance = 2000;
            _bankDbContext.Customers.Add(model);
            _bankDbContext.SaveChanges();
            ViewBag.Userid = model.AccountNumber;
            ViewBag.Pwd = model.PinNumber;
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Bank_CustomersModel em)
        {
            
                var authorized_cust = _bankDbContext.Customers.FirstOrDefault(cust => cust.AccountNumber == em.AccountNumber && cust.PinNumber == em.PinNumber);
                if (authorized_cust == null)
                {
                    ViewBag.error = "Invalid Credentials..!!Try again";
                    return View();
                }
                else
                {
                  TempData["name"] = authorized_cust.CustomerName;
                  HttpContext.Session.SetString(name,authorized_cust.CustomerName);

                _contextAccessor.HttpContext.Session.SetInt32("AccNo", authorized_cust.AccountNumber);

               // Session["name"] = authorized_cust.CustomerName;
                  //TempData["Balance"] = authorized_cust.Balance;
                  return RedirectToAction("MainPage", "ATM");
                }
          
          
            
        }

        [HttpGet]
        public IActionResult MainPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Deposit()
        {
            if (HttpContext.Session.GetString(name)!=null)
            {
                // Retrieve the value associated with the "name" key
               // string customerName = TempData.Peek("name").ToString();
                 string customerName = HttpContext.Session.GetString(name);
                // Now you can use the customerName variable in your Deposit view or logic
                ViewBag.CustomerName = customerName;

                
            }

            return View();
        }
        [HttpPost]
        public IActionResult Deposit(Bank_CustomersModel em)
        {
            if (HttpContext.Session.GetString(name)!=null)
            {
                // string customerName = TempData["name"].ToString();
                /// double amount = Convert.ToDouble(TempData["Balance"]);

                string customerName = HttpContext.Session.GetString(name);

                ViewBag.CustomerName = customerName;

                var CustInfo=_bankDbContext.Customers.FirstOrDefault(cust=>cust.CustomerName==customerName);
                
                if (CustInfo != null)
                {
                    //Bank_CustomersModel model = new Bank_CustomersModel();
                    //model.Balance = em.Balance + CustInfo.Balance;
                    CustInfo.Balance += em.Balance;
                   //model.AccountNumber = CustInfo.AccountNumber;
                    _bankDbContext.Customers.Update(CustInfo);
                  
                    _bankDbContext.SaveChanges();
                }
                
            }
            return View();
        }

        public IActionResult Withdraw()
        {
            if (TempData.ContainsKey("name"))
            {
                // Retrieve the value associated with the "name" key
                string customerName = TempData.Peek("name").ToString();

                // Now you can use the customerName variable in your Deposit view or logic
                ViewBag.CustomerName = customerName;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Withdraw(Bank_CustomersModel em)
        {
            if (HttpContext.Session.GetString(name)!=null)
            {
               // string customerName = TempData["name"].ToString();

                string customerName = HttpContext.Session.GetString(name);

                ViewBag.CustomerName = customerName;

                var CustInfo = _bankDbContext.Customers.FirstOrDefault(cust => cust.CustomerName == customerName);

                if (CustInfo != null)
                {
                    

                    CustInfo.Balance -= em.Balance;
                   
                    if(CustInfo.Balance >= 2000)
                    {
                        _bankDbContext.Customers.Update(CustInfo);

                        _bankDbContext.SaveChanges();
                    }
                    else
                    {
                        ViewBag.error = "Invalied Funds";
                    }
                   
                }

            }
            return View();
        }


        public IActionResult ChangePwd()
        {
            if (HttpContext.Session.GetString(name) != null)
            {
                string customerName = HttpContext.Session.GetString(name);
                ViewBag.CustomerName = customerName;
            }
            return View();
        }

        [HttpPost]
        public IActionResult ChangePwd(Bank_CustomersModel em,int repwd,int ConformPwd)
        {
            if(HttpContext.Session.GetString(name) != null)
            {
                string customerName = HttpContext.Session.GetString(name);
                ViewBag.CustomerName = customerName;

                var custInfo = _bankDbContext.Customers.FirstOrDefault(pin => pin.CustomerName == customerName);
                if (custInfo.PinNumber == em.PinNumber)
                {
                    if (repwd == ConformPwd)
                    {
                        custInfo.PinNumber= repwd;
                       // _bankDbContext.Customers.Update(custInfo);
                        _bankDbContext.SaveChanges();
                    }
                }
                else
                {
                    ViewBag.error = "Wrong Pin Number";
                }
            }
            return View();
        }



        public IActionResult ShowBalance()
        {
            if(HttpContext.Session.GetString(name) != null) {
                string customerName = HttpContext.Session.GetString(name);
                ViewBag.CustomerName = customerName;

                var custInfo=_bankDbContext.Customers.FirstOrDefault(cust=>cust.CustomerName== customerName);

                if (custInfo != null)
                {
                    double availableBalance=custInfo.Balance;
                    ViewBag.availableBalance= availableBalance;

                    double efficentBalance = availableBalance - 2000;
                    ViewBag.efficentBalance= efficentBalance;
                }

            }
            return View();
        }

        public IActionResult MiniState()
        {
            if (HttpContext.Session.GetString(name) != null)
            {
                string customerName = HttpContext.Session.GetString(name);
                 var AccountNum = _contextAccessor.HttpContext.Session.GetInt32("AccNo");
                ViewBag.CustomerName = customerName;
                var parameter = new[]
                {
                    new SqlParameter("@id",AccountNum),
               };
            var miniSatemnet = _bankDbContext.Transations.FromSqlRaw("EXEC GetMinistate @id", parameter);
                return View(miniSatemnet);
            }
            return View();
        }
    }
}
