
    // Start is called before the first frame update
/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using UnityEngine;
using V_AnimationSystem;
using CodeMonkey.Utils;

/*
 * Simple Jump
 * */
public class Player : MonoBehaviour {

    [SerializeField] private LayerMask platformsLayerMask;
    private Player_Base playerBase;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;

    public float jumpVelocity = 20f;
    public float moveSpeed = 5f;
    public float midAirControl = 3f;


    bool isInvincible;
    float invincibleTimer;



    Animator animator;
    public playerController jallo;
    private void Awake() {
        playerBase = gameObject.GetComponent<Player_Base>();
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        jallo = GetComponent<playerController>();
        animator = GetComponent<Animator>();

    }

    private void Update() {
 
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) {
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }

        HandleMovement_FullMidAirControl();
        //HandleMovement_SomeMidAirControl();

        // Set Animations

    }

    private bool IsGrounded() {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    
    private void HandleMovement_FullMidAirControl() {
        if (Input.GetKey(KeyCode.A)) {
            rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
        } else {
            if (Input.GetKey(KeyCode.D)) {
                rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
            } else {
                // No keys pressed
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            }
        }
    }

    private void HandleMovement_SomeMidAirControl() {
        if (Input.GetKey(KeyCode.A)) {
            if (IsGrounded()) {
                rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            } else {
                rigidbody2d.velocity += new Vector2(-moveSpeed * midAirControl * Time.deltaTime, 0);
                rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
            }
        } else {
            if (Input.GetKey(KeyCode.D)) {
                if (IsGrounded()) {
                    rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                } else {
                    rigidbody2d.velocity += new Vector2(+moveSpeed * midAirControl * Time.deltaTime, 0);
                    rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
                }
            } else {
                // No keys pressed
                if (IsGrounded()) {
                    rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                }
            }
        }
    }

    private void HandleMovement_NoMidAirControl() {
        if (IsGrounded()) {
            if (Input.GetKey(KeyCode.A)) {
                rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            } else {
                if (Input.GetKey(KeyCode.D)) {
                    rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                } else {
                    // No keys pressed
                    rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                }
            }
        }
    }

}

