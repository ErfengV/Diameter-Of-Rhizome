using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stele : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;

    [Header("UI组件")]
    public GameObject gameObject;

    [Header("文本文件")]
    public TextAsset textFile;//聆听的文字

    [Header("npc名称")]
    public string npc="T1";

    private TextReading text;
    private List<string> npclist;
    private int index=0;
    private bool flag = false;



    //初始化对象
    void Start()
    {
        text = new TextReading(textFile,textLabel);
        npclist = text.readText(npc);
        index = 0;
    }

    void Update() {
        //if(index == npclist.Count)
        //{
        //    gameObject.SetActive(false);
        //    PlayerController1.canCtrl = true;
        //    index = 0;
        //    return;
        //}
        if (Input.GetKeyDown(KeyCode.W) && index == npclist.Count)
        {
           gameObject.SetActive(false);
            textLabel.text = "聆听 W";
            index = 1;
            return;
        }      
        if (Input.GetKeyDown(KeyCode.W))
        {
            /*
            if (flag == false && npclist[0]==npc && Dialog.dialogFlag==true) {
                flag = true;             
                WillPoint.add();
                Debug.Log("意志点已收集>>>>" + WillPoint.get()+npc);

            }
            Debug.Log(npclist.Count);
            */
            textLabel.text = npclist[index];
             index++;
            PlayerController1.canCtrl = false;//聆听时无法行动

        }
           
        
    }
  

}
