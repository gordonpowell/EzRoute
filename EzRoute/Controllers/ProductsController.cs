using EzRoute.Models;
using EzRoute.Models.ViewModel;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EzRoute.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private EzRouteEntities db = new EzRouteEntities();

        // GET: Products
        [AcceptVerbs(HttpVerbs.Get|HttpVerbs.Post)]
        public ActionResult Index(SearchViewModel viewModel)
        {
            IQueryable<Product> products = db.Products;
            bool searchemail = false;
            if(viewModel!= null)
            {
                if(viewModel.productID!= null&& viewModel.productID.Any())
                {
                    products = products.Join(viewModel.productID, p => p.productID, v => v, (p, v) => p);
                    searchemail = true;
                }
                if(viewModel.tagID!= null)
                {
                    products = products.Where(p => p.tagID == viewModel.tagID);
                    searchemail = true;
                }
                if(viewModel.aisleID!= null)
                {
                    products = products.Where(p => p.aisleID == viewModel.aisleID);
                    searchemail = true;
                }

            }
            else
            {
                viewModel = new SearchViewModel();

            }ViewBag.SearchModel = viewModel;

            if(searchemail)
            {
                var emailaddress = User.Identity.GetUserName();
                string emailmessage = @"Here's the items you requested


";
                foreach (var product in products)
                {
                    emailmessage += product.name + ", price $"+product.price+", size "+product.size+", aisle " +product.Aisle.number+" "+ product.Tag.name + "\n";
                }
                emailmessage += @"

Thank You for using EasyRoute"; EmailUtility.sendMail(emailaddress, emailmessage, "Here's the items you requested");
            }
                
            return View(products.ToList());
        }
        [ChildActionOnly]
        public ActionResult Search()
        {
            ViewBag.aislelist = new SelectList(db.Aisles, "aisleID", "number");
            ViewBag.taglist = new SelectList(db.Tags, "tagID", "name");
            ViewBag.productlist = new MultiSelectList(db.Products, "productID", "name");
            return PartialView();
        }
        
        
            
        
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.aisleID = new SelectList(db.Aisles, "aisleID", "number");
            ViewBag.tagID = new SelectList(db.Tags, "tagID", "name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,price,size,aisleID,tagID,productID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.aisleID = new SelectList(db.Aisles, "aisleID", "number", product.aisleID);
            ViewBag.tagID = new SelectList(db.Tags, "tagID", "name", product.tagID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.aisleID = new SelectList(db.Aisles, "aisleID", "number", product.aisleID);
            ViewBag.tagID = new SelectList(db.Tags, "tagID", "name", product.tagID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "name,price,size,aisleID,tagID,productID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.aisleID = new SelectList(db.Aisles, "aisleID", "number", product.aisleID);
            ViewBag.tagID = new SelectList(db.Tags, "tagID", "name", product.tagID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
