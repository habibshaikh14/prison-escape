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
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRun();
        PlayerJump();
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

    private void HandlePlayerMovements()
    {
        bool playerHasMoved = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", playerHasMoved);
        if (playerHasMoved)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.velocity.x) * 1f, transform.localScale.y);
        }
    }
}
