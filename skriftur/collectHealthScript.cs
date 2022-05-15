using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectHealthScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D obj){
        playerController caller = obj.GetComponent<playerController>();
        if(caller != null){
            if(caller.health != caller.maxHealth){
                caller.ChangeHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
