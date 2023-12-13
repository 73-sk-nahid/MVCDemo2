using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo2.Models;

namespace MVCDemo2.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            using (Product_DBEntities1 db = new Product_DBEntities1())
            {
                List<tblproduct> ProductList = (from data in db.tblproducts select data).ToList();
                return View(ProductList);
            }
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View(new tblproduct());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(tblproduct product, HttpPostedFileBase postedFile)
        {
            try
            {
                // TODO: Add insert logic here
                string extension = Path.GetExtension(postedFile.FileName);
                if(extension.Equals(".jpg") || extension.Equals(".png"))
                {
                    string filename = "IMG-" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + extension;
                    string savepath = Server.MapPath("~/Content/images/");
                    postedFile.SaveAs(savepath + filename);
                    product.prod_pic = filename;
                    using (Product_DBEntities1 db = new Product_DBEntities1())
                    {
                        db.tblproducts.Add(product);
                        db.SaveChanges();
                    }
                }
                else
                {
                    return Content("<h1>You can only upload in JPG or PNG format</h1>");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            using (Product_DBEntities1 db = new Product_DBEntities1())
            {
                //tblproduct product = (from data in db.tblproducts where data.prod_id == id select data).Single();
                tblproduct product = db.tblproducts.Find(id);
                return View(product);
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(tblproduct product, HttpPostedFileBase postedFile)
        {
            try
            {
                // TODO: Add update logic here
                string filename = "";
                if (postedFile != null)
                {
                    string extension = Path.GetExtension(postedFile.FileName);
                    if (extension.Equals(".jpg") || extension.Equals(".png"))
                    {
                        filename = "IMG-" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + extension;
                        string savepath = Server.MapPath("~/Content/images/");
                        postedFile.SaveAs(savepath + filename);                       
                    }
                }
                using (Product_DBEntities1 db = new Product_DBEntities1())
                {
                    tblproduct tbl = db.tblproducts.Find(product.prod_id);
                    tbl.prod_name = product.prod_name;
                    tbl.prod_price = product.prod_price;
                    tbl.prod_qty = product.prod_qty;
                    if(!filename.Equals(""))
                    {
                        tbl.prod_pic = filename;
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (Product_DBEntities1 db = new Product_DBEntities1())
            {
                db.tblproducts.Remove(db.tblproducts.Find(id));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // POST: Product/Delete/5
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
