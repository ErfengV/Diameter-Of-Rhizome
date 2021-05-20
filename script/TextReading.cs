using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextReading : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;

    [Header("文本文件")]
    public TextAsset textFile;//聆听的文字

   

    List<string> textList = new List<string>();

    public TextReading(TextAsset textFile,Text textLabel) {
        
        this.textFile = textFile;
        this.textLabel = textLabel;
    }

    /*
     * 
     * */
   
    public List<string> readText(string npc)
    {
        textList = GetTextFile(textFile);
        List<string> npclist = new List<string>();
        Debug.Log("输入的npc的名称" + npc);
        
       for(int index=0;index<textList.Count;index++)
        { 
            string[] npcdo = textList[index].Split('|');
            Debug.Log("搜索出来的npc的名称"+npcdo[0]);
            if (npcdo[0].Equals(npc))
            {
                for (int j = 0; j < npcdo.Length; j++)
                {
                    npclist.Add(npcdo[j]);
                    
                }
            }
        }
        return npclist;
        
    }

    //提取文本内容
    List<string> GetTextFile(TextAsset file)
    {       
        textList.Clear();
       
        string[] lineData = file.text.Split('\n');//将文本按行切割

        for (int i = 0; i < lineData.Length; i++)
        {
            textList.Add(lineData[i]);
        }
        return textList;
    }
      
 }
