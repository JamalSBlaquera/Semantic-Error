using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class sliderValue : MonoBehaviour
{
    [SerializeField] protected Slider slider;
    [SerializeField] protected TMP_Text val;
    [SerializeField] protected TMP_Text Textname;
    [SerializeField] protected GameObject SliderContainer;

    private void Start()
    {
        UpdateValue(slider.value);
        slider.onValueChanged.AddListener(UpdateValue);

        Textname.text = SliderContainer.name;
    }

    // Update is called once per frame
    void UpdateValue(float value)
    {
        val.text = slider.value.ToString();
    }
}
