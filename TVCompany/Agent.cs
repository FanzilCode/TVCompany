using System;
using System.Collections.Generic;
using System.Text;

namespace TVCompany
{
    class Agent
    {
        // полное имя
        public string fullname { get; private set; }
        // номер телефона
        public string phone { get; private set; }
        // зарплата
        public double wage { get; private set; }
        // конструктор с 2-мя параметрами
        public Agent(string fullname, string phone)
        {
            this.fullname = fullname;
            this.phone = phone;
            wage = 0;
        }
        // конструктор для чтения из файла
        public Agent(string[] arr)
        {
            fullname = arr[0];
            phone = arr[1];
            wage = 0;
        }
        // переопределение метода ToString() для сохранения в файл
        public override string ToString()
        {
            return $"{fullname}%{phone}";
        }
        // метод Print() для печати информации об агенте на экран
        public void Print()
        {
            if (wage != 0)
                Console.WriteLine($"ФИО: {fullname}\n" +
                    $"Номер телефона: {phone}\n" +
                    $"Зарплата: {wage} руб.");
            else
                Console.WriteLine($"ФИО: {fullname}\n" +
                $"Номер телефона: {phone}\n");
        }
        public void AddWage(double wage)
        {
            this.wage += wage;
        }
        public static bool operator == (Agent agent1, Agent agent2)
        {
            return (agent1.fullname == agent2.fullname && agent1.phone == agent2.phone);
        }
        public static bool operator !=(Agent agent1, Agent agent2)
        {
            return !(agent1 == agent2);
        }
    }
}
