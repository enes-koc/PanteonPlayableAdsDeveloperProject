using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushSizeController : MonoBehaviour
{
    [SerializeField] RectTransform brushHandle;
    public void setUIBrushSize(float sliderValue)
    {
        float normalizedSliderValue = Mathf.Lerp(70f, 150f, (sliderValue - 0.2f)/(1 - 0.2f));
        brushHandle.sizeDelta = new Vector2(normalizedSliderValue, normalizedSliderValue);
    }

}
