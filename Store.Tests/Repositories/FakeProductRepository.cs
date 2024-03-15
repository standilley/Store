using Store.Domain.Entities;
using Store.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.Repositories
{
    internal class FakeProductRepository : IProductRepository
    {
        public IEnumerable<Product> Get(IEnumerable<Guid> ids)
        {
            IList<Product> products = new List<Product>();
            products.Add(new Product("produto 01", 10, true));
            products.Add(new Product("produto 02", 10, true));
            products.Add(new Product("produto 03", 10, true));
            products.Add(new Product("produto 04", 10, false));
            products.Add(new Product("produto 05", 10, false));

            return products;
        }
    }
}
