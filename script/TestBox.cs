using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//碰撞检测
public class TestBox : MonoBehaviour
{
    public static bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player"))
        {
            flag = true;
        }

    }
}
