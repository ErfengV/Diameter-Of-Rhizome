using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject player;//玩家
    private Transform playerTranform;
    private Animator pAnim;//播放玩家死亡动画
    private Rigidbody2D p2d;//

    public float speed;
    public float Times = 1.5f;//触发时间
    private static bool flag = false;


    public Transform MovePosition;
    

    private Vector3 StartPosition;
    private Vector3 EndPosition;
    private bool OnTheMove=false;
   


    // Start is called before the first frame update


    void Start()
    {
       pAnim= player.GetComponent<Animator>();
        p2d = player.GetComponent<Rigidbody2D>();
        EndPosition = MovePosition.position;
     
    }

    void Awake()
    {
        playerTranform = player.GetComponent<Transform>();
    }
    private void FixedUpdate() { 

        float step = speed * Time.deltaTime;
       
        if (OnTheMove == false && flag ==true)      
        {
            Debug.Log(playerTranform.position+"不要出bug了");
            playerTranform.position = Vector3.MoveTowards(playerTranform.position,EndPosition, step);
        }
        if (playerTranform.position.x== EndPosition.x && playerTranform.position.y == EndPosition.y && OnTheMove == false)
        {
            Debug.Log(playerTranform.position + "不要出bug了");
           
            Dead();
            OnTheMove = true;
        }
        if (OnTheMove) {
            playerTranform.position = new Vector3(EndPosition.x, EndPosition.y, 0);
        }

    }

    void Dead()
    {
        pAnim.Play("蘑菇死亡");
       
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player"))
        {
            //Dead();
            flag = true;
           
            PlayerController1.canCtrl = false;//取消人物控制  
        }

    }

    void destroy() {
        Destroy(p2d);
    }
}
