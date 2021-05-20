using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeltMoos : MonoBehaviour
{
    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        ani = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player"))
        {
          
            ani.Play("play");  //直接播放动画的名字
        }

    }
}
