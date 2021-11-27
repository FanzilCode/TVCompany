using System;
using System.Collections.Generic;
using System.Text;

namespace TVCompany
{
    interface ITVShow
    {
        // тип телепередачи
        public string type { get; set; }
        // название телепередачи
        public string name { get; set; }
        // стоимость минуты рекламы в данной телепередаче
        public double minuteCost { get; set; }
        // рейтинг телепередачи
        public double rating { get; set; }
        // метод Print() для печати на экран информации о телепередаче
        public void Print();
        public double GetCost();
    }
}
