using System;
using System.Collections.Generic;


public class DatabaseQueryManager
{

    public List<DatabaseQuery> Queries = new()
    {

        new DatabaseQuery(
            "Список клиентов",
            @"SELECT 
                ClientID,
                OrganizationName,
                INN,
                Phone,
                Email,
                ClientType
              FROM Client",
            typeof(ClientQueryResult)
        ),



        new DatabaseQuery(
            "Действующие договоры",
            @"SELECT
                ContractID,
                ClientID,
                Number,
                DateStart,
                DateEnd,
                Status
              FROM Contract
              WHERE Status = 'Действует'",
            typeof(ContractQueryResult)
        ),



        new DatabaseQuery(
            "Заявки высокого приоритета",
            @"SELECT
                ApplicationID,
                ClientID,
                ServiceID,
                EmployeeID,
                DateCreated,
                Status,
                Priority
            FROM Application
              WHERE Priority = 'Высокий'",
            typeof(ApplicationQueryResult)
        ),



        new DatabaseQuery(
            "Все оборудование клиентов",
            @"SELECT
                EquipmentID,
                ClientID,
                Type,
                Model,
                SerialNumber,
                Condition
              FROM Equipment",
            typeof(EquipmentQueryResult)
        ),



        new DatabaseQuery(
            "Счета по договорам",
            @"SELECT
                InvoiceID,
                ContractID,
                DateIssued,
                Amount,
                Status,
                Comment
              FROM Invoice",
            typeof(InvoiceQueryResult)
        ),



        new DatabaseQuery(
            "Платежи по счетам",
            @"SELECT
                PaymentID,
                InvoiceID,
                DatePayment,
                Amount,
                Method,
                Confirmation
              FROM Payment",
            typeof(PaymentQueryResult)
        ),



        new DatabaseQuery(
            "Сотрудники отдела",
            @"SELECT
                EmployeeID,
                FullName,
                Position,
                Department,
                Phone,
                Email
              FROM Employee",
            typeof(EmployeeQueryResult)
        ),



        new DatabaseQuery(
            "Количество заявок",
            @"SELECT
                COUNT(*) AS ApplicationsCount
              FROM Application",
            typeof(CountQueryResult)
        ),



        new DatabaseQuery(
            "Общая сумма платежей",
            @"SELECT
                SUM(CAST(Amount AS REAL)) AS TotalPayments
              FROM Payment",
            typeof(SumQueryResult)
        )

    };



    public List<string> GetNames()
    {
        List<string> names = new();


        foreach (DatabaseQuery query in Queries)
        {
            names.Add(query.Name);
        }


        return names;
    }
}