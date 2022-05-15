using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class byssuSkot : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2D;
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 1000.0f){
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 dir, float force){
        rigidbody2D.AddForce(dir * force);
    }

    void OnCollisionEnter2D(Collision2D obj){
        enemyController e = obj.collider.GetComponent<enemyController>();
        if(e != null ){
            Debug.Log("Virkar að skojata");
            e.changeHealth(1);
        }
        Debug.Log("Virkar");
        Destroy(gameObject);
    }
}
