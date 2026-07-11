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

        for (int i = 0; i < columnValues.Length; i++)
        {
            bool isLastColumn = i == columnValues.Length - 1;

            // Последняя колонка у строк данных = кнопка
            if (isLastColumn && !isHeader)
            {
                Button btn = Instantiate(buttonPrefab, cellContainer.transform);

                var layout = btn.GetComponent<LayoutElement>() ??
                             btn.gameObject.AddComponent<LayoutElement>();

                layout.preferredWidth = columnWidths[i];
                layout.flexibleWidth = 0;

                var text = btn.GetComponentInChildren<TextMeshProUGUI>();
                if (text != null)
                    text.text = "Удалить";

                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => buttonCallback?.Invoke());

                continue;
            }

            GameObject newCell = Instantiate(cellPrefab, cellContainer.transform);

            var cellLayout = newCell.GetComponent<LayoutElement>() ??
                             newCell.AddComponent<LayoutElement>();

            cellLayout.preferredWidth = columnWidths[i];
            cellLayout.flexibleWidth = 0;

            var cellText = newCell.GetComponentInChildren<TextMeshProUGUI>();
            if (cellText != null)
                cellText.text = columnValues[i];
        }
    }
}