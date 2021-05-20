using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleMove : MonoBehaviour
{
    Rigidbody2D rig;
    Animator anim;

    public Transform groundPos, frontPos;

    bool ground, onwall,yMove,wallJumping=false;
    float xMove, wallJumpTo, wallJumpTime=0.1f;

    // Start is called before the first frame update
    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetKeyDown(KeyCode.W);

        anim.SetFloat("velocityX",Mathf.Abs( xMove));
        anim.SetBool("grounded", ground);

        check();
        Slide();
        if (!wallJumping)
        {
            Move();
        }        
        Flip(xMove);       
    }
    private void check()
    {
        ground = false;
        onwall = false;
        if ( Physics2D.OverlapCircle(groundPos.position, 0.1f).tag=="platform")
        {
            ground = true;
        }
        else if(Physics2D.OverlapCircle(frontPos.position,0.1f).tag== "platform")
        {
            onwall = true;
        }
    }
    private void Move()
    {
        rig.velocity = new Vector2(xMove * 4, rig.velocity.y);
        if (yMove&&ground)
        {
            rig.velocity = new Vector2(0f, 7f);
        }
        if (Input.GetKeyUp(KeyCode.W) && rig.velocity.y > 0)//如果松开跳跃键，则不再向上
        {
            rig.velocity = new Vector2(rig.velocity.x, 0f);
        }
    }
    private void Slide()
    {
        if (onwall && !wallJumping)
        {
            rig.velocity = new Vector2(rig.velocity.x, Mathf.Clamp(rig.velocity.y, -2f, float.MaxValue));
            if (yMove)//蹬墙跳
           {
            wallJumping = true;
            wallJumpTo = transform.localScale.x;
            Invoke("stopWallJump", wallJumpTime);
           }
        }
        if (wallJumping)
        {
            rig.velocity = new Vector2( -wallJumpTo*4f, 6f);
        }
    }
    private void Flip(float x)
    {
        if (x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(x<0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }       
    }
    private void stopWallJump()
    {
        wallJumping = false;
    }
}
