using System;

namespace TVCompany
{
    abstract class TVShow : ITVShow
    {
        // тип телепередачи
        public string type { get; set; }
        // название телепередачи
        public string name { get; set; }
        // стоимость минуты рекламы в данной телепередаче
        public double minuteCost { get; set; }
        // рейтинг телепередачи
        public double rating { get; set; }
        // метод для печати на экран информации о телепередаче
        public void Print()
        {
            Console.WriteLine($"Название: {name}\n" +
                $"Стоимость минуты рекламы: {minuteCost} рублей\n" +
                $"Рейтинг: {rating*100}%");
        }
        // переопределение метода ToString() для сохранения в файл
        public override string ToString()
        {
            return $"{type}\n" +
                $"{name}%{minuteCost}%{rating}";
        }
        public abstract double GetCost();
        public static bool operator ==(TVShow tVShow1, TVShow tVShow2)
        {
            return (tVShow1.type == tVShow2.type && tVShow1.name == tVShow2.name && tVShow1.minuteCost == tVShow2.minuteCost
                && tVShow1.rating == tVShow2.rating);
        }
        public static bool operator != (TVShow tVShow1, TVShow tVShow2)
        {
            return !(tVShow1 == tVShow2);
        }
    }
}
