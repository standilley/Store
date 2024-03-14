﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Product : Entity
    {
        public Product(string title, decimal price, bool active)
        {
            Title = title;
            Price = price;
            Active = active;
        }

        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public bool Active { get; private set; }
    }
}
