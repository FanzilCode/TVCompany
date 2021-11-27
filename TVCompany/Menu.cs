using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TVCompany
{
    class Menu
    {
        public static List<Advertise> advertises = new List<Advertise>();
        public static List<Agent> agents = new List<Agent>();
        public static List<ITVShow> tVShows = new List<ITVShow>();
        public static List<Customer> customers = new List<Customer>();
        // cохранение в файл
        public static void SaveToFile(string path)
        {
            string strings = "";
            foreach (var ad in advertises)
            {
                strings += ad + "\n";
            }
            strings = strings.Trim();
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(strings);
            }
        }
        // чтение из файла
        public static void ReadOnFile(string path)
        {
            string line;
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split("%");
                    ITVShow tVShow;
                    switch(arr[0])
                    {
                        case "Кинофильм":
                            {
                                tVShow = new Movie(arr);
                                break;
                            }
                        case "Новости":
                            {
                                tVShow = new News(arr);
                                break;
                            }
                        default:
                            {
                                tVShow = new Comic(arr);
                                break;
                            }
                    }
                    Customer customer = new Customer(sr.ReadLine().Split("%"));
                    DateTime date = Convert.ToDateTime(sr.ReadLine());
                    int time = Convert.ToInt32(sr.ReadLine());
                    Agent agent = new Agent(sr.ReadLine().Split("%"));
                    // добавляем новую запись о рекламе в список
                    advertises.Add(new Advertise(tVShow, customer, date, time, agent));
                    // если в списке телепередач нет tVShow
                    if (tVShows.IndexOf(tVShow) < 0)
                        // добавляем tVShow в список телепередач
                        tVShows.Add(tVShow);
                    // если в списке заказчиков нет customer
                    if (customers.IndexOf(customer) < 0)
                        // добавляем customer в список заказчиков
                        customers.Add(customer);
                    // если в списке агентов нет agent
                    if (agents.IndexOf(agent) < 0)
                        // добавляем agent в список агентов
                        agents.Add(agent);
                }
            }
        }
        // метод PrintAds() - выведениен на экран списка записей о рекламах
        public static void PrintAds(DateTime time1, DateTime time2)
        {
            // вызов метода Clear() для очистки консоли
            Console.Clear();
            int index = 0;
            foreach (var ad in advertises)
            {
                Console.WriteLine($"Индекс: {index}");
                if (ad.IsAvailable(time1, time2))
                {
                    ad.Print();
                }
                index++;
            }
        }
        // метод PrintTVShows() - выведение на экран списка телепередач
        public static void PrintTVShows()
        {
            // вызов метода Clear() для очистки консоли
            Console.Clear();
            int index = 0;
            foreach(var tVShow in tVShows)
            {
                Console.WriteLine($"Индекс: {index}");
                tVShow.Print();
                index++;
            }
        }
        // метод PrintCustomers() - выведение на экран списка заказчиков
        public static void PrintCustomers()
        {
            // вызов метода Clear() для очистки консоли
            Console.Clear();
            int index = 0;
            foreach (var customer in customers)
            {
                Console.WriteLine($"Индекс: {index}");
                customer.Print();
                index++;
            }
        }
        // метод PrintAgents() - выведение на экран списка агентов
        public static void PrintAgents()
        {
            // вызов метода Clear() для очистки консоли
            Console.Clear();
            int index = 0;
            foreach (var agent in agents)
            {
                Console.WriteLine($"Индекс: {index}");
                agent.Print();
                index++;
            }
        }
        // метод AddAdvertise() - добавление новой записи о рекламе в список
        public static void AddAdvertise()
        {
            Console.WriteLine("Телепередача\nВыберите: 1) Хочу добавить новую телепередачу\t 2) Выбрать из списка?");
            int choise = Convert.ToInt32(Console.ReadLine());
            ITVShow tVShow;
            if (choise == 1)
            {
                tVShow = AddTVShow();
            }
            else
            {
                Console.WriteLine("Выберите передачу(введите индекс):");
                PrintTVShows();
                tVShow = tVShows[Convert.ToInt32(Console.ReadLine())];
            }
            Console.WriteLine("Заказчик\nВыберите: 1) Хочу добавить нового заказчика \t 2) Выбрать из списка?");
            choise = Convert.ToInt32(Console.ReadLine());
            Customer customer;
            if (choise == 1)
            {
                customer = AddCustomer();
            }
            else
            {

                Console.WriteLine("Выберите заказчика(введите индекс):");
                PrintCustomers();
                customer = customers[Convert.ToInt32(Console.ReadLine())];
            }

            Console.Write("Введите дату рекламы: ");
            DateTime date = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Введите продолжительность(в минутах) рекламы: ");
            int time = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Агент\nВыберите: 1) Хочу добавить нового агента \t 2) Выбрать из списка?");
            choise = Convert.ToInt32(Console.ReadLine());
            Agent agent;

            if (choise == 1)
            {
                agent = AddAgent();
            }
            else
            {
                Console.WriteLine("Выберите агента(введите индекс):");
                PrintAgents();
                agent = agents[Convert.ToInt32(Console.ReadLine())];
            }
            advertises.Add(new Advertise(tVShow, customer, date, time, agent));
            PrintAds(DateTime.MinValue, DateTime.MaxValue);
        }
        // метод AddTVShow - добавление новой телепередачи в список
        public static ITVShow AddTVShow()
        {
            Console.WriteLine("Выберите тип телепередачи:\n1) Кинофильм;\t2)Новости;\t3)Юмористический");
            int choise = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите название: ");
            string name = Console.ReadLine();

            Console.Write("Введите стоимость минуты рекламы в данной телепередаче: ");
            double minuteCost = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите рейтинг(в %): ");
            double rating = Convert.ToDouble(Console.ReadLine());
            ITVShow tVShow;
            switch(choise)
            {
                case 1:
                    {
                        tVShow = new Movie(name, minuteCost, rating);
                        if(tVShows.IndexOf(tVShow) < 0)
                            tVShows.Add(tVShow);
                        else
                            Console.WriteLine("Телепередача уже есть в списке.");
                        break;
                    }
                case 2:
                    {
                        tVShow = new News(name, minuteCost, rating);

                        if (tVShows.IndexOf(tVShow) < 0)
                            tVShows.Add(tVShow);
                        else
                            Console.WriteLine("Телепередача уже есть в списке.");
                        break;
                    }
                default:
                    {
                        tVShow = new Comic(name, minuteCost, rating);
                        if (tVShows.IndexOf(tVShow) < 0)
                            tVShows.Add(tVShow);
                        else
                            Console.WriteLine("Телепередача уже есть в списке.");
                        break;
                    }
            }
            PrintTVShows();
            return tVShow;
        }
        // метод AddCustomer() - добавление нового заказчика в список
        public static Customer AddCustomer()
        {
            Console.Write("Введите название: ");
            string name = Console.ReadLine();

            Console.Write("Введите банковские реквизиты: ");
            string bankDetails = Console.ReadLine();

            Console.Write("Введите номер телефона: ");
            string phone = Console.ReadLine();

            Console.Write("Контактное лицо: ");
            string contactPerson = Console.ReadLine();

            Customer customer = new Customer(name, bankDetails, phone, contactPerson);
            if(customers.IndexOf(customer) < 0)
            {
                customers.Add(customer);
                PrintCustomers();
            }
            else
                Console.WriteLine("Заказчик уже есть в списке.");
            return customer;
        }
        // метод AddAgent() - добавление нового агента в список
        public static Agent AddAgent()
        {
            Console.Write("Введите полное имя: ");
            string fullname = Console.ReadLine();

            Console.Write("Введите номер телефона: ");
            string phone = Console.ReadLine();

            Agent agent = new Agent(fullname, phone);
            if(agents.IndexOf(agent) < 0)
            {
                agents.Add(agent);
                PrintAgents();
            }
            else
            {
                Console.WriteLine("Агент уже есть в списке.");
            }
            return agent;
        }
        // метод AdsOfThePeriod(time1, time2, tVShow) - итоги рекламы по передачам за период с time1 по time2
        public static void AdsOfThePeriod(DateTime time1, DateTime time2, ITVShow tVShow)
        {
            Console.WriteLine($"\t\t\tРеклама по передачам с {time1.ToShortDateString()} по {time2.ToShortDateString()}:");
            foreach(var ad in advertises)
            {
                bool k = false;
                if(ad.IsAvailable(time1, time2, tVShow))
                {
                    ad.Print();
                    k = true;
                }
                if (!k)
                {
                    Console.WriteLine("Ничего не найдено.");
                }
            }
        }
        // метод AdsOfThePeriod(time1, time2, customer) - итоги рекламы по заказчикам за период с time1 по time2
        public static void AdsOfThePeriod(DateTime time1, DateTime time2, Customer customer)
        {
            Console.WriteLine($"\t\t\tРеклама по заказчикам с {time1.ToShortDateString()} по {time2.ToShortDateString()}:");
            foreach (var ad in advertises)
            {
                bool k = false;
                if (ad.IsAvailable(time1, time2, customer))
                {
                    ad.Print();
                    k = true;
                }
                if (!k)
                {
                    Console.WriteLine("Ничего не найдено.");
                }
            }
        }
        // метод AdsOfThePeriod(time1, time2, customer) - итоги рекламы по агентам за период с time1 по time2
        public static void AdsOfThePeriod(DateTime time1, DateTime time2, Agent agent)
        {
            Console.WriteLine($"\t\t\tРеклама по агентам с {time1.ToShortDateString()} по {time2.ToShortDateString()}:");
            foreach (var ad in advertises)
            {
                bool k = false;
                if (ad.IsAvailable(time1, time2, agent))
                {
                    ad.Print();
                    k = true;
                }
                if (!k)
                {
                    Console.WriteLine("Ничего не найдено.");
                }
            }
        }
        // метод AdsOfThePeriod(time1, time2, tvShowsWithoutAds) - телепередачи без рекламы за период
        public static void AdsOfThePeriod(DateTime time1, DateTime time2, List<ITVShow> tVShows)
        {
            bool k = false;
            foreach(var show in tVShows)
            {
                Console.WriteLine($"\t\t\tТелепередачи без рекламы за период с {time1.ToShortDateString()} по {time2.ToShortDateString()}");
                if (!advertises.Any(x => x.tVShow == show))
                {
                    show.Print();
                    k = true;
                }
            }
            if(!k)
            {
                Console.WriteLine("Ничего не найдено.");
            }
        }
        // метод AdsOfThePeriod(time1, time2, customersWithoutAds) - заказчики без заказов за период
        public static void AdsOfThePeriod(DateTime time1, DateTime time2, List<Customer> customers)
        {
            Console.WriteLine($"\t\t\tЗаказчики без заказов за период {time1.ToShortDateString()} по {time2.ToShortDateString()}");
            bool k = false;
            foreach(var customer in customers)
            {
                if(!advertises.Any(x => x.customer == customer))
                {
                    k = true;
                    customer.Print();
                }
            }
            if (!k)
            {
                Console.WriteLine("Ничего не найдено.");
            }
        }
        // метод AdsOfThePeriod(time1, time2, List<Agent> agents) - агенты без заказов за период
        public static void AdsOfThePeriod(DateTime time1, DateTime time2, List<Agent> agents)
        {
            Console.WriteLine($"\t\t\tАгенты без заказов за период {time1.ToShortDateString()} по {time2.ToShortDateString()}");
            bool k = false;
            foreach (var agent in agents)
            {
                if (!advertises.Any(x => x.agent == agent))
                {
                    k = true;
                    agent.Print();
                }
            }
            if (!k)
            {
                Console.WriteLine("Ничего не найдено.");
            }
        }
        // метод PayOfThePeriod(time1, time2) - зарплата агентов за период
        public static void PayOfThePeriod(DateTime time1, DateTime time2)
        {
            Console.WriteLine($"\t\t\tЗарплата агентов за период с {time1.ToShortDateString()} по {time2.ToShortDateString()}");
            foreach(var agent in agents)
            {
                foreach(var ad in advertises)
                {
                    if(ad.agent == agent)
                    {
                        agent.AddWage(ad.GetCost() * 0.1); // 10% с каждой сделки
                    }
                }
                agent.Print();
            }
        }

        static void Main(string[] args)
        {
            string path = @"Ads.txt";
            ReadOnFile(path);
            int choise = 1;
            while(choise >= 1 && choise <= 5)
            {
                Console.WriteLine("\t\t\tГлавная\nВыберите:");
                Console.WriteLine("1) Реклама\t2) Передачи\t3) Заказчики\t4) Агенты\t5) Отчёты\t6) Выход");
                choise = Convert.ToInt32(Console.ReadLine());
                switch(choise)
                {
                    case 1:
                        {
                            Console.WriteLine("\t\t\tРеклама\nВыберите:");
                            Console.WriteLine("1) Посмотреть все отчёты по рекламам\n" +
                                "2) Посмотреть отчёты по рекламам за период\n3) Добавить отчёт по рекламе\n4) На главную");
                            int choise2 = Convert.ToInt32(Console.Read());
                            if (advertises.Count == 0)
                            {
                                Console.WriteLine("Список отчётов пуст");
                            }
                            switch (choise2)
                            {
                                case 1:
                                    {    
                                        PrintAds(DateTime.MinValue, DateTime.MaxValue);
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.Write("Введите дату, с которой нужно начать отсчет: ");
                                        DateTime date1 = Convert.ToDateTime(Console.ReadLine());
                                        Console.Write("Введите дату, до которой нужно вести отсчет: ");
                                        DateTime date2 = Convert.ToDateTime(Console.ReadLine());
                                        PrintAds(date1, date2);
                                        break;
                                    }
                                case 3:
                                    {
                                        AddAdvertise();
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("\t\t\tПередачи\nВыберите:");
                            Console.WriteLine("1) Получить список передач\n2) Добавить передачу\n3) На главную");
                            int choise2 = Convert.ToInt32(Console.ReadLine());
                            if(choise2 == 1)
                            {
                                if (tVShows.Count == 0)
                                    Console.WriteLine("Спиок передач пуст");
                                else
                                    PrintTVShows();
                            }
                            else if(choise2 == 2)
                            {
                                AddTVShow();
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("\t\t\tЗаказчики\nВыберите:");
                            Console.WriteLine("1) Получить список заказчиков\n2) Добавить заказчика\n3) На главную");
                            int choise2 = Convert.ToInt32(Console.ReadLine());
                            if (choise2 == 1)
                            {
                                if (customers.Count == 0)
                                    Console.WriteLine("Список заказчиков пуст.");
                                else
                                    PrintCustomers();
                            }
                            else if(choise2 == 2)
                            {
                                AddCustomer();
                            }    
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("\t\t\tАгенты:\nВыберите:");
                            Console.WriteLine("1) Получить список агентов\n2) Добавить агента\n3) На главную");
                            int choise2 = Convert.ToInt32(Console.ReadLine());
                            if(choise2 == 1)
                            {
                                if (agents.Count == 0)
                                    Console.WriteLine("Список агентов пуст");
                                else
                                    PrintAgents();
                            }
                            else if(choise2 == 2)
                            {
                                AddAgent();
                            }
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("\t\t\tОтчёты\nВыберите:");
                            Console.WriteLine("1) Итоги по передачам за период\n2) Итоги по заказчикам за период\n" +
                                "3) Итоги по агентам за период\n4) Передачи без рекламы за период\n" +
                                "5) Заказчики без заказов за период\n6) Агенты без заказов за период\n" +
                                "6) Зарплата агентов за период\nНа главную");
                            int choise2 = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Введите дату, с которой нужно начать отсчет: ");
                            DateTime date1 = Convert.ToDateTime(Console.ReadLine());

                            Console.Write("Введите дату, до которой нужно вести отсчет: ");
                            DateTime date2 = Convert.ToDateTime(Console.ReadLine());

                            switch (choise2)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("Выберите передачу(введите индекс):");
                                        PrintTVShows();
                                        int ch = Console.Read();
                                        AdsOfThePeriod(date1, date2, tVShows[ch]);
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.WriteLine("Выберите заказчика(введите индекс):");
                                        PrintCustomers();
                                        int ch = Console.Read();
                                        AdsOfThePeriod(date1, date2, customers[ch]);
                                        break;
                                    }
                                case 3:
                                    {
                                        Console.WriteLine("Выберите агента(введите индекс):");
                                        PrintAgents();
                                        int ch = Console.Read();
                                        AdsOfThePeriod(date1, date2, agents[ch]);
                                        break;
                                    }
                                case 4:
                                    {
                                        AdsOfThePeriod(date1, date2, tVShows);
                                        break;
                                    }
                                case 5:
                                    {
                                        AdsOfThePeriod(date1, date2, customers);
                                        break;
                                    }
                                case 6:
                                    {
                                        AdsOfThePeriod(date1, date2, agents);
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            SaveToFile(path);
        }
    }
}