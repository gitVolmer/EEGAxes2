using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIList : MonoBehaviour
{
    public GameObject uiTextPrefab;
    public int numberOfElements = 10;
    public float spacing = 30f;

    private List<GameObject> spawnedElements = new List<GameObject>();

    private void Start()
    {
        SpawnUIElements();
    }

    private void SpawnUIElements()
    {
        for (int i = 0; i < numberOfElements; i++)
        {
            GameObject uiElement = Instantiate(uiTextPrefab, transform);
            uiElement.GetComponent<Text>().text = "Element " + (i + 1).ToString();
            uiElement.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -i * spacing);
            spawnedElements.Add(uiElement);
        }
    }
}