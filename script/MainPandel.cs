using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainPandel : MonoBehaviour
{
    private Button btn_start;
    private Button btn_store;
    private Button btn_ranking;
    private Button btn_sound;
    // Start is called before the first frame update
    void Awake()
    {
        
        Init();
    }
    private void Init()
    {
        btn_start = transform.Find("btn_start").GetComponent<Button>();
        btn_start.onClick.AddListener(onBtnStartClick);
        print(btn_start);
        btn_store = transform.Find("buttons/btn_store").GetComponent<Button>();
        btn_store.onClick.AddListener(onBtnStoreClick);
        btn_ranking = transform.Find("buttons/btn_ranking").GetComponent<Button>();
        btn_ranking.onClick.AddListener(onBtnRankingClick);
        btn_sound = transform.Find("buttons/btn_sound").GetComponent<Button>();
       
        btn_sound.onClick.AddListener(onBtnSoundClick);
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.LogWarning("1");

    }
    private void OnMouseDown()
    {
        RaycastHit hit;
        
    }
    public void onBtnStartClick()
    {
        print("2");
        EventCenter.Broadcast(EventType.showGamePandel);
        gameObject.SetActive(false);
    }
    public void onBtnStoreClick()
    {
        print("3");
    }
    public void onBtnRankingClick()
    {

    }
    public void onBtnSoundClick()
    {

    }
}
