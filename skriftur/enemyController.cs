using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    // Start is called before the first frame update
    internal Transform thisTransform;
    public int npcMaxHealth = 10;
    public int isBoss = 0;
    public GameObject minionPrefab;
 
    // The movement speed of the object
    public float moveSpeed = 2.0f;
 
    // A minimum and maximum time delay for taking a decision, choosing a direction to move in
    public Vector2 decisionTime = new Vector2(1, 4);
    public float spawnTime = 50.0f;
    internal float decisionTimeCount = 0;
 
    // The possible directions that the object can move int, right, left, up, down, and zero for staying in place. I added zero twice to give a bigger chance if it happening than other directions
    internal Vector3[] moveDirections = new Vector3[] { Vector3.right, Vector3.left, Vector3.zero, Vector3.zero};
    internal int currentMoveDirection;
    public int bossHealth{get {return health;}}
    int health;
    float teljari;
    int ekkiAftur;
    public int isdead{get {return isDead;}}
    int isDead;

    Rigidbody2D rigidbody2d;
 
    // Use this for initialization
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        // Cache the transform for quicker access
        thisTransform = this.transform;

        health = npcMaxHealth;
        ekkiAftur = 0;
 
        // Set a random time delay for taking a decision ( changing direction, or standing in place for a while )
        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);
 
        teljari = -1;
        //isDead= 0;
    
        // Choose a movement direction, or stay in place
        ChooseMoveDirection();
    }
 
    // Update is called once per frame
    void Update()
    {
       
        // Move the object in the chosen direction at the set speed
        thisTransform.position += moveDirections[currentMoveDirection] * Time.deltaTime * moveSpeed;
 
        if (decisionTimeCount > 0) decisionTimeCount -= Time.deltaTime;
        else
        {
            // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);
 
            // Choose a movement direction, or stay in place
            ChooseMoveDirection();
            //Debug.Log(currentMoveDirection);
        }
        if(teljari > -1){ //Þetta update fall sér um hvenær á að fela tal boxið aftur en ég stilli tíman í public breytuni timeDisplay
        teljari -= Time.deltaTime;
        Debug.Log(teljari);
            if(teljari < 0){
                if(isBoss == 1){
                    int hveMargir = Mathf.FloorToInt(Random.Range(5,12));
                    spawnMinions(hveMargir);
                    teljari = spawnTime;
                }
            }
        }
    }
    public void changeHealth(int amount){
        health=health - amount;
        if(health <= 0){
            isBoss= 1;
            Destroy(gameObject);
        }
        if(isBoss == 1){
            UINPCHealthScript.instance.breytaGildi(health/(float)npcMaxHealth);
        }

    }
    void spawnMinions(int fjoldi){
        //Debug.Log("Hann fer inn í siggadotið");
        if(isBoss == 1){
            //Debug.Log("Hann er að bua til sigga");
            for(int i = 0; i < fjoldi; i++){
                float randomStadur = Mathf.FloorToInt(Random.Range(2f,20f));
                GameObject minion = Instantiate(minionPrefab, rigidbody2d.position + Vector2.left * randomStadur + Vector2.right * randomStadur + Vector2.up * 1.0f, Quaternion.identity);
            }
        }
    }
    void ChooseMoveDirection()
    {
        // Choose whether to move sideways or up/down
        currentMoveDirection = Mathf.FloorToInt(Random.Range(0, moveDirections.Length));
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        
        if(obj.gameObject.tag == "areaBlocker"){
            //Debug.Log("Letsog");
            if(currentMoveDirection == 0){
                currentMoveDirection = 1;

            }else{
                currentMoveDirection = 0;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D obj){
        //Debug.Log("Hann skynjar hann");
        playerController adstod = obj.collider.GetComponent<playerController>();
        if(obj.gameObject.tag == "Player"){
            //Debug.Log("Hann þekkir hann");
            adstod.ChangeHealth(-1);
        }
        if(obj.gameObject.tag == "skot"){
            if(isBoss == 1 && ekkiAftur == 0){
                teljari = 10f;
                ekkiAftur = 1;
                Debug.Log("Hann er byrjaður að telja");
            }
        }

    }
}

