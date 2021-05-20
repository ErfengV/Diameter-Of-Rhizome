using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController2 : MonoBehaviour
{

   
    public static bool canCtrl = true; //判断角色是否控制  true 可控制 false 不可控制
    private Save save;
    public Transform playerXY;//
    public float speed = 5;
    public float wallSlidingSpeed=2;
    public float jumpSpeed = 7f;
    public float xWallForce=4f,yWallForce=6f;
    public bool canWallJump = false, canSprinting=false, throughWall=false,canJumpTwice=false;
    public float sprintSpeed=12f,sprintTime=0.25f,sprintCD=0.5f;
    
    
    private float GroundJumpTime;
    private float WallJumpTime=0.1f;

    private float move;
    private bool jump;
    private Rigidbody2D rig;
    private Animator anim;
    private CapsuleCollider2D cap;
    //蹬墙跳
    private bool isTouchingFront,wallSliding,wallJumping,sprinting;
    private float wallJumpTo,sprintingTo;
    public GameObject corpose;
    public Transform m_GroundCheck;
    public Transform frontCheck;
    public Transform home;
    public GameObject pauseMenu;
    public LayerMask groundMask;
    
    const float k_GroundedRadius = 0.1f, m_NextGroundChecking = 0.1f;//检测地面的小圆
    private float sprintingTimer;
    private bool m_Grounded;
    private bool m_FacingRight = true;
    private bool twiceJump = true;
    private bool airSprinting = true;
    //private Vector3 m_Velocity = Vector3.zero;
    
    private Collider2D[] colliders;

    [Header("Events")]
    [Space]
    public UnityEvent OnlandEvent;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    // Start is called before the first frame update

        void Awake(){        
        if (OnlandEvent == null)
        {
            OnlandEvent = new UnityEvent();
        }
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider2D>();

        playerXY = GetComponent<Transform>();
        Debug.Log("场景已加载"+Save.flag);
        save = new Save();
      
    }

    void Start() {
        
        if (Load.flag)
        {
            Debug.Log("人物坐标");
            playerXY.position = save.xiuxichu.position;
        }
        Load.flag = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        move = Input.GetAxis("Horizontal");
       
        move *= speed;
        jump = Input.GetKeyDown(KeyCode.K);      
        //jump = Input.GetKeyDown(KeyCode.Escape);     
        anim.SetBool("grounded",m_Grounded);      
        if (pauseMenu.activeSelf)
        {
            anim.SetFloat("velocityX",0f);
        }
        else
        {
            anim.SetFloat("velocityX", Mathf.Abs(move));
        }
    }

    private void FixedUpdate()
    {
        if (canCtrl)
        {
            //检测
            bool wasGrounded = m_Grounded;
            m_Grounded = false;//接触地面
            if (Time.time > GroundJumpTime)
            {
                colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius);
                for (int i = 0; i < colliders.Length; i++)

                {
                    if (colliders[i].tag == "platform")
                    {
                        m_Grounded = true;
                        //if (isTouchingFront)
                        //{
                        //    rig.velocity = new Vector2(rig.velocity.x, 0f);
                        //}

                        twiceJump = true;
                        airSprinting = true;
                        if (!wasGrounded)
                            OnlandEvent.Invoke();
                    }
                }
            }

            isTouchingFront = false;//接触墙面
            Collider2D[] cs = Physics2D.OverlapCircleAll(frontCheck.position, k_GroundedRadius);
            for (int i = 0; i < cs.Length; i++)
            {
                if (cs[i].tag == "platform")
                {
                    isTouchingFront = true;
                    rig.velocity = new Vector2(0f, rig.velocity.y);
                }
            }


            //控制
            if (!sprinting)
            {
                sprintingTimer += Time.deltaTime;
                if (canSprinting)//习得冲刺
                {
                    if (Input.GetKeyDown(KeyCode.H) && !wallJumping && sprintingTimer > sprintCD && airSprinting)//冲刺
                    {
                        if (!m_Grounded) airSprinting = false;
                        sprintingTimer = 0;
                        sprinting = true;
                        sprintingTo = transform.localScale.x;
                        Invoke("SetSprintingToFalse", sprintTime);
                        anim.SetBool("sprinting", true);
                        if (throughWall) cap.enabled = false;
                    }
                }


                if (isTouchingFront && !m_Grounded && canWallJump)//贴墙滑
                {
                    wallSliding = true;
                    twiceJump = true;
                    airSprinting = true;
                }
                else
                {
                    wallSliding = false;
                }
                if (wallSliding)
                {
                    rig.velocity = new Vector2(rig.velocity.x, Mathf.Clamp(rig.velocity.y, -wallSlidingSpeed, float.MaxValue));
                    if (jump && !wallJumping)//蹬墙跳
                    {
                        wallJumping = true;
                        wallJumpTo = transform.localScale.x;
                        //rig.AddForce(new Vector2(xWallForce* transform.localScale.x, yWallForce)) ;
                        Invoke("SetWallJumpingToFalse", WallJumpTime);
                    }
                }
                if (wallJumping)
                {
                    //print(wallJumpTo + " " + move);
                    rig.velocity = new Vector2(xWallForce * -wallJumpTo, yWallForce);
                }
            }
            else
            {
                rig.velocity = new Vector2(sprintingTo * sprintSpeed, 0f);
            }


            if (Input.GetKeyUp(KeyCode.K) && rig.velocity.y > 0)//如果松开跳跃键，则不再向上
            {
                rig.velocity = new Vector2(rig.velocity.x, 0f);
            }
            if (pauseMenu.activeSelf)
            {
                rig.velocity = new Vector2(0f, rig.velocity.y);
            }
            else if (!wallJumping && !sprinting)
            {
                Move(move, jump);
            }
        }
    }
    public void Move(float move, bool jump)
    {
        
        if (m_Grounded ||!wallSliding)//可控制的
        {
            //输入move决定瞬间速度
            rig.velocity = new Vector2(move, rig.velocity.y);//左右移动
        }
        if (move > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }
        if (m_Grounded && jump)
        {
            m_Grounded = false;
            rig.velocity=(new Vector2(0f, jumpSpeed));//起跳
            GroundJumpTime = Time.time + m_NextGroundChecking;
        }
        else if (!m_Grounded &&!wallSliding&& jump && twiceJump&&rig.velocity.y<=0&&canJumpTwice)//二段跳
        {
            twiceJump = false;
            rig.velocity = (new Vector2(0f, jumpSpeed));
            GroundJumpTime = Time.time + m_NextGroundChecking;
        }
        if (m_Grounded&& rig.velocity.y > 0.5f)
        {
            rig.velocity = (new Vector2(rig.velocity.x, 0.5f));
        }
    }
    private void Flip()//反转图像
    {
        m_FacingRight = !m_FacingRight;
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
    }
    private void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
    private void SetSprintingToFalse()
    {
        sprinting = false;
        anim.SetBool("sprinting", false);
        cap.enabled = true;
    }
    public void Hurt()
    {
        if (!anim.GetBool("dead"))
        {
            anim.SetTrigger("hurt");
            anim.SetBool("dead", true);
            Invoke("backToHome", 1.5f);
        }
        
    }
    public void backToHome()
    {
        GameObject.Instantiate(corpose, transform.position, transform.rotation);
        transform.position = home.position;
        anim.SetBool("dead", false);
    }
}
