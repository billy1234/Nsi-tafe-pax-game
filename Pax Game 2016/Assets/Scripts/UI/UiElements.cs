using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[System.Serializable]
public class UiSlider
{
    public delegate float UiFloatDelegate();

    public Slider slider;
    public UiFloatDelegate getValue;

    public void update()
    {
        //slider.value = getValue();
    }
    
   
}

[System.Serializable]
public class UiText
{
    public delegate string UiStringDelegate();

    public Text text;
    public UiStringDelegate getValue;

    public void update()
    {
        text.text = getValue();
    }


}
