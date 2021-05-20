using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{

    public static bool flag = false;//判断场景是否可以被加载
    public Transform xiuxichu;

    public static float X = 10;
    public static float Y = 10;


    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player"))
        {
            flag = true;
        }

    }
    void Awake()
    {
        X = xiuxichu.position.x;
        Y = xiuxichu.position.y;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
