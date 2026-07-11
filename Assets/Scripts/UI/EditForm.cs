using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class EditForm : MonoBehaviour
{
    [SerializeField] private DynamicField fieldPrefab;
    [SerializeField] private Transform fieldsContainer;

    private readonly Dictionary<PropertyInfo, DynamicField> _fields
        = new();

    public void BuildForm<T>(T data = default) where T : new()
    {
        foreach (Transform child in fieldsContainer)
            Destroy(child.gameObject);

        _fields.Clear();

        var props = typeof(T).GetProperties();

        foreach (var prop in props)
        {
            if (prop.Name.EndsWith("Id"))
                continue; // ID не редактируем

            DynamicField field =
                Instantiate(fieldPrefab, fieldsContainer);

            string value = "";

            if (data != null)
            {
                object v = prop.GetValue(data);
                value = v?.ToString() ?? "";
            }

            field.Setup(prop.Name, value);

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