using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deadToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    float waitTime = 8f;
    float waitTimer;
    void Start()
    {
        waitTimer = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("SKirftan keyrir");
        waitTimer -= Time.deltaTime;
        if(waitTimer < 0){
            //Debug.Log("Tímerinn virkar");
            SceneManager.LoadScene(0);
        }
    }
}
