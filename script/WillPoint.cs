using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WillPoint : MonoBehaviour
{
    private static int willPoint = 0;

  

    void Start() {
        this.GetComponent<Text>().text="0";
       
    }
    void Update() {
        this.GetComponent<Text>().text = willPoint.ToString();
    }

    public void addPoint() {
        willPoint++;
        
        //this.GetComponent<Text>().text = willPoint+" ";
    }

    public static void add() {
      
    }

    public static int get() {
        return willPoint;
    }
}
