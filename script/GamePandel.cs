using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GamePandel : MonoBehaviour
{
    private Button btn_Pause;
    private Button btn_Play;
    private Text txt_Score;
    private Text txt_DiamondCount;
    public static GamePandel _GamePandel;
    // Start is called before the first frame update
    private void Awake()
    {
        _GamePandel = this;
        EventCenter.AddListener(EventType.showGamePandel, show);
        btn_Pause = transform.Find("btn_Pause").GetComponent<Button>();
        btn_Pause.onClick.AddListener(OnPauseClick);
        btn_Pause.gameObject.SetActive(true);
        btn_Play = transform.Find("btn_Play").GetComponent<Button>();
        btn_Play.onClick.AddListener(OnPlayClick);
        btn_Play.gameObject.SetActive(false);
        txt_Score = transform.Find("txt_Score").GetComponent<Text>();
        txt_DiamondCount = transform.Find("Image/txt_DiamondCount").GetComponent<Text>();
        gameObject.SetActive(false);
    }
    public void show()
    {
        gameObject.SetActive(true);
    }
    public void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.showGamePandel,show);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnPauseClick()
    {
        btn_Pause.gameObject.SetActive(false);
        Time.timeScale = 0;
        btn_Play.gameObject.SetActive(true);
    }
    private void OnPlayClick()
    {
        btn_Pause.gameObject.SetActive(true);
        Time.timeScale = 1;
        btn_Play.gameObject.SetActive(false);
    }
}
