using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DynamicRow : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private HorizontalLayoutGroup cellContainer;

    public void SetupRow(string[] columnValues,
                     float[] columnWidths,
                     Action buttonCallback = null)
    {
        foreach (Transform child in cellContainer.transform)
            Destroy(child.gameObject);

        bool isHeader = buttonCallback == null;

        // Создаём все ячейки данных
        for (int i = 0; i < columnValues.Length; i++)
        {
            GameObject newCell =
                Instantiate(
                    cellPrefab,
                    cellContainer.transform
                );

            var layout =
                newCell.GetComponent<LayoutElement>() ??
                newCell.AddComponent<LayoutElement>();

            layout.preferredWidth = columnWidths[i];
            layout.flexibleWidth = 0;

            var text =
                newCell.GetComponentInChildren<TextMeshProUGUI>();

            if (text != null)
                text.text = columnValues[i];
        }

        // Кнопка удаления только для строк данных
        if (!isHeader)
        {
            Button btn =
                Instantiate(
                    buttonPrefab,
                    cellContainer.transform
                );

            var layout =
                btn.GetComponent<LayoutElement>() ??
                btn.gameObject.AddComponent<LayoutElement>();

            layout.preferredWidth = 100;
            layout.flexibleWidth = 0;

            var text =
                btn.GetComponentInChildren<TextMeshProUGUI>();

            if (text != null)
                text.text = "Удалить";

            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => buttonCallback?.Invoke());
        }
    }
}