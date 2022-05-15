using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINPCHealthScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static UINPCHealthScript instance {get; private set;}

    public Image mask;
    float startSize;
    void Awake(){
        instance = this;
    }
    void Start()
    {
        startSize = mask.rectTransform.rect.width;  
    }

    // Update is called once per frame
    public void breytaGildi(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, startSize * value);
    }
}
