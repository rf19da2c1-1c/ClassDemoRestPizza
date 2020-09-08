using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaLib.model
{
    public class Pizza
    {
        private int _nr;
        private String _desciption;
        private bool _familyPizza;
        private int _price;

        public Pizza()
        {
        }

        public Pizza(int nr, string desciption, bool familyPizza, int price)
        {
            _nr = nr;
            _desciption = desciption;
            _familyPizza = familyPizza;
            _price = price;
        }

        public int Nr
        {
            get => _nr;
            set => _nr = value;
        }

        public string Desciption
        {
            get => _desciption;
            set => _desciption = value;
        }

        public bool FamilyPizza
        {
            get => _familyPizza;
            set => _familyPizza = value;
        }

        public int Price
        {
            get => _price;
            set => _price = value;
        }

        public override string ToString()
        {
            return $"{nameof(Nr)}: {Nr}, {nameof(Desciption)}: {Desciption}, {nameof(FamilyPizza)}: {FamilyPizza}, {nameof(Price)}: {Price}";
        }
    }
}
