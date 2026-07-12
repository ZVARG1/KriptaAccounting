using UnityEngine;
using System.Collections.Generic;
using SQLite;
using System;

public class DatabaseHandler
{
    private SQLiteConnection _connection;


    public DatabaseHandler()
    {
        string dbPath =
            System.IO.Path.Combine(
                UnityEngine.Application.persistentDataPath,
                "KriptaDatabase.db"
            );


        if (!System.IO.File.Exists(dbPath))
        {
            string source =
                System.IO.Path.Combine(
                    UnityEngine.Application.streamingAssetsPath,
                    "KriptaDatabase.db"
                );


            System.IO.File.Copy(
                source,
                dbPath
            );


            Debug.Log("База скопирована из StreamingAssets");
        }


        _connection =
            new SQLiteConnection(dbPath);


        _connection.CreateTable<Client>();
        _connection.CreateTable<Service>();
        _connection.CreateTable<Employee>();
        _connection.CreateTable<Application>();
        _connection.CreateTable<Contract>();
        _connection.CreateTable<Equipment>();
        _connection.CreateTable<Invoice>();
        _connection.CreateTable<Payment>();


        Debug.Log(
            "Подключена БД: " + dbPath
        );
    }

    public List<T> GetAll<T>() where T : new()
    {
        return _connection.Table<T>().ToList();
    }


    public void SeedDatabase()
    {
        if (_connection.Table<Client>().Count() > 0)
            return;


        // =========================
        // Клиенты
        // =========================

        _connection.InsertAll(new List<Client>
    {
        new Client
        {
            OrganizationName = "ООО ТехноМир",
            INN = "7701234567",
            Phone = "+79001112233",
            Email = "info@tehnomir.ru",
            ClientType = "Юридическое лицо"
        },

        new Client
        {
            OrganizationName = "ИП Иванов",
            INN = "7707654321",
            Phone = "+79002223344",
            Email = "ivanov@mail.ru",
            ClientType = "ИП"
        },

        new Client
        {
            OrganizationName = "ООО АльфаСофт",
            INN = "7703456789",
            Phone = "+79003334455",
            Email = "alpha@mail.ru",
            ClientType = "Юридическое лицо"
        },

        new Client
        {
            OrganizationName = "ООО БайтСервис",
            INN = "7704567890",
            Phone = "+79004445566",
            Email = "byte@mail.ru",
            ClientType = "Юридическое лицо"
        },

        new Client
        {
            OrganizationName = "ИП Петров",
            INN = "7705678901",
            Phone = "+79005556677",
            Email = "petrov@mail.ru",
            ClientType = "ИП"
        }
    });



        // =========================
        // Услуги
        // =========================

        _connection.InsertAll(new List<Service>
    {
        new Service
        {
            ServiceName = "Настройка 1С",
            Direction = "1С",
            Cost = "15000",
            Periodicity = "Разовая",
            Department = "ИТ"
        },

        new Service
        {
            ServiceName = "Обслуживание серверов",
            Direction = "Инфраструктура",
            Cost = "25000",
            Periodicity = "Ежемесячно",
            Department = "ИТ"
        },

        new Service
        {
            ServiceName = "Разработка ПО",
            Direction = "Разработка",
            Cost = "50000",
            Periodicity = "Разовая",
            Department = "Разработка"
        },

        new Service
        {
            ServiceName = "Техническая поддержка",
            Direction = "Поддержка",
            Cost = "10000",
            Periodicity = "Ежемесячно",
            Department = "Сервис"
        },

        new Service
        {
            ServiceName = "Аудит системы",
            Direction = "Безопасность",
            Cost = "30000",
            Periodicity = "Разовая",
            Department = "Аналитика"
        }
    });



        // =========================
        // Сотрудники
        // =========================

        _connection.InsertAll(new List<Employee>
    {
        new Employee
        {
            FullName = "Иванов Сергей",
            Position = "Программист",
            Department = "Разработка",
            Phone = "+79990001122",
            Email = "ivanov@company.ru"
        },

        new Employee
        {
            FullName = "Петров Алексей",
            Position = "Администратор",
            Department = "ИТ",
            Phone = "+79990002233",
            Email = "petrov@company.ru"
        },

        new Employee
        {
            FullName = "Сидорова Анна",
            Position = "Аналитик",
            Department = "Аналитика",
            Phone = "+79990003344",
            Email = "sidorova@company.ru"
        },

        new Employee
        {
            FullName = "Кузнецов Дмитрий",
            Position = "Инженер",
            Department = "Сервис",
            Phone = "+79990004455",
            Email = "kuznetsov@company.ru"
        },

        new Employee
        {
            FullName = "Орлова Мария",
            Position = "Менеджер",
            Department = "Продажи",
            Phone = "+79990005566",
            Email = "orlova@company.ru"
        }
    });



        // =========================
        // Заявки
        // =========================

        _connection.InsertAll(new List<Application>
    {
        new Application
        {
            ClientID = 1,
            ServiceID = 1,
            EmployeeID = 1,
            DateCreated = DateTime.Now.AddDays(-10),
            Status = "Новая",
            Priority = "Высокий"
        },

        new Application
        {
            ClientID = 2,
            ServiceID = 2,
            EmployeeID = 2,
            DateCreated = DateTime.Now.AddDays(-8),
            Status = "В работе",
            Priority = "Средний"
        },

        new Application
        {
            ClientID = 3,
            ServiceID = 3,
            EmployeeID = 3,
            DateCreated = DateTime.Now.AddDays(-5),
            Status = "Завершена",
            Priority = "Низкий"
        },

        new Application
        {
            ClientID = 4,
            ServiceID = 4,
            EmployeeID = 4,
            DateCreated = DateTime.Now.AddDays(-3),
            Status = "В работе",
            Priority = "Высокий"
        },

        new Application
        {
            ClientID = 5,
            ServiceID = 5,
            EmployeeID = 5,
            DateCreated = DateTime.Now,
            Status = "Новая",
            Priority = "Средний"
        }
    });



        // =========================
        // Договоры
        // =========================

        _connection.InsertAll(new List<Contract>
    {
        new Contract
        {
            ClientID = 1,
            Number = "DOG-001",
            DateStart = DateTime.Now.AddMonths(-6),
            DateEnd = DateTime.Now.AddMonths(6),
            Status = "Действует"
        },

        new Contract
        {
            ClientID = 2,
            Number = "DOG-002",
            DateStart = DateTime.Now.AddMonths(-3),
            DateEnd = DateTime.Now.AddMonths(9),
            Status = "Действует"
        },

        new Contract
        {
            ClientID = 3,
            Number = "DOG-003",
            DateStart = DateTime.Now.AddYears(-1),
            DateEnd = DateTime.Now.AddMonths(-1),
            Status = "Завершён"
        },

        new Contract
        {
            ClientID = 4,
            Number = "DOG-004",
            DateStart = DateTime.Now,
            DateEnd = DateTime.Now.AddYears(1),
            Status = "Действует"
        },

        new Contract
        {
            ClientID = 5,
            Number = "DOG-005",
            DateStart = DateTime.Now,
            DateEnd = DateTime.Now.AddMonths(8),
            Status = "Действует"
        }
    });



        // =========================
        // Оборудование
        // =========================

        _connection.InsertAll(new List<Equipment>
    {
        new Equipment
        {
            ClientID = 1,
            Type = "Сервер",
            Model = "Dell PowerEdge",
            SerialNumber = "SRV001",
            Condition = "Исправно"
        },

        new Equipment
        {
            ClientID = 2,
            Type = "ПК",
            Model = "HP ProDesk",
            SerialNumber = "PC002",
            Condition = "Исправно"
        },

        new Equipment
        {
            ClientID = 3,
            Type = "Ноутбук",
            Model = "Lenovo ThinkPad",
            SerialNumber = "NB003",
            Condition = "Ремонт"
        },

        new Equipment
        {
            ClientID = 4,
            Type = "Сервер",
            Model = "HP Server",
            SerialNumber = "SRV004",
            Condition = "Исправно"
        },

        new Equipment
        {
            ClientID = 5,
            Type = "Принтер",
            Model = "Canon",
            SerialNumber = "PR005",
            Condition = "Исправно"
        }
    });



        // =========================
        // Счета
        // =========================

        _connection.InsertAll(new List<Invoice>
    {
        new Invoice
        {
            ContractID = 1,
            DateIssued = DateTime.Now.AddDays(-20),
            Amount = "15000",
            Status = "Оплачен",
            Comment = "Январь"
        },

        new Invoice
        {
            ContractID = 2,
            DateIssued = DateTime.Now.AddDays(-15),
            Amount = "25000",
            Status = "Оплачен",
            Comment = "Февраль"
        },

        new Invoice
        {
            ContractID = 3,
            DateIssued = DateTime.Now.AddDays(-10),
            Amount = "50000",
            Status = "Не оплачен",
            Comment = ""
        },

        new Invoice
        {
            ContractID = 4,
            DateIssued = DateTime.Now.AddDays(-5),
            Amount = "10000",
            Status = "Оплачен",
            Comment = ""
        },

        new Invoice
        {
            ContractID = 5,
            DateIssued = DateTime.Now,
            Amount = "30000",
            Status = "Не оплачен",
            Comment = ""
        }
    });



        // =========================
        // Платежи
        // =========================

        _connection.InsertAll(new List<Payment>
    {
        new Payment
        {
            InvoiceID = 1,
            DatePayment = DateTime.Now.AddDays(-18),
            Amount = "15000",
            Method = "Карта",
            Confirmation = "Да"
        },

        new Payment
        {
            InvoiceID = 2,
            DatePayment = DateTime.Now.AddDays(-13),
            Amount = "25000",
            Method = "Счёт",
            Confirmation = "Да"
        },

        new Payment
        {
            InvoiceID = 3,
            DatePayment = DateTime.Now.AddDays(-8),
            Amount = "10000",
            Method = "Карта",
            Confirmation = "Да"
        },

        new Payment
        {
            InvoiceID = 4,
            DatePayment = DateTime.Now.AddDays(-3),
            Amount = "10000",
            Method = "Счёт",
            Confirmation = "Да"
        },

        new Payment
        {
            InvoiceID = 5,
            DatePayment = DateTime.Now,
            Amount = "5000",
            Method = "Карта",
            Confirmation = "Нет"
        }
    });


        Debug.Log("База данных заполнена тестовыми данными.");
    }


    public void AddRecord<T>(T record) where T : new()
    {
        _connection.Insert(record);
    }


    public void DeleteRecord<T>(int id) where T : new()
    {
        _connection.Delete<T>(id);
    }

    public SQLiteConnection GetConnection()
    {
        return _connection;
    }
}