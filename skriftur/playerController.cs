using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 5;
    public int stigCap = 10;
    public int maxRegen = 15;
  
    public float regenRate = 20.0f;
    public float stickFree = 2.0f;
    public GameObject projectilePrefab;
    public AudioClip skjotaHljod;
    public AudioClip meidastHljod;
    public AudioSource _AudioSource;
    public AudioClip utiHljod;

    int utiOn;
    public AudioClip hellirHljod;
    int hellirOn;
    public AudioClip bossHljod;
    int bossOn;

    public int health{ get { return currentHealth;}}

    public int stig{get {return currentStig;}}

    public int regen{get {return currentRegen;}}
    int currentStig;
    int currentHealth;
    int currentRegen;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    Animator animator;
    AudioSource audioSource;
    Vector2 lookDir = new Vector2(1,0);
    float hlaup;
    float larett;
    float lodrett;

    int lokaBoss;
    float setRegenRate;
    int noRegen;
    int isBosss;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        currentRegen = 15;
        animator = GetComponent<Animator>();
        audioSource= GetComponent<AudioSource>();
        //Debug.Log(enemyController.e.isdead);
        _AudioSource.clip = utiHljod;
        _AudioSource.Play();

        utiOn = 0;
        hellirOn = 0;
        bossOn = 0;

        //lokaBoss = 0;
        setRegenRate = regenRate;
        noRegen = 1;
        isBosss = 0;

    }

    // Update is called once per frame 
    void Update()
    {
        larett = Input.GetAxis("Horizontal");
        lodrett = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(larett, lodrett);
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)){
            lookDir.Set(move.x, move.y);
            hlaup = lookDir.x;
            lookDir.Normalize();
        };
        animator.SetFloat("Look X", lookDir.x);
        animator.SetFloat("Speed", move.magnitude);
        if(currentHealth <= 0){
            SceneManager.LoadScene(4);
        }
        if(isInvincible){
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0){
                isInvincible = false;
            }
        }
        if(Input.GetButtonDown("Fire1")){
            playSound(skjotaHljod);
            launch();
        }
        if(currentRegen == 0){
            noRegen = 1;
        }

        if(noRegen != 1){
            Debug.Log("Hann a að regena");
            setRegenRate -= Time.deltaTime;
            if(setRegenRate < 0){
                Debug.Log("Hann regenaði");
                setRegenRate = regenRate;
                ChangeHealth(1);
            }
        }
        
    }
    void FixedUpdate(){//Hérna er allt teinkt hreyfingum en þá næ ég í staðsetningu ruby með Vector2 dotinu sem gefur mer 2D staðsetningu(x,y)
        //Síðan eru allir þessi "floknu" utreikningar notaðir til þess að hreyfa Ruby á ákveðnum hraða
    }

    public void ChangeHealth(int amount){
        Debug.Log("Hann fer inn í health");
        if(amount < 0){
            Debug.Log("Hann veit að hann er að missa");
            if(isInvincible){
                return;
            }
            //playSound(meidastHljod);
            isInvincible = true;
            invincibleTimer = stickFree;
        }
        //Debug.Log("Jallo er með ");
        Debug.Log(currentRegen);
        if(noRegen != 1){
            currentRegen = Mathf.Clamp(currentRegen + amount, 0, maxRegen);
            UIRegenScript.instance.breytaGildi(currentRegen/(float)maxRegen);
        }else{
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            UIHealthScript.instance.breytaGildi(currentHealth/(float)maxHealth);
        }


    }

        //Debug.Log(currentHealth+"/"+maxHealth
    public void changePoints(int amount){
        //Debug.Log("Jallo komst i changePOint");
        currentStig = Mathf.Clamp(currentStig + amount, 0, stigCap);
        UIScript.instance.breytaGildi(currentStig/(float)stigCap);
    }
    public void playSound(AudioClip clip){ //Þetta fall get ég kallað á til þess að spila hljóð þegar ég tildæmis fæ líf, missi líf eða skít
        audioSource.PlayOneShot(clip);

    }
    void launch(){
        if((int)lookDir.x < 0){
            //Debug.Log("Skoh");
            GameObject byssuKula = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.left * 1.0f + Vector2.up * 1.0f, Quaternion.identity);
            byssuSkot skot = byssuKula.GetComponent<byssuSkot>();
            skot.Launch(lookDir, 1000);
        }else{
            GameObject byssuKula = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.right * 1.0f + Vector2.up * 1.0f, Quaternion.identity);
            byssuSkot skot = byssuKula.GetComponent<byssuSkot>();
            skot.Launch(lookDir, 1000);
        }

        animator.SetTrigger("Launch");
    }

    private void OnTriggerEnter2D(Collider2D obj){

        if(obj.gameObject.tag == "accessTrigger"){
            
            if(currentStig >= stigCap){
                //Debug.Log("Hurðin");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }
            
            noRegen = 0;
            currentRegen = 15;
            //Debug.Log("óvirkandi Hurðin");
        }
        if(obj.gameObject.tag == "utskrift"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if(obj.gameObject.tag == "meiriSkit"){
            textDisplay syna = obj.GetComponent<textDisplay>();
            if(currentStig != stigCap){
                //Debug.Log("Meiri Skít");
                syna.synaBox(0);
            }
            //Debug.Log("Meiri skít en þettta vikrar ekki");
        }
        if(obj.gameObject.tag == "ovinaBar"){
            textDisplay syna = obj.GetComponent<textDisplay>();
            syna.synaBox(1);
            Debug.Log("AUUUUUUU");
        }
        if(obj.gameObject.tag == "iGegn"){
            SceneManager.LoadScene(4);
        }
        if(obj.gameObject.tag == "hellir"){
            utiOn = 0;
            bossOn = 0;
            if(hellirOn != 1){
                _AudioSource.Stop();
                _AudioSource.clip = hellirHljod;
                _AudioSource.Play();
            }
            hellirOn = 1;


        }
        if(obj.gameObject.tag == "uti"){
            hellirOn = 0;
            bossOn = 0;
            if(utiOn != 1){
                _AudioSource.Stop();
                _AudioSource.clip = utiHljod;
                _AudioSource.Play();
            }
            utiOn = 1;

        }
        if(obj.gameObject.tag == "boss"){
            utiOn = 0;
            hellirOn = 0;
            if(bossOn != 1){
                _AudioSource.Stop();
                _AudioSource.clip = bossHljod;
                _AudioSource.Play();
            }
            textDisplay syna = obj.GetComponent<textDisplay>();
            syna.synaBox(1);
            bossOn = 1;
        
            
        }
    }
}
