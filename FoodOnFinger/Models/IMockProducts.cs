using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOnFinger.Models
{
    public interface IMockProducts

    {

        IQueryable<Product> Products { get; }

        IQueryable<Cuisine> Cuisines { get; }

        Product Save(Product product);

        void Delete(Product product);

        void Dispose();

    }

}
