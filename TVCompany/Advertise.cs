using System;
using System.Collections.Generic;
using System.Text;

namespace TVCompany
{
    class Advertise // реклама
    {
        // передача
        public ITVShow tVShow { get; private set; }
        // заказчик
        public Customer customer { get; private set; }
        // дата
        public DateTime date { get; private set; }
        // длительность (в минутах)
        public int time { get; private set; }
        // агент, заключивший сделку
        public Agent agent { get; private set; }
        // конструктор с 4-мя параметрами
        public Advertise(ITVShow tVShow, Customer customer, DateTime date, int time, Agent agent)
        {
            this.tVShow = tVShow;
            this.customer = customer;
            this.date = date;
            this.time = time;
            this.agent = agent;
        }
        // переопределение метода ToString для записи в файл
        public override string ToString()
        {
            return $"{tVShow}\n" +
                $"{customer}\n" +
                $"{date.ToShortDateString()}\n" +
                $"{time}\n" +
                $"{agent}";
        }
        // метод Print() для печати на экран информации о рекламе
        public void Print()
        {
            Console.WriteLine("Телепередача:");
            tVShow.Print();
            Console.WriteLine("Заказчик:");
            customer.Print();
            Console.WriteLine($"Дата рекламы: {date.ToShortDateString()}\n" +
                $"Длительность(в минутах): {time}");
            Console.WriteLine("Агент, заключивший сделку:");
            Console.WriteLine($"Общая стоимость рекламы(в рублях): {GetCost()}");
            agent.Print();
        }
        // метод, для вычисления стоимости рекламы
        public double GetCost()
        {
            return time * tVShow.GetCost();
        }
        public bool IsAvailable(DateTime date1, DateTime date2, ITVShow tVShow)
        {
            return (date1 <= date && date2 >= date && this.tVShow == tVShow);
        }
        public bool IsAvailable(DateTime date1, DateTime date2, Customer customer)
        {
            return (date1 <= date && date2 >= date && this.customer == customer);
        }
        public bool IsAvailable(DateTime date1, DateTime date2, Agent agent)
        {
            return (date1 <= date && date2 >= date && this.agent == agent);
        }
        public bool IsAvailable(DateTime date1, DateTime date2)
        {
            return (date1 <= date && date2 >= date);
        }
        public static bool operator == (Advertise ad1, Advertise ad2)
        {
            return (ad1.tVShow == ad2.tVShow && ad1.customer == ad2.customer && ad1.agent == ad2.agent && ad1.date == ad2.date
                && ad1.time == ad2.time);
        }
        public static bool operator !=(Advertise ad1, Advertise ad2)
        {
            return !(ad1 == ad2);
        }
    }
}
