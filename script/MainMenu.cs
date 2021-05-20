using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private GameObject Player;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void pauseGame() {
        pauseMenu.SetActive(true);
        pauseMenu.GetComponent<Animator>().SetBool("active", true);//呼出菜单动画
        //Player.GetComponent<PlayerController1>().canCtrl= false;//拿到脚本后反而不能用公开静态
        PlayerController1.canCtrl= false;
        Invoke("stopGameTime", 1.5f);//等动画放完停止时间


    }
    private void stopGameTime()
    {
        //Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;//暂停时让玩家静止，否则会飞
        //Time.timeScale = 0f;
    }
    IEnumerator StopGame()
    {
        yield return new WaitForSeconds(1f);
    }

    public void backGame() {
       
    }
}
