using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public GameObject dialog;
    public static bool dialogFlag = false;//判断当前对话框是否显示

    private WillPoint willPoint = new WillPoint();

    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            dialogFlag = true;
            Debug.Log("显示对话框");
            dialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player")
        {
            dialogFlag = false;
            willPoint.addPoint();
            Debug.Log("关闭对话框"+WillPoint.get());
            dialog.SetActive(false);
        }
    }
}
