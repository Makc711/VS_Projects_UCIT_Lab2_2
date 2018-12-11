﻿using System;

namespace Lab2_2
{
    class Product : IEquatable<Product>
    {
        private readonly Department _department;
        private readonly Cashbox _cashbox;
        public string Name { get; }
        public int Quantity { get; private set; }
        public int Size { get; } // Занимаемая площадь одним продуктом, см^2
        public float Markup { get; } // Наценка (150% = 1,5)
        public int Pice { get; private set; }

        public Product(string name, int size, float markup, Department department)
        {
            Name = name;
            Size = size;
            Markup = markup;
            _department = department;
            _cashbox = department.GetCashbox();
            _department.AddProduct(this);
        }

        public void Buy(int quantity, int price)
        {
            _cashbox.Buy(quantity * price);
            _department.OccupyArea(quantity * Size);
            Quantity += quantity;
            Pice = (int) (price * Markup);
        }

        public void Sell(int quantity)
        {
            if (Quantity >= quantity)
            {
                Quantity -= quantity;
                _cashbox.Sell(quantity * Pice);
                _department.ClearArea(quantity * Size);
            }
            else
            {
                throw new ArgumentException($"Продукт {Name} закончился ({Quantity} < {quantity})");
            }
        }

        public override string ToString()
        {
            return $"{Name}; Количество: {Quantity}; Цена: {Pice}";
        }

        public bool Equals(Product other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((Product)obj);
        }

        public override int GetHashCode()
        {
            return 397 ^ (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
