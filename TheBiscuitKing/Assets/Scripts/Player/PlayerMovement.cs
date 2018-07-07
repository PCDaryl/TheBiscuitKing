/* File: PlayerMovement.cs
 * Author: D-Wings
 * 
 * Objective: This Script will control the player's movement -- the code here may eventually be assimilated into a script called, "PlayerController".
 * 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float moveSpeed = 0.0f,
                  jumpForce = 0.0f;

    private int speedModifier = 10,
                jumpForceModifier = 10;

    private bool canJump = true;

    private Animator animr;

    private Rigidbody2D playerRb;

	// Use this for initialization
	void Start ()
    {
        animr = this.GetComponent<Animator>();
        playerRb = this.GetComponent<Rigidbody2D>();
	}
    
    private void FixedUpdate()
    {
        PlayerControlls();
    }
    
    private void PlayerControlls()
    {
        //CommitChanges
        SetPlayerMovement();

        //BasicMovement
        if (Input.GetAxis("Horizontal") > 0.0f)
            this.moveSpeed = 10.0f;
        else if (Input.GetAxis("Horizontal") < 0.0f)
            this.moveSpeed = -10.0f;
        else
            this.moveSpeed = 0.0f;

        //SpeedModifier
        if (Input.GetKey(KeyCode.LeftShift))
            SetSpeedModifier(2);
        else
            SetSpeedModifier(1);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && this.canJump)
        {
            this.jumpForce = 10.0f;
            SetCanJump(false);
        }
            
        else
            this.jumpForce = 0.0f;

        //SetAnimrParameters
        animr.SetFloat("movement", moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
            SetCanJump(true);
    }

    private void SetPlayerMovement()
    {
        //if (moveSpeed == 0.0f && jumpForce == 0.0f)
        //    this.playerRb.Sleep();
        //else
        //{
            Vector2 playerMovement = new Vector2(moveSpeed * speedModifier, jumpForce *jumpForceModifier);
            playerRb.AddForce(playerMovement);
        //}

       

    }

    public void SetSpeedModifier(int speedMod)
    {
        this.speedModifier = speedMod;
    }

    public void SetJumpForceModifier(int jumpForceModifier)
    {
        this.jumpForceModifier = jumpForceModifier;
    }

    public void SetCanJump(bool canJump)
    {
        this.canJump = canJump;
    }
}
