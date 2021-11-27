using System;
using System.Collections.Generic;
using System.Text;

namespace TVCompany
{
    class Comic : TVShow
    {
        // конструктор с тремя параметрами
        public Comic(string name, double minuteCost, double rating)
        {
            this.name = name;
            this.minuteCost = minuteCost;
            this.rating = (double)(rating / 100);

            this.type = "Юмористический";
        }
        // конструктор для чтения из файла
        public Comic(string[] arr)
        {
            name = arr[0];
            minuteCost = Convert.ToDouble(arr[1]);
            rating = Convert.ToDouble(arr[2]);
            this.rating = (double)(rating / 100);

            type = "Юмористический";
        }
        public override double GetCost()
        {
            return minuteCost * rating * 0.9;
        }
    }
}
