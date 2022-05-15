using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textDisplay : MonoBehaviour
{
    // Start is called before the first frame update    
    public float timeDisplay = 5.0f;
    public GameObject box;
    public GameObject bossBox;
    float timerDisplay;
    void Start()
    {
        box.SetActive(false); //Default er box(tal boxið) ekki sýnilegt
        bossBox.SetActive(false);
        timerDisplay = -1.0f; //Og teljarinn er -1 (er alveg að null stilla altt)+

    }

    // Update is called once per frame
    void Update()
    {
        if(timerDisplay > -1){ //Þetta update fall sér um hvenær á að fela tal boxið aftur en ég stilli tíman í public breytuni timeDisplay
        timerDisplay -= Time.deltaTime;
            if(timerDisplay < 0){
                box.SetActive(false);
            }
        }

    }

    public void synaBox(int boss){//Þetta er fall sem ég get kallað á í ö'rum scriptum (public) en ég kalla á það til þess að byrta tal boxið hjá froskinum þegar við á 
        if(boss == 1){
            bossBox.SetActive(true);
        }
        timerDisplay = timeDisplay;
        box.SetActive(true);

    }
}
