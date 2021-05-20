using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    public float jumpForce = 400f;
    public bool canAirControl = false;
    public LayerMask groundMask;
    public Transform m_GroundCheck;

    const float k_GroundedRadius = 0.1f,m_NextGroundChecking=0.1f;//检测地面的小圆
    public  bool m_Grounded;
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;

    float m_NextGroundCheckTime;
    private Rigidbody2D m_rig;
    private Collider2D[] colliders;


    [Header("Events")]
    [Space]
    public UnityEvent OnlandEvent;
    [System.Serializable]
    public  class BoolEvent : UnityEvent<bool> { }
    // Start is called before the first frame update
    

    //private void OnTriggerEnter(Collider other)
    //{
    //    colliders.
    //}
    // Update is called once per frame
    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        if (Time.time > m_NextGroundCheckTime)
        {            
            colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position,k_GroundedRadius);
            for (int i = 0; i < colliders.Length; i++)
                
            {
                if (colliders[i].gameObject != gameObject&&colliders[i].tag=="platform")
                {
                    m_Grounded = true;
                    if (!wasGrounded)
                    OnlandEvent.Invoke();

                }
            }
        }
    }
    public void Move(float move,bool jump)
    {
        if(m_Grounded || canAirControl)//可控制的
        {
            //输入move决定瞬间速度
            m_rig.velocity = new Vector2(move, m_rig.velocity.y);//左右移动
        }
        if(move>0&& !m_FacingRight)
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
            m_rig.AddForce(new Vector2(0f, jumpForce));//起跳
            m_NextGroundCheckTime = Time.time + m_NextGroundChecking;
        }
    }
    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
    }
}
