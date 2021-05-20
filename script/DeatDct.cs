using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeatDct : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.Play("idle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("deadMush"))
        {
            animator.Play("play");
        } else if (collider2D.gameObject.CompareTag("Player")) {
            animator.Play("play");
        }
        
    }
}
