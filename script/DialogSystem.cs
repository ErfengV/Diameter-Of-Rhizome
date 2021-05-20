using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 从文本中读取需要聆听的文字
 * */
public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;

    [Header("文本文件")]
    public TextAsset textFile;//聆听的文字

    /*
    [Header("族群刻文")]
    public GameObject EthnicEngraving;

    [Header("同伴尸体")]
    public GameObject CompanionCorpse1;

    public GameObject CompanionCorpse2;

    [Header("枯萎的毡苔草")]
    public GameObject WitheredGrass;
    */
 
    public int index=0;

    List<string> textList = new List<string>();

    /*
    //族群刻文
    List<string> Ethnic = new List<string>();
    //同伴尸体
    List<string> Companion = new List<string>();
    //枯萎的毡苔草
    List<string> Withered = new List<string>();
    */

    void Start()
    {
        GetTextFile(textFile);
        index = 0;
    }


    // Update is called once per frame
    void Update()
    {
        //当文本结束时
        
        if (Input.GetKeyDown(KeyCode.W) && index == textList.Count) {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        

        if (Input.GetKeyDown(KeyCode.W) ) {
            textLabel.text = textList[index];
            index++;
        }
    }
    void readText() {
        
        /*
        if (Input.GetKeyDown(KeyCode.W) && index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }*/


        if (Input.GetKeyDown(KeyCode.W))
        {
            textLabel.text = textList[index];
            index++;
        }

    }

    void GetTextFile(TextAsset file) {
        /*
          Ethnic.Clear();
        Companion.Clear();
        Withered.Clear();
         */

        textList.Clear();
        index = 0;
        string []lineData= file.text.Split('\n');//将文本按行切割

        for (int i = 0; i < lineData.Length; i++) {
            textList.Add(lineData[i]);
        }



        /*
        for (int i = 0; i < lineData.Length; i++) {
            if (lineData[i] == "Q1")
            {

                Ethnic.Add(lineData[i]);
               
            }
             if (lineData[i] == "T1"  || lineData[i] == "T2") {
                Companion.Add(lineData[i]);
             

            }
            if (lineData[i] == "K1")
            {
                Withered.Add(lineData[i]);
            }
        }*/
    }
}
