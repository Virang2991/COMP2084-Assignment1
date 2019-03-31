using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOnFinger.Models
{
    public class IDataProducts : IMockProducts
    {
        //db connection

        private ProductView db = new ProductView();
               
        public IQueryable<Product> Products { get { return db.Products; } }
        
        public IQueryable<Cuisine> Cuisines { get { return db.Cuisines; } }

        public void Delete(Product product)
        {
            db.Products.Remove(product);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Product Save(Product product)
        {
            if (product.ProductID == 0)
            {
                db.Products.Add(product);
            }
            else
            {
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            }            
            db.SaveChanges();
            return product;
        }

    }

}