using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//易滑落的方块
public class sq : MonoBehaviour
{
    [Header("自动恢复的时间")]
    private float Time = 4f;

    Animator animator;

    bool flag = false;
    void Start() {
        animator = GetComponent<Animator>();
    }


    void OnTriggerEnter2D(Collider2D collider2D) {
        if (collider2D.gameObject.CompareTag("Player"))
        {
            flag = true;
            animator.Play("Idel");  //直接播放动画的名字
        }

    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Reserve());
           
        }

    }

    IEnumerator Reserve() {
       
        yield return new WaitForSeconds(Time);
        animator.Play("resrve");  //直接播放动画的名字
    }

}
