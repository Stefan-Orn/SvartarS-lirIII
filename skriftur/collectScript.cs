using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D obj){
        //Player controller = obj.GetComponent<Player>();
        playerController caller = obj.GetComponent<playerController>();
        //Debug.Log(controller.jallo);
        //Debug.Log("Jessses");
        if(caller != null){
            //Debug.Log("Veirkar?");  
            //Debug.Log(caller);
            caller.changePoints(1);
            //Debug.Log("Foubleyes");
            Destroy(gameObject);
        }
    }
    
}
