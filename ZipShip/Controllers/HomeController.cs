using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ZipShip.Models;

namespace ZipShip.Controllers
{
    public class HomeController : Controller
    {

        


        public ActionResult Index(string Message)
        {
            DBZipShipEntities db = new DBZipShipEntities();


            //For Starter Initializations of Recent Orders & Feedbacks 
            /*for(int i=0;i<4;i++)
            {
                CompletedOrder c = new CompletedOrder();
                c.OrderCountry = "Order Country";
                c.OrderName = "Order Name";
                c.TravellerName = "Traveller Name";
                c.ShopperName = "Shopper Name";
                c.ImagePath = "/Content/Images/recentorder.png";
                db.CompletedOrders.Add(c);

                Review r = new Review();
                r.Name = "Person Name";
                r.ImagePath = "/Content/Images/user.png";
                r.Review1= "Person Review";
                db.Reviews.Add(r);
                db.SaveChanges();
            }*/
            
            List<ReviewViewModel> list = new List<ReviewViewModel>();
            var reviews = db.Reviews.ToList();
            foreach(var i in reviews)
            {
                ReviewViewModel r = new ReviewViewModel();
                r.Review = i.Review1;
                r.Name = i.Name;
                r.ImagePath = i.ImagePath;
                list.Add(r);
            }
            int count = list.Count;

            ReviewViewModel user1 = list[count - 1];
            ReviewViewModel user2= list[count - 2];
            ReviewViewModel user3 = list[count - 3];
            ReviewViewModel user4 = list[count - 4];






            ViewBag.p1 = user1.ImagePath;
            ViewBag.p2 = user2.ImagePath;
            ViewBag.p3 = user3.ImagePath;
            ViewBag.p4 = user4.ImagePath;

            ViewBag.n1 = user1.Name;
            ViewBag.n2 = user2.Name;
            ViewBag.n3 = user3.Name;
            ViewBag.n4 = user4.Name;

            ViewBag.r1 = user1.Review;
            ViewBag.r2 = user2.Review;
            ViewBag.r3 = user3.Review;
            ViewBag.r4 = user4.Review;

            List<CompletedOrdersViewModel1> listorders = new List<CompletedOrdersViewModel1>();
            var orders = db.CompletedOrders.ToList();
            foreach (var i in orders)
            {
                CompletedOrdersViewModel1 c = new CompletedOrdersViewModel1();
                c.OrderCountry = i.OrderCountry;
                c.OrderName = i.OrderName;
                c.ImagePath = i.ImagePath;
                c.TravellerName = i.TravellerName;
                c.ShopperName = i.ShopperName;
                listorders.Add(c);
            }
            int count2 = listorders.Count;

            CompletedOrdersViewModel1 corder1 = listorders[count2 - 1];
            CompletedOrdersViewModel1 corder2 = listorders[count2 - 2];
            CompletedOrdersViewModel1 corder3 = listorders[count2 - 3];
            CompletedOrdersViewModel1 corder4 = listorders[count2 - 4];






            ViewBag.ordersp1 = corder1.ImagePath;
            ViewBag.ordersp2 = corder2.ImagePath;
            ViewBag.ordersp3 = corder3.ImagePath;
            ViewBag.ordersp4 = corder4.ImagePath;

            ViewBag.ordersn1 = corder1.OrderName;
            ViewBag.ordersn2 = corder2.OrderName;
            ViewBag.ordersn3 = corder3.OrderName;
            ViewBag.ordersn4 = corder4.OrderName;

            ViewBag.ordersT1 = corder1.TravellerName;
            ViewBag.ordersT2 = corder2.TravellerName;
            ViewBag.ordersT3 = corder3.TravellerName;
            ViewBag.ordersT4 = corder4.TravellerName;

            ViewBag.ordersS1 = corder1.ShopperName;
            ViewBag.ordersS2 = corder2.ShopperName;
            ViewBag.ordersS3 = corder3.ShopperName;
            ViewBag.ordersS4 = corder4.ShopperName;

            ViewBag.country1 = corder1.OrderCountry;
            ViewBag.country2 = corder2.OrderCountry;
            ViewBag.country3 = corder4.OrderCountry;
            ViewBag.country4 = corder4.OrderCountry;

            ViewBag.Message = Message;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}