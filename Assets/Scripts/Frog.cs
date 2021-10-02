using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    [SerializeField]private float leftCap;
    [SerializeField]private float rightCap;
    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask Ground;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Collider2D frogCollider;
    private bool facingLeft = true;
    private Animator FrogFSM;
    private enum FrogState {idle, jump, falling };
    private FrogState currentState = FrogState.idle;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        sprite = GetComponent<SpriteRenderer>();
        frogCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        FrogFSM = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StateSwitch();
        FrogFSM.SetInteger("State", (int)currentState);
    }

    private void Move()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (frogCollider.IsTouchingLayers(Ground))
                {
                    currentState = FrogState.jump;
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                }
            }
            else
            {
                sprite.flipX = true;
                facingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightCap)
            {
                if (frogCollider.IsTouchingLayers(Ground))
                {
                    currentState = FrogState.jump;
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                }
            }
            else
            {
                sprite.flipX = false;
                facingLeft = true;
            }
        }
    }

    private void StateSwitch()
    {
        if (currentState == FrogState.jump)
        {
            if (rb.velocity.y < .1f)
            {
                currentState = FrogState.falling;
            }
        }//end if
        else if (currentState == FrogState.falling)
        {
            if (frogCollider.IsTouchingLayers(Ground))
            {
                currentState = FrogState.idle;
            }
        }//end else if
        else
        {
            currentState = FrogState.idle;
            rb.velocity = new Vector2(0, 0);
        }
    }

    
}
