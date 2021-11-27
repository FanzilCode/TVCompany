using System;
using System.Collections.Generic;
using System.Text;

namespace TVCompany
{
    class News : TVShow
    {
        // конструктор с тремя параметрами
        public News(string name, double minuteCost, double rating)
        {
            this.name = name;
            this.minuteCost = minuteCost;
            this.rating = (double)(rating/ 100);

            this.type = "Новости";
        }
        // конструктор для чтения из файла
        public News(string[] arr)
        {
            name = arr[0];
            minuteCost = Convert.ToDouble(arr[1]);
            rating = Convert.ToDouble(arr[2]);
            this.rating = (double)(rating / 100);

            type = "Новости";
        }
        public override double GetCost()
        {
            return minuteCost * rating * 0.95;
        }
    }
}
