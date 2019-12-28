using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZipShip.Models;

namespace ZipShip.Controllers
{
    public class AdminController : Controller
    {
         
        // GET: Admin
        public ActionResult Dashboard(string Message)
        {
            List<AdminDashboardViewModel> list = new List<AdminDashboardViewModel>();
            DBZipShipEntities db = new DBZipShipEntities();
            var orders = db.Orders.ToList();
            var trips = db.Trips.ToList();
            var deal = db.Deals.ToList();
            if(deal == null)
            {
                AdminDashboardViewModel a = new AdminDashboardViewModel();

                a.Traveller = "No Deal Yet";
                a.Shopper = "No Deal Yet";
                a.ZipShipEarning =Convert.ToDouble("No Deal Yet");
                a.Order = "No Deal Yet";
                
                list.Add(a);
            }
            else
            {
                AdminDashboardViewModel a = new AdminDashboardViewModel();

                foreach (var d in deal)
                {
                    var traveller = db.AspNetUsers.Where(x=> x.Id == d.SelectedBy).First();
                    a.Traveller = traveller.Name;
                    var order= db.Orders.Where(x => x.Id == d.OrderId).First();
                    var shopper = db.AspNetUsers.Where(x => x.Id == order.AddedBy).First();
                    a.Shopper = shopper.Name;
                    a.Order = order.Name;
                    long p =Convert.ToInt16(order.DealPrice - order.Price);
                    a.ZipShipEarning = Convert.ToDouble((0.07 * order.Price)+(0.07*p));
                    a.OrderId = order.Id;
                    list.Add(a);
                }
                
            }
            var admin = db.Admins.First();
            ViewBag.earning = admin.Earnings;
            ViewBag.Message = Message;
            return View(list);
        }

        public ActionResult RegisteredUsers()
        {
            List<UserViewModel> list = new List<UserViewModel>();
            DBZipShipEntities db = new DBZipShipEntities();
            var users = db.AspNetUsers.ToList();
            foreach(var i in users)
            {
                UserViewModel u = new UserViewModel();
                u.Name = i.Name;
                u.Address = i.Address;
                u.CNIC = i.CNIC;
                u.PhoneNumber = i.PhoneNumber;
                u.Email = i.Email;
                u.ImagePath = i.ImagePath;
                list.Add(u);
            }

            return View(list);
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel collection)
        {
            DBZipShipEntities db = new DBZipShipEntities();
            var admin = db.Admins.First();
            if(admin.Password == collection.Password && admin.Email == collection.Email)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                

                return RedirectToAction("Index", "Home", new { Message = "Invalid Admin Login!" });
            }
            
        }
        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        public ActionResult Select(int id)
        {
            DBZipShipEntities db = new DBZipShipEntities();
            var order = db.Orders.Where(x => x.Id == id).First();
            string shopperid = order.AddedBy;
            var shopper = db.AspNetUsers.Where(x => x.Id == shopperid).First();
            
            order.Status = "Deleivered";
            CompletedOrder comorder = new CompletedOrder();
            var deal = db.Deals.Where(x => x.OrderId == order.Id).First();
            var traveller = db.AspNetUsers.Where(x => x.Id == deal.SelectedBy).First();
            comorder.OrderName = order.Name;
            comorder.OrderCountry = order.Country;
            comorder.ShopperName = shopper.Name;
            comorder.TravellerName = traveller.Name;
            comorder.ImagePath = order.ImagePath;
            comorder.TravellerId = traveller.Id;
            comorder.ShopperId = shopper.Id;
            db.CompletedOrders.Add(comorder);

            var admin = db.Admins.First();
            long p = Convert.ToInt16(order.DealPrice - order.Price);
            double earnings = Convert.ToDouble((0.07 * order.Price) + (0.07 * p));
            admin.Earnings =Convert.ToInt16(admin.Earnings + earnings);

            string message = "Deal Between" + traveller.Name + " and " + shopper.Name +   " is Completed! " + earnings + " Rs is added to ZipShip Earnings";

            db.Entry(order).State = System.Data.Entity.EntityState.Deleted;
            db.Entry(deal).State = System.Data.Entity.EntityState.Deleted;

            db.SaveChanges();
            
            return RedirectToAction("Index", "Order", new { Message = message });
        }

        public ActionResult Invalid()
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit()
        {
            DBZipShipEntities db = new DBZipShipEntities();
            var admin = db.Admins.First();
            AdminViewModel a = new AdminViewModel();
            a.Name = admin.Name;
            a.Address = admin.Address;
            a.CNIC = admin.CNIC;
            a.PhoneNumber = admin.Phone;
            a.Email = admin.Email;
            a.Password = admin.Password;
            return View(a); 

        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(AdminViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                DBZipShipEntities db = new DBZipShipEntities();
                var admin = db.Admins.First();
                admin.Name = collection.Name;
                admin.CNIC = collection.CNIC;
                admin.Address = collection.Address;
                admin.Phone = collection.PhoneNumber;
                admin.Email = collection.Email;
                admin.Password = collection.Password;
                db.SaveChanges();
                
                var user = db.Admins.First();

                string message = "Your Information is Updated Successfully " + user.Name;
                return RedirectToAction("Index", "Order", new { Message = message });
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
