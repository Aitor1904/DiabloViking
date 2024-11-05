using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDetector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private float fadeSpeed = 5f;

    private Color targetColor;
    private Color visibleColor;
    private Color hiddenColor;

    private void Awake()
    {
        visibleColor = itemText.color;
        hiddenColor = new Color(visibleColor.r, visibleColor.g, visibleColor.b, 0);
        itemText.color = hiddenColor;
        targetColor = hiddenColor;
    }

    private void Update()
    {
        if (itemText == null) return;

        itemText.transform.position = Input.mousePosition;
        itemText.color = Color.Lerp(itemText.color, targetColor, fadeSpeed * Time.deltaTime);
    }

    public void ShowItemText(string itemName)
    {
        if (itemText == null) return;
        itemText.text = itemName;
        targetColor = visibleColor; 
    }

    public void HideItemText()
    {
        if (itemText == null) return;
        targetColor = hiddenColor; 
    }
}
