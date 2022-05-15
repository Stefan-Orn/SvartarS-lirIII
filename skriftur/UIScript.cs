using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIScript instance {get; private set;}

    public Image mask;
    float startSize;
    void Awake(){
        instance = this;
    }
    void Start()
    {
        startSize = mask.rectTransform.rect.width;  
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
    }

    // Update is called once per frame
    public void breytaGildi(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, startSize * value);
    }
}
