using UnityEngine;
using System.Collections;


public class deadLine : MonoBehaviour
{
    
    public float speed;
    public float Times = 1.5f;//触发时间
    private static bool flag=false;

    
    public Transform MovePosition;

    private Vector3 StartPosition;
    private Vector3 EndPosition;
    private bool OnTheMove;
    private Animator animator;

   
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("idle");
        StartPosition = this.transform.position;
        EndPosition = MovePosition.position;
    }

    void FixedUpdate()
    {
        if (OnTheMove) {
            return;
        }
        MoveAi();  
    }
    void MoveAi() {
        float step = speed * Time.deltaTime;
        flag = TestBox.flag;
        if (OnTheMove == false && flag == true)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, EndPosition, step);
            StartCoroutine(Run());
        }

        if (this.transform.position.x == EndPosition.x && this.transform.position.y == EndPosition.y && OnTheMove == false)
        {
            Dead();
            OnTheMove = true;
        }
    }

    void Dead() {
        animator.Play("dead");  //直接播放动画的名字
    }

    IEnumerator Run()
    {
        yield return new WaitForSeconds(Times);
        animator.Play("Run");  //直接播放动画的名字
    }
}