using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using SQLite;

public class EditForm : MonoBehaviour
{
    [SerializeField] private DynamicField fieldPrefab;
    [SerializeField] private Transform fieldsContainer;

    private readonly Dictionary<PropertyInfo, DynamicField> _fields
        = new();

    private readonly Dictionary<string, string> fieldNames = new()
{
    {"ClientID", "Код клиента"},
    {"OrganizationName", "Организация"},
    {"INN", "ИНН"},
    {"Phone", "Телефон"},
    {"Email", "Электронная почта"},
    {"ClientType", "Тип клиента"},

    {"ServiceID", "Код услуги"},
    {"ServiceName", "Название услуги"},
    {"Direction", "Направление"},
    {"Cost", "Стоимость"},
    {"Periodicity", "Периодичность"},
    {"Department", "Отдел"},

    {"EmployeeID", "Код сотрудника"},
    {"FullName", "ФИО"},
    {"Position", "Должность"},

    {"ApplicationID", "Код заявки"},
    {"DateCreated", "Дата создания"},
    {"Status", "Статус"},
    {"Priority", "Приоритет"},

    {"ContractID", "Код договора"},
    {"Number", "Номер договора"},
    {"DateStart", "Дата начала"},
    {"DateEnd", "Дата окончания"},

    {"EquipmentID", "Код оборудования"},
    {"Type", "Тип оборудования"},
    {"Model", "Модель"},
    {"SerialNumber", "Серийный номер"},
    {"Condition", "Состояние"},

    {"InvoiceID", "Код счёта"},
    {"DateIssued", "Дата выставления"},
    {"Amount", "Сумма"},
    {"Comment", "Комментарий"},

    {"PaymentID", "Код платежа"},
    {"DatePayment", "Дата платежа"},
    {"Method", "Способ оплаты"},
    {"Confirmation", "Подтверждение"}
};

    public void BuildForm<T>(T data = default) where T : new()
    {
        foreach (Transform child in fieldsContainer)
            Destroy(child.gameObject);

        _fields.Clear();

        var props = typeof(T).GetProperties();

        foreach (var prop in props)
        {
            bool isPrimaryKey =
    Attribute.IsDefined(
        prop,
        typeof(PrimaryKeyAttribute)
    );

            if (isPrimaryKey)
                continue;// ID не редактируем

            DynamicField field =
                Instantiate(fieldPrefab, fieldsContainer);

            string value = "";

            if (data != null)
            {
                object v = prop.GetValue(data);
                value = v?.ToString() ?? "";
            }

            string displayName =
    fieldNames.TryGetValue(prop.Name, out var name)
        ? name
        : prop.Name;

            field.Setup(displayName, value);


            _fields.Add(prop, field);
        }
    }


    public T CreateObject<T>() where T : new()
    {
        T obj = new();

        foreach (var pair in _fields)
        {
            PropertyInfo prop = pair.Key;
            string value = pair.Value.Value;

            object converted =
                Convert.ChangeType(value, prop.PropertyType);

            prop.SetValue(obj, converted);
        }

        return obj;
    }
}