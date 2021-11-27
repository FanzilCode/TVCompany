using System;
using System.Collections.Generic;
using System.Text;

namespace TVCompany
{
    class Customer // заказчик
    {
        // название
        public string name { get; private set; }
        // банковские реквизиты
        public string bankDetails { get; private set; }
        // телефон
        public string phone { get; private set; }
        // контактное лицо
        public string contactPerson { get; private set; }
        // конструктор с 4-мя параметрами
        public Customer(string name, string bankDetails, string phone, string contactPerson)
        {
            this.name = name;
            this.bankDetails = bankDetails;
            this.phone = phone;
            this.contactPerson = contactPerson;
        }
        // конструктор для чтения из файла
        public Customer(string[] arr)
        {
            this.name = arr[0];
            this.bankDetails = arr[1];
            this.phone = arr[2];
            this.contactPerson = arr[3];
        }
        // переопределение метода ToString() для записи в файл
        public override string ToString()
        {
            return $"{name}%{bankDetails}%{phone}%{contactPerson}";
        }
        // метод Print для печати информации о заказчике на экран
        public void Print()
        {
            Console.WriteLine($"Название: {name}\n" +
                $"Банковские реквизиты: {bankDetails}\n" +
                $"Номер телефона: {phone}\n" +
                $"Контактное лицо: {contactPerson}\n");
        }
        public static bool operator ==(Customer customer1, Customer customer2)
        {
            return (customer1.name == customer2.name && customer1.bankDetails == customer2.bankDetails
                && customer1.phone == customer2.phone && customer1.contactPerson == customer2.contactPerson);
        }
        public static bool operator !=(Customer customer1, Customer customer2)
        {
            return !(customer1 == customer2);
        }
    }
}
