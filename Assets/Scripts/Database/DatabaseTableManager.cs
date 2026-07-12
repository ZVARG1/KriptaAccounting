using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;
using System.Reflection;
using UnityEngine.UI;


public class DatabaseTableManager : MonoBehaviour
{
    private DatabaseHandler _db;

    private RawSQLHandler rawSQL;

    private DatabaseQueryManager queryManager;


    [SerializeField] private DynamicRow rowPrefab;

    [SerializeField] private Transform tableContent;

    [SerializeField] private TMP_Dropdown mainDropdown;


    [SerializeField] private Button addingButton;

    [SerializeField] private EditForm editForm;

    [SerializeField] private GameObject editPanel;



    private enum DropdownMode
    {
        Tables,
        Queries
    }


    private DropdownMode currentMode;

    // текущая выбранная таблица
    private Type currentTableType;



    private void Start()
    {
        _db = new DatabaseHandler();

        _db.SeedDatabase();


        rawSQL = new RawSQLHandler(
    _db.GetConnection()
);

        queryManager = new DatabaseQueryManager();



        currentMode = DropdownMode.Tables;


        mainDropdown.onValueChanged.AddListener(
            OnDropdownValueChanged
        );


        FillDropdown();


        OnDropdownValueChanged(0);
    }




    public void OnDropdownValueChanged(int index)
    {

        if (currentMode == DropdownMode.Queries)
        {
            ShowQuery(index);
            return;
        }



        switch (index)
        {

            case 0:

                currentTableType = typeof(Client);

                ShowTable<Client>(
                    new[]
                    {
                        "Код",
                        "Название",
                        "ИНН",
                        "Телефон",
                        "Email",
                        "Тип"
                    },
                    new float[]
                    {
                        50,200,100,120,150,150
                    });

                break;



            case 1:

                currentTableType = typeof(Service);

                ShowTable<Service>(
                    new[]
                    {
                        "Код",
                        "Название",
                        "Направление",
                        "Стоимость",
                        "Период",
                        "Отдел"
                    },
                    new float[]
                    {
                        50,200,120,100,100,120
                    });

                break;



            case 2:

                currentTableType = typeof(Employee);

                ShowTable<Employee>(
                    new[]
                    {
                        "Код",
                        "ФИО",
                        "Должность",
                        "Отдел",
                        "Телефон",
                        "Email"
                    },
                    new float[]
                    {
                        50,200,150,120,120,150
                    });

                break;



            case 3:

                currentTableType = typeof(Application);

                ShowTable<Application>(
                    new[]
                    {
                        "Код",
                        "Клиент",
                        "Услуга",
                        "Сотрудник",
                        "Дата",
                        "Статус",
                        "Приоритет"
                    },
                    new float[]
                    {
                        50,80,80,100,100,100,100
                    });

                break;



            case 4:

                currentTableType = typeof(Contract);

                ShowTable<Contract>(
                    new[]
                    {
                        "Код",
                        "Клиент",
                        "Номер",
                        "Начало",
                        "Конец",
                        "Статус"
                    },
                    new float[]
                    {
                        50,80,120,100,100,100
                    });

                break;



            case 5:

                currentTableType = typeof(Equipment);

                ShowTable<Equipment>(
                    new[]
                    {
                        "Код",
                        "Клиент",
                        "Тип",
                        "Модель",
                        "Серийный номер",
                        "Состояние"
                    },
                    new float[]
                    {
                        50,80,100,150,150,120
                    });

                break;



            case 6:

                currentTableType = typeof(Invoice);

                ShowTable<Invoice>(
                    new[]
                    {
                        "Код",
                        "Договор",
                        "Дата",
                        "Сумма",
                        "Статус",
                        "Комментарий"
                    },
                    new float[]
                    {
                        50,80,100,100,100,150
                    });

                break;



            case 7:

                currentTableType = typeof(Payment);

                ShowTable<Payment>(
                    new[]
                    {
                        "Код",
                        "Счёт",
                        "Дата",
                        "Сумма",
                        "Метод",
                        "Подтверждение"
                    },
                    new float[]
                    {
                        50,80,100,100,120,120
                    });

                break;

        }
    }



    private void ShowTable<T>(
        string[] headers,
        float[] widths)
        where T : new()
    {

        foreach (Transform child in tableContent)
            Destroy(child.gameObject);



        DynamicRow header =
            Instantiate(
                rowPrefab,
                tableContent,
                false
            );


        header.SetupRow(
            headers,
            widths,
            null
        );



        var data = _db.GetAll<T>();


        foreach (var item in data)
        {

            DynamicRow row =
                Instantiate(
                    rowPrefab,
                    tableContent,
                    false
                );


            PropertyInfo[] props =
                typeof(T).GetProperties();



            int id =
                (int)props[0].GetValue(item);



            List<string> values = new();



            foreach (var p in props)
            {
                values.Add(
                    p.GetValue(item)?.ToString()
                );
            }


            row.SetupRow(
                values.ToArray(),
                widths,
                () =>
                {
                    _db.DeleteRecord<T>(id);

                    OnDropdownValueChanged(
                        mainDropdown.value
                    );
                }
            );
        }
    }
    // =========================================
    // ЗАПРОСЫ
    // =========================================

    private void ShowQuery(int index)
    {
        DatabaseQuery query =
            queryManager.Queries[index];

        MethodInfo method =
            GetType().GetMethod(
                nameof(ExecuteAndRenderQuery),
                BindingFlags.NonPublic |
                BindingFlags.Instance
            );

        MethodInfo genericMethod =
            method.MakeGenericMethod(
                query.ResultType
            );

        genericMethod.Invoke(
            this,
            new object[]
            {
            query.SQL
            }
        );
    }




    private void RenderTable(
        List<string[]> rows,
        string[] headers)
    {

        foreach (Transform child in tableContent)
            Destroy(child.gameObject);



        float[] widths =
            new float[headers.Length];


        for (int i = 0; i < widths.Length; i++)
            widths[i] = 120;



        DynamicRow header =
            Instantiate(
                rowPrefab,
                tableContent,
                false
            );


        header.SetupRow(
            headers,
            widths,
            null
        );



        foreach (var row in rows)
        {

            DynamicRow newRow =
                Instantiate(
                    rowPrefab,
                    tableContent,
                    false
                );


            newRow.SetupRow(
                row,
                widths,
                null
            );
        }
    }
    private void ExecuteAndRenderQuery<T>(
    string sql)
    where T : new()
    {
        var data =
            rawSQL.ExecuteQuery<T>(sql);

        PropertyInfo[] props =
            typeof(T).GetProperties();

        List<string[]> rows = new();

        foreach (var item in data)
        {
            List<string> values = new();

            foreach (var prop in props)
            {
                values.Add(
                    prop.GetValue(item)?
                        .ToString()
                    ?? ""
                );
            }

            rows.Add(
                values.ToArray()
            );
        }

        string[] headers =
            GetRussianHeaders(
                props
            );

        RenderTable(
            rows,
            headers
        );
    }
    private string[] GetRussianHeaders(
        PropertyInfo[] props)
    {
        Dictionary<string, string> names =
            new()
            {
            { "ClientID", "Код клиента" },
            { "OrganizationName", "Организация" },
            { "INN", "ИНН" },
            { "Phone", "Телефон" },
            { "Email", "Email" },
            { "ClientType", "Тип клиента" },

            { "EmployeeID", "Код сотрудника" },
            { "FullName", "ФИО" },
            { "Position", "Должность" },
            { "Department", "Отдел" },

            { "ApplicationID", "Код заявки" },
            { "ServiceID", "Код услуги" },
            { "DateCreated", "Дата создания" },
            { "Priority", "Приоритет" },

            { "ContractID", "Код договора" },
            { "Number", "Номер" },
            { "DateStart", "Дата начала" },
            { "DateEnd", "Дата окончания" },

            { "EquipmentID", "Код оборудования" },
            { "Type", "Тип" },
            { "Model", "Модель" },
            { "SerialNumber", "Серийный номер" },
            { "Condition", "Состояние" },

            { "InvoiceID", "Код счёта" },
            { "DateIssued", "Дата выставления" },
            { "Amount", "Сумма" },
            { "Comment", "Комментарий" },

            { "PaymentID", "Код платежа" },
            { "DatePayment", "Дата платежа" },
            { "Method", "Метод оплаты" },
            { "Confirmation", "Подтверждение" },

            { "ApplicationsCount", "Количество заявок" },
            { "TotalPayments", "Общая сумма платежей" }
            };

        string[] result =
            new string[props.Length];

        for (int i = 0; i < props.Length; i++)
        {
            result[i] =
                names.TryGetValue(
                    props[i].Name,
                    out string value
                )
                ? value
                : props[i].Name;
        }

        return result;
    }




    // =========================================
    // РЕЖИМЫ
    // =========================================


    public void OnClickTablesMode()
    {
        currentMode = DropdownMode.Tables;

        currentTableType = typeof(Client);

        addingButton.gameObject.SetActive(true);

        mainDropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);

        FillDropdown();

        mainDropdown.value = 0;

        mainDropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        OnDropdownValueChanged(0);
    }



    public void OnClickQueriesMode()
    {
        currentMode = DropdownMode.Queries;

        currentTableType = null;

        addingButton.gameObject.SetActive(false);

        mainDropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);

        FillDropdown();

        mainDropdown.value = 0;

        mainDropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        ShowQuery(0);
    }




    private void FillDropdown()
    {

        mainDropdown.ClearOptions();


        List<string> options;


        if (currentMode == DropdownMode.Tables)
        {

            options = new()
            {
                "Клиент",
                "Услуга",
                "Сотрудник",
                "Заявка",
                "Договор",
                "Оборудование",
                "Счёт",
                "Платёж"
            };

        }
        else
        {
            options =
                queryManager.GetNames();
        }



        mainDropdown.AddOptions(options);

        mainDropdown.RefreshShownValue();
    }




    // =========================================
    // ДОБАВЛЕНИЕ ЗАПИСЕЙ
    // =========================================


    public void OnClickEditingForm()
    {

        if (currentTableType == null)
            return;


        if (currentTableType == typeof(Client))
            editForm.BuildForm<Client>();

        else if (currentTableType == typeof(Service))
            editForm.BuildForm<Service>();

        else if (currentTableType == typeof(Employee))
            editForm.BuildForm<Employee>();

        else if (currentTableType == typeof(Application))
            editForm.BuildForm<Application>();

        else if (currentTableType == typeof(Contract))
            editForm.BuildForm<Contract>();

        else if (currentTableType == typeof(Equipment))
            editForm.BuildForm<Equipment>();

        else if (currentTableType == typeof(Invoice))
            editForm.BuildForm<Invoice>();

        else if (currentTableType == typeof(Payment))
            editForm.BuildForm<Payment>();


        editPanel.SetActive(true);
    }




    public void SaveRecord()
    {

        if (currentTableType == typeof(Client))
            _db.AddRecord(
                editForm.CreateObject<Client>()
            );


        else if (currentTableType == typeof(Service))
            _db.AddRecord(
                editForm.CreateObject<Service>()
            );


        else if (currentTableType == typeof(Employee))
            _db.AddRecord(
                editForm.CreateObject<Employee>()
            );


        else if (currentTableType == typeof(Application))
            _db.AddRecord(
                editForm.CreateObject<Application>()
            );


        else if (currentTableType == typeof(Contract))
            _db.AddRecord(
                editForm.CreateObject<Contract>()
            );


        else if (currentTableType == typeof(Equipment))
            _db.AddRecord(
                editForm.CreateObject<Equipment>()
            );


        else if (currentTableType == typeof(Invoice))
            _db.AddRecord(
                editForm.CreateObject<Invoice>()
            );


        else if (currentTableType == typeof(Payment))
            _db.AddRecord(
                editForm.CreateObject<Payment>()
            );


        editPanel.SetActive(false);


        OnDropdownValueChanged(
            mainDropdown.value
        );
    }




    public void CancelEdit()
    {
        editPanel.SetActive(false);
    }



    public void CloseEditPanel()
    {
        editPanel.SetActive(false);
    }
}