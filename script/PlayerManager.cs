using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float movex;
    public JumpState jumpState;
    public bool stopJump, controlEnabled = true,ground=true;
    
    public float maxSpeed = 5,addForce=20, jumpForce=50;
    private float jumpEndurance = 0.5f;
    public float Endurance = 0.75f;
    private float platformCount=0;
    //public float jumpTakeOffSpeed = 7;

    //public JumpState jumpState = JumpState.Grounded;

    /*internal new*/
    //public Collider2D collider2d;
    /*internal new*/
    //public AudioSource audioSource;
    //public Health health;


    bool jump;
    
    Vector2 move;
    SpriteRenderer spriteRenderer;
    private Animator anim;
    private Rigidbody2D rig;
    //internal Animator animator;
    //readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

    //public Bounds Bounds => collider2d.bounds;
    // Start is called before the first frame update
    void Awake()
    {
        //QualitySettings.vSyncCount =200;
        //Application.targetFrameRate = 60;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        jumpEndurance = Endurance;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        //Debug.Log("0");
        if (controlEnabled)
        {
            movex = Input.GetAxis("Horizontal");
            if (jumpState == JumpState.Grounded && Input.GetKeyDown(KeyCode.W))
                jumpState = JumpState.PrepareToJump;
            else if (Input.GetKeyUp(KeyCode.W))
            {
                stopJump = true;
                jump = false;
                //Schedule<PlayerStopJump>().player = this;
            }
        }
        else
        {
            movex = 0;
        }

        UpdateJumpState();
        Move();
        print(platformCount);
        if (platformCount>0)
        {
            jumpEndurance = Endurance;
        }                         
            Jumping();
        
        
        setAnim();
    }
    private void Move()
    {
        
        //print(rig.velocity);
        if (rig.velocity.magnitude < maxSpeed)
        {
            float moveSpeed;
            if (movex > 0.01f)//右
            {
                transform.localScale = new Vector3(1, 1, 1);
                moveSpeed = addForce * movex;
                if (moveSpeed > maxSpeed)
                {
                    moveSpeed = maxSpeed;
                }
                rig.velocity = new Vector2(moveSpeed, rig.velocity.y);
                //rig.velocity = new Vector2(maxSpeed, rig.velocity.y);
            }
            else if (movex < -0.01f)//左
            {
                transform.localScale = new Vector3(-1, 1, 1);
                moveSpeed = addForce * movex;
                if (Mathf.Abs( moveSpeed) > maxSpeed)
                {
                    moveSpeed = -maxSpeed;
                }
                rig.velocity = new Vector2(moveSpeed, rig.velocity.y);
                //rig.velocity = new Vector2(-maxSpeed, rig.velocity.y);
            }
        }
        if (movex == 0&&rig.velocity.magnitude!=0)//减速
        {
            rig.velocity = new Vector2(Mathf.Lerp(0, rig.velocity.x, Time.deltaTime * 4), rig.velocity.y);          
        }
        
        //transform.localScale = new Vector3(movex, 1, 1);
        //transform.Translate(new Vector3(movex,0,0) * Time.deltaTime* maxSpeed);
    }
    private void Jumping()
    {
        if (jump)
        {
            if (jumpEndurance > 0)
            {
                jumpEndurance -= Time.deltaTime;

            }
            else if (rig.velocity.y > 0)
            {
                rig.velocity = new Vector2(rig.velocity.x, 0);
            }
        }
        else if (rig.velocity.y > 0)
        {
            rig.velocity = new Vector2(rig.velocity.x, 0);
        }


    }
    private void setAnim()
    {
        anim.SetFloat("velocityX",Mathf.Abs( movex));
        anim.SetFloat("velocityY", rig.velocity.y);       
        anim.SetBool("grounded", platformCount > 0);
        print(jumpState);
    }
    void UpdateJumpState()
    {
        //jump = false;
        switch (jumpState)
        {
            //case JumpState.Grounded:
            //    jumpEndurance = 1;
            //    break;
            case JumpState.PrepareToJump:
                rig.velocity=new Vector2(rig.velocity.x, jumpForce);
                jumpState = JumpState.Jumping;
                jump = true;
                stopJump = false;
                break;
            case JumpState.Jumping:
                if (platformCount<=0)
                {
                    //Schedule<PlayerJumped>().player = this;
                    jumpState = JumpState.InFlight;
                }
                break;
            case JumpState.InFlight:
                if (platformCount > 0)
                {
                    //Schedule<PlayerLanded>().player = this;
                    jumpState = JumpState.Landed;
                    jump = false;
                }
                break;
            case JumpState.Landed:
                
                jumpState = JumpState.Grounded;
                break;
        }
    }
    public enum JumpState
    {
        Grounded,
        PrepareToJump,
        Jumping,
        InFlight,
        Landed
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "platform")
        {
            platformCount++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "platform")
        {
            platformCount--;
        }
    }
}
