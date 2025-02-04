using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyRestaurant.Models;

namespace MyRestaurant.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;

        public AdminController(ModelContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //for session
            ViewData["username"] = HttpContext.Session.GetString("AdminName");
            ViewData["adminId"] = HttpContext.Session.GetInt32("AdminId");

            ViewBag.CustomerCount = _context.UserCustomers.Count();

            ViewBag.TotalSale = _context.UserProducts.Sum(x => x.Sale);

            ViewData["MaxPrice"] =_context.UserProducts.Max(x => x.Price);
            ViewData["MinPrice"] = _context.UserProducts.Min(x => x.Price);

            var product = _context.UserProducts.ToList();
            var customer = _context.UserCustomers.ToList();
            var category = _context.UserCategories.ToList();


            var finalResult = Tuple.Create<IEnumerable<UserProduct>,
    IEnumerable<UserCustomer>, IEnumerable<UserCategory>>(product, customer, category);



            return View(finalResult);
        }

        public IActionResult JoinTable()
        {
            var products = _context.UserProducts.ToList();
            var customers = _context.UserCustomers.ToList();
            var categories = _context.UserCategories.ToList();
            var productCustomers = _context.UserProductCustomers.ToList();


            var result = from c in customers
                         join pc in productCustomers
                         on c.Id equals pc.CustomerId
                         join p in products
                         on pc.ProductId equals p.Id
                         join cat in categories
                         on p.CategoryId equals cat.Id

                         select new JoinTables
                         {
                             Product = p,
                             Customer = c,
                             Category = cat,
                             CustomerProduct = pc

                         };
            

            return View(result);

        }

        public IActionResult Search()
        {
            var result = _context.UserProductCustomers.Include(x => x.Product).Include(x => x.Customer).ToList();
            ViewBag.TotalQuantity = result.Sum(x => x.Quantity);
            ViewBag.TotalPrice = result.Sum(x => x.Product.Price * x.Quantity);

            return View(result);
        }
        [HttpPost]
        public IActionResult Search(DateTime? startDate, DateTime? endDate)
        {
            var result = _context.UserProductCustomers.Include(x => x.Product).Include(x => x.Customer).ToList();

            if (startDate == null && endDate == null)
            {
                ViewBag.TotalQuantity = result.Sum(x => x.Quantity);
                ViewBag.TotalPrice = result.Sum(x => x.Product.Price * x.Quantity);
                return View(result);
            }
            else if (startDate != null && endDate == null)
            {
                result = result.Where(x => x.DateFrom.Value.Date >= startDate).ToList();
                ViewBag.TotalQuantity = result.Sum(x => x.Quantity);
                ViewBag.TotalPrice = result.Sum(x => x.Product.Price * x.Quantity);

                return View(result);
            }
            else if (startDate == null && endDate != null)
            {
                result = result.Where(x => x.DateFrom.Value.Date <= endDate).ToList();
                ViewBag.TotalQuantity = result.Sum(x => x.Quantity);
                ViewBag.TotalPrice = result.Sum(x => x.Product.Price * x.Quantity);

                return View(result);
            }

            else
            {
                result = result.Where(x => x.DateFrom.Value.Date >= startDate && x.DateFrom.Value.Date <= endDate).ToList();
                ViewBag.TotalQuantity = result.Sum(x => x.Quantity);
                ViewBag.TotalPrice = result.Sum(x => x.Product.Price * x.Quantity);

                return View(result);

            }

        }

    }
}
