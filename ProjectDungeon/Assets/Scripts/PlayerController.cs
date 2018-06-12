using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float dodgeSpeed;
    public bool isDodging;
    public bool isAttacking;
    public bool isDead;
    public GameObject deathText;
    public Transform attackPos;
    public float attackRadius;
    public Collider2D[] objectsHit;
    int dodgeTimer;

    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 direction = new Vector2(); //used for finding the direction the player is facing without rotating player
    private float xMovement;
    private float yMovement;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDead)
        {
            if (InputManager.AttackButton() && !isAttacking) //check if attacking
            {
                isAttacking = true;
            }
            if (InputManager.DodgeButton() && !isDodging) //check if dodging
            {
                dodgeTimer = 10;
                isDodging = true;
                anim.SetBool("isDodging", true);
            }


            if (!isDodging && !isAttacking)
            {


                xMovement = InputManager.MoveHorizontal(); //get horizontal movement
                yMovement = InputManager.MoveVertical();   //get vertical movement
                if (xMovement != 0 || yMovement != 0)
                {
                    anim.SetBool("isWalking", true);
                    anim.SetFloat("xInput", xMovement);
                    anim.SetFloat("yInput", yMovement);
                }
                else
                {
                    anim.SetBool("isWalking", false);
                }
                direction = new Vector2(xMovement, yMovement);

                Physics2D.Raycast(transform.position, direction); //make raycast to show facing direction, used for attacking? 
                Debug.DrawRay(transform.position, direction * moveSpeed, Color.green);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        }
        else
        {
            deathText.SetActive(true);
            rb.velocity = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            if (!isDodging && !isAttacking)
            {
                rb.velocity = direction.normalized * moveSpeed; //move player
            }
            else if (isDodging)
            {
                if (dodgeTimer > 0)
                {
                    rb.velocity = direction.normalized * dodgeSpeed; //dodge
                    dodgeTimer--;
                }
                else
                {
                    isDodging = false; //end dodge
                    anim.SetBool("isDodging", false);
                }
            }
            else if (isAttacking)
            {
                Physics2D.OverlapCircleNonAlloc(attackPos.position, attackRadius, objectsHit);
                Debug.Log("Attacking");
                if(objectsHit.Length > 0)
                {
                    foreach(Collider2D hit in objectsHit)
                    {
                        Debug.Log("Found objects");
                        Debug.Log(hit.gameObject.name);
                        if(hit.gameObject.tag == "Enemy")
                        {
                            Destroy(hit.gameObject);
                        }
                    }
                }
                isAttacking = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPos.position, attackRadius);
    }
}
