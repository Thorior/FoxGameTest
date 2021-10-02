using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator SpriteMachine;
    private Collider2D PlayerCollider;
    public Collectible Item;
    private AudioSource JumpSound;

    //interactable surfaces
    [SerializeField]private LayerMask Ground;
    [SerializeField]private LayerMask DeathPlane;
    //player variables
    [SerializeField]private float fVelocity;
    [SerializeField]private float jumpforce;
    private float hurtforce = 5f;
    
    //animation enum
    private enum AnimStateMachine { idle, running, jumping, falling, death, hurt};
    private AnimStateMachine currentState = AnimStateMachine.idle;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        SpriteMachine = GetComponent<Animator>();
        PlayerCollider = GetComponent<Collider2D>();
        JumpSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float hDirection = Input.GetAxis("Horizontal");
        if (currentState != AnimStateMachine.hurt)
        {
            if (hDirection < 0)
            {
                rb.velocity = new Vector2(-fVelocity, rb.velocity.y);
                sprite.flipX = true;
            }
            else if (hDirection > 0)
            {
                rb.velocity = new Vector2(fVelocity, rb.velocity.y);
                sprite.flipX = false;
            }
            if (Input.GetButtonDown("Jump")&& PlayerCollider.IsTouchingLayers(Ground))
             {
                Jump();
             }
        }
        if (PlayerCollider.IsTouchingLayers(DeathPlane))
        {
            currentState = AnimStateMachine.death;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(0, 0);
        }
        StateSwitch();
        SpriteMachine.SetInteger("State", (int)currentState);
    }

    private void StateSwitch()
    {
        if(currentState == AnimStateMachine.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                currentState = AnimStateMachine.falling;
            }
        }//end if
        else if(currentState == AnimStateMachine.falling)
        {
            if(PlayerCollider.IsTouchingLayers(Ground))
            {
                currentState = AnimStateMachine.idle;
            }
        }//end else if
        else if(currentState == AnimStateMachine.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                currentState = AnimStateMachine.idle;
            }
        }
        else if(Mathf.Abs(rb.velocity.x)>2f)
        {
            currentState = AnimStateMachine.running;
        }
        else if(currentState == AnimStateMachine.death)
        {
            Destroy(this.gameObject, 0.55f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            currentState = AnimStateMachine.idle;
        }//end else
    }//end state switch
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectible")
        {
            Item = collision.gameObject.GetComponent<Collectible>();
            Item.currentState = Collectible.AnimFSM.pickedup;
            Item.StateCheck(Item.currentState);
            Item.PickUpAction();
        }
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
         if (collision.gameObject.tag == "Door" && Input.GetButtonDown("Submit"))
        {/*Mathf.Abs(Input.GetAxis("Vertical"))> .1f*/
            collision.gameObject.GetComponent<TreasureHouse>().EnterHouse();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if(currentState == AnimStateMachine.falling)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is to my right and i should be damaged and move left
                    rb.velocity = new Vector2(-hurtforce, rb.velocity.y);
                    currentState = AnimStateMachine.hurt;
                }
                else
                {
                    //Enemy is to my left and i should be damaged and move right
                    rb.velocity = new Vector2(hurtforce, rb.velocity.y);
                    currentState = AnimStateMachine.hurt;
                }
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        currentState = AnimStateMachine.jumping;
        JumpSound.Play();
    }
}//end class
