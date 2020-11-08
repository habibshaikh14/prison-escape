using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float jumpForce = 20f;
    
    // Chached parameters
    private Rigidbody2D myRigidBody2D;
    private Animator myAnimator;
    private Collider2D myCollider2D;
    private float gravityScaleAtStart;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
        gravityScaleAtStart = myRigidBody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRun();
        PlayerJump();
        PlayerClimb();
        HandlePlayerMovements();
    }

    private void PlayerRun()
    {
        Vector2 inputVelocity = new Vector2(Input.GetAxis("Horizontal") * playerSpeed, myRigidBody2D.velocity.y);
        myRigidBody2D.velocity = inputVelocity;
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpSpeed = new Vector2(0f, jumpForce);
            myRigidBody2D.velocity += jumpSpeed;
        }
    }

    private void PlayerClimb()
    {
        bool playerIsTouchingLadder = myCollider2D.IsTouchingLayers(LayerMask.GetMask("Interactables"));
        bool playerHasVerticalVelocity = Mathf.Abs(myRigidBody2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimbing", playerIsTouchingLadder && playerHasVerticalVelocity && !myAnimator.GetBool("IsRunning"));
        myRigidBody2D.gravityScale = playerIsTouchingLadder ? 0 : gravityScaleAtStart;
        if (playerIsTouchingLadder)
        {
            Vector2 inputVelocity = new Vector2(myRigidBody2D.velocity.x, Input.GetAxis("Vertical") * playerSpeed);
            myRigidBody2D.velocity = inputVelocity;
        }
    }

    private void HandlePlayerMovements()
    {
        bool playerHasHorizontalVelocity = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", playerHasHorizontalVelocity && !myAnimator.GetBool("IsClimbing"));
        if (playerHasHorizontalVelocity)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.velocity.x), transform.localScale.y);
        }
    }
}
