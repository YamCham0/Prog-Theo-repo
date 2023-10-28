using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingTextEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private Color[] colors; // SkyBlue, Blue, Purple, Pink

    private int currentColorIndex = 0;
    private int currentDotCount = 0;

    private void Start()
    {
        Debug.Log("Initial Text: " + loadingText.text);
        Debug.Log("Initial Color: " + loadingText.color);
        StartCoroutine(ColorChangeEffect());
        StartCoroutine(DotsEffect());
    }

    IEnumerator ColorChangeEffect()
{
    while (true)
    {
        Debug.Log("Current color index: " + currentColorIndex);
        Color newColor = colors[currentColorIndex];
        Debug.Log("Changing color to: " + newColor);

        // Make sure the alpha is set to 1 (completely opaque)
        newColor.a = 1.0f;

        loadingText.color = newColor;
        
        Debug.Log("After setting, color is: " + loadingText.color);

        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        yield return new WaitForSeconds(0.25f);
    }
}


    IEnumerator DotsEffect()
    {
        while (true)
        {
            string dots = new string('.', currentDotCount);
            loadingText.text = "Loading" + dots;
            currentDotCount = (currentDotCount + 1) % 4;
            yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds before changing the number of dots
        }
    }
}