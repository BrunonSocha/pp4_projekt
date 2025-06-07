//using Microsoft.AspNetCore.Mvc;
//using EShopService.Models; // Adjust namespace as needed
////using EShopService.Data;   // Adjust namespace as needed
//using System.Linq;

//namespace EShopService.Controllers
//{
//    public class ShoppingCartController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public ShoppingCartController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: ShoppingCart
//        public IActionResult Index()
//        {
//            var carts = _context.ShoppingCarts.ToList();
//            return View(carts);
//        }

//        // GET: ShoppingCart/Details/5
//        public IActionResult Details(int id)
//        {
//            var cart = _context.ShoppingCarts.Find(id);
//            if (cart == null)
//                return NotFound();
//            return View(cart);
//        }

//        // GET: ShoppingCart/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: ShoppingCart/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(ShoppingCart cart)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.ShoppingCarts.Add(cart);
//                _context.SaveChanges();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(cart);
//        }

//        // GET: ShoppingCart/Edit/5
//        public IActionResult Edit(int id)
//        {
//            var cart = _context.ShoppingCarts.Find(id);
//            if (cart == null)
//                return NotFound();
//            return View(cart);
//        }

//        // POST: ShoppingCart/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(int id, ShoppingCart cart)
//        {
//            if (id != cart.Id)
//                return BadRequest();

//            if (ModelState.IsValid)
//            {
//                _context.Update(cart);
//                _context.SaveChanges();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(cart);
//        }

//        // GET: ShoppingCart/Delete/5
//        public IActionResult Delete(int id)
//        {
//            var cart = _context.ShoppingCarts.Find(id);
//            if (cart == null)
//                return NotFound();
//            return View(cart);
//        }

//        // POST: ShoppingCart/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            var cart = _context.ShoppingCarts.Find(id);
//            if (cart != null)
//            {
//                _context.ShoppingCarts.Remove(cart);
//                _context.SaveChanges();
//            }
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}