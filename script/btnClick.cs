using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => 
        { 
            EventCenter.Broadcast(EventType.showText,"这是文字",0000,"shenmasadj",true); 
        }) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
