using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZipShip.Models;

namespace ZipShip.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index(string Message)
        {

            DBZipShipEntities db = new DBZipShipEntities();
            var orders = db.Orders.ToList();
            
            List<OrderListViewModel> list = new List<OrderListViewModel>();
            foreach(var i in orders)
            {
                if(i.Status != "Selected")
                {
                    OrderListViewModel o = new OrderListViewModel();
                    o.Id = i.Id;
                    o.Name = i.Name;
                    o.Quantity = i.Quantity;
                    o.Price = Convert.ToDouble(i.Price);
                    o.DealPrice = Convert.ToDouble(i.DealPrice);
                    var forname = db.AspNetUsers.Where(x => x.Id == i.AddedBy).First();
                    o.AddedByName = forname.Name;
                    o.AddedBy = i.AddedBy;
                    o.ImagePath = i.ImagePath;
                    o.Country = i.Country;
                    o.Brand = i.Brand;
                    if (o.AddedBy != User.Identity.GetUserId())
                    {
                        list.Add(o);
                    }
                }
            }
            ViewBag.Message = Message;
            
            return View(list);
        }

        public ActionResult SelectedOrder()
        {

            DBZipShipEntities db = new DBZipShipEntities();
            string personID = User.Identity.GetUserId();
            var deals = db.Deals.ToList();
            List<int> list = new List<int>();
            foreach (Deal d in deals)
            {
                if(d.SelectedBy == personID)
                {
                    int i = Convert.ToInt16(d.OrderId);
                    list.Add(i);
                }
            }
            List<OrderListViewModel> listOfOrders = new List<OrderListViewModel>();
            var AllOrders = db.Orders.ToList();
            foreach(var v in AllOrders)
            {
                foreach(int a in list)
                {
                    if(v.Id == a)
                    {
                        OrderListViewModel o = new OrderListViewModel();
                        o.Id = v.Id;
                        o.Name = v.Name;
                        o.Quantity = v.Quantity;
                        o.Price = Convert.ToDouble(v.Price);
                        o.DealPrice = Convert.ToDouble(v.DealPrice);
                        o.AddedBy = v.AddedBy;
                        o.ImagePath = v.ImagePath;
                        o.Country = v.Country;
                        o.Brand = v.Brand;
                        listOfOrders.Add(o);
                    }
                }
            }
            
        
            return View(listOfOrders);
        }
        // GET: Order/MyList
        public ActionResult MyList()
        {
            DBZipShipEntities db = new DBZipShipEntities();
            string personID = User.Identity.GetUserId();
            var orders = db.Orders.Where(x => x.AddedBy == personID);
            List<OrderListViewModel> list = new List<OrderListViewModel>();
            foreach (var i in orders)
            {
                OrderListViewModel o = new OrderListViewModel();
                o.Id = i.Id;
                o.Name = i.Name;
                o.Quantity = i.Quantity;
                o.Price = Convert.ToDouble(i.Price);
                o.DealPrice = Convert.ToDouble(i.DealPrice);
                o.ImagePath = i.ImagePath;
                o.Country = i.Country;
                o.Brand = i.Brand;
                list.Add(o);
            }
            return View(list);
        }

        // GET: Order/Details/5
        public ActionResult Details(string id)
        {
            DBZipShipEntities db = new DBZipShipEntities();
            UserViewModel u = new UserViewModel();
            var user = db.AspNetUsers.Where(x => x.Id == id).First();
            u.Name = user.Name;
            u.Email = user.Email;
            u.Address = user.Address;
            u.CNIC = user.CNIC;
            u.PhoneNumber = user.PhoneNumber;
            u.ImagePath = user.ImagePath;
            return View(u);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(OrderViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                DBZipShipEntities db = new DBZipShipEntities();
                string id = User.Identity.GetUserId();

                var user = db.AspNetUsers.Where(x => x.Id == id).First();
                string name = user.Name;
                Order o = new Order();
                if (collection.Image != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(collection.Image.FileName);
                    string ext = Path.GetExtension(collection.Image.FileName);
                    filename = filename + DateTime.Now.Millisecond.ToString();
                    filename = filename + ext;
                    string filetodb = "/Image/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                    collection.Image.SaveAs(filename);
                    collection.ImagePath = filetodb;
                }
                else
                {
                    collection.ImagePath = "/Content/Images/recentorder.png";
                }
                o.Name = collection.Name;
                o.Quantity = collection.Quantity;
                o.Price =Convert.ToInt64(collection.Price);
                o.DealPrice =Convert.ToInt64(collection.DealPrice);
                o.AddedBy = User.Identity.GetUserId();
                o.AddedOn = DateTime.Now.Date;
                o.ImagePath = collection.ImagePath;
                o.Country = collection.Country;
                o.Brand = collection.Brand;
                db.Orders.Add(o);
                db.SaveChanges();
                string message = "Your Order is Created Successfully " + name;

                return RedirectToAction("Index", "Order", new { Message = message });
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            DBZipShipEntities db = new DBZipShipEntities();
            OrderViewModel user = new OrderViewModel();
            foreach (Order p in db.Orders)
            {
                if (id == p.Id)
                {
                    user.Name = p.Name;
                    user.Quantity = p.Quantity;
                    user.Price =Convert.ToInt16(p.Price);
                    user.DealPrice = Convert.ToInt16(p.DealPrice);
                    user.ImagePath = p.ImagePath;
                    user.Brand = p.Brand;
                    user.Country = p.Country;
                    break;
                }
            }
            return View(user);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, OrderViewModel collection)
        {
            // TODO: Add update logic here
            try
            {
                DBZipShipEntities db = new DBZipShipEntities();
                OrderViewModel user = new OrderViewModel();
                string userid = User.Identity.GetUserId();


                if (collection.Image != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(collection.Image.FileName);
                    string ext = Path.GetExtension(collection.Image.FileName);
                    filename = filename + DateTime.Now.Millisecond.ToString();
                    filename = filename + ext;
                    string filetodb = "/Image/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                    collection.Image.SaveAs(filename);
                    collection.ImagePath = filetodb;
                }
                else
                {
                    collection.ImagePath = "/Content/Images/recentorder.png";
                }

                var curruser = db.AspNetUsers.Where(x => x.Id == userid).First();
                string name = curruser.Name;
                foreach (Order p in db.Orders)
                {
                    if (p.Id == id)
                    {

                        p.Name = collection.Name;
                        p.Quantity = collection.Quantity;
                        p.Price = Convert.ToInt16(collection.Price);
                        p.DealPrice = Convert.ToInt16(collection.DealPrice);
                        p.ImagePath = collection.ImagePath;
                        p.Brand = collection.Brand;
                        p.Country = collection.Country;
                        break;
                    }
                }
                db.SaveChanges();
                string message = "Your Order is Updated Successfully " + name;
                return RedirectToAction("Index", "Order", new { Message = message });
            }
            catch
            {
                return View();
            }
            
        }


        
        // POST: Order/Select/5
        
        public ActionResult Select(int id,OrderViewModel collection)
        {
            
            DBZipShipEntities db = new DBZipShipEntities();
            var s = db.Orders.Where(x => x.Id == id).First();
            s.Status = "Selected";
            Deal d = new Deal();
            d.OrderId = id;
            d.SelectedBy = User.Identity.GetUserId();
            db.Deals.Add(d);
            db.SaveChanges();
            string message = "Your Order is Selected";
            return RedirectToAction("Index", "Order", new { Message = message });

        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)  //Delete for Deal
        {
            try
            {
                // TODO: Add delete logic here
                DBZipShipEntities db = new DBZipShipEntities();
                var order = db.Deals.Where(x => x.OrderId == id).First();
                db.Entry(order).State = System.Data.Entity.EntityState.Deleted;
                var s = db.Orders.Where(x => x.Id == id).First();
                s.Status = null;
                db.SaveChanges();
                string message = "Your Deal is Deleted";
                return RedirectToAction("Index", "Order", new { Message = message });
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/DeleteOrder/5
        public ActionResult DeleteOrder(int id)
        {
            return View();
        }

        // POST: Order/DeleteOrder/5
        [HttpPost]
        public ActionResult DeleteOrder(int id, OrderViewModel collection)
        {
                // TODO: Add delete logic here
                DBZipShipEntities db = new DBZipShipEntities();
                var order = db.Orders.Where(x => x.Id == id).First();
                db.Entry(order).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            string message = "Your Order is Deleted";
            return RedirectToAction("Index", "Order", new { Message = message });

        }
    }
}
