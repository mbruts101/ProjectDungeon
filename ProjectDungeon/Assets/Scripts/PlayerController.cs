using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float dodgeSpeed;
    public bool isDodging;
    public bool isAttacking;
    int dodgeTimer;

    private Rigidbody2D rb;
    private Vector2 direction = new Vector2(); //used for finding the direction the player is facing without rotating player
    private float xMovement;
    private float yMovement;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if(InputManager.AttackButton() && !isAttacking) //check if attacking
        {
            isAttacking = true;
        }
        if(InputManager.DodgeButton() && !isDodging) //check if dodging
        {
            dodgeTimer = 15;
            isDodging = true;
        }


        if (!isDodging && !isAttacking) 
        {
            xMovement = InputManager.MoveHorizontal(); //get horizontal movement
            yMovement = InputManager.MoveVertical();   //get vertical movement
            
            direction = new Vector2(xMovement,yMovement);

            Physics2D.Raycast(transform.position, direction); //make raycast to show facing direction, used for attacking? 
            Debug.DrawRay(transform.position, direction * moveSpeed, Color.green);
        }
    }

    void FixedUpdate()
    {
        if (!isDodging && !isAttacking)
        {
            rb.velocity = direction.normalized * moveSpeed; //move player
        }
        else if(isDodging)
        {
            if (dodgeTimer > 0)
            {
                rb.velocity = direction.normalized * dodgeSpeed; //dodge
                dodgeTimer--;
            }
            else
            {
                isDodging = false; //end dodge
            }
        }
        else if (isAttacking)
        {
            isAttacking = false;
        }
    }
}
