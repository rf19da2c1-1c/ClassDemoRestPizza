using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaLib.model
{
    public class QueryPizza
    {
        private String _family;
        private string _topping;

        public QueryPizza()
        {
        }

        public QueryPizza(String family, string topping)
        {
            _family = family;
            _topping = topping;
        }

        public String Family
        {
            get => _family;
            set => _family = value;
        }

        public string Topping
        {
            get => _topping;
            set => _topping = value;
        }

        public override string ToString()
        {
            return $"{nameof(Family)}: {Family}, {nameof(Topping)}: {Topping}";
        }
    }
}
