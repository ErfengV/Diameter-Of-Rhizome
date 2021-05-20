using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//易滑落的方块
public class SlipCube : MonoBehaviour
{
    [Header("自动恢复的时间")]
    public float Time = 4f;

    [Header("易滑落的状态(anim动画名称)")]
    public string PlaySlip = "PlaySlip";

    [Header("闲置的状态(anim动画名称)")]
    public string IdelSlip = "IdelSlip";


    Animator animator;

    bool flag = false;
    void Start() {
        animator = GetComponent<Animator>();
    }


    void OnTriggerEnter2D(Collider2D collider2D) {
        if (collider2D.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlayAnim());  //直接播放动画的名字
        }

    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Reserve());
           
        }

    }

    IEnumerator PlayAnim()
    {
        yield return new WaitForSeconds(Time);
        animator.Play(PlaySlip);  //直接播放动画的名字
        this.GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator Reserve() {
       
        yield return new WaitForSeconds(Time);
        animator.Play(IdelSlip);  //直接播放动画的名字
        this.GetComponent<Collider2D>().enabled = true;
    }

}
