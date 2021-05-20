using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showText : MonoBehaviour
{
    public void Awake()
    {
        gameObject.SetActive(false);
        EventCenter.AddListener< string,int ,string ,bool > (EventType.showText, showtext);
    }
    public void OnDestroy()
    {
        EventCenter.RemoveListener<string, int, string, bool>(EventType.showText, showtext);
    }
    public void showtext(string s,int i,string d,bool t)
    {
        gameObject.SetActive(true);
        GetComponent<Text>().text = s+i+d+t;
    }
}
