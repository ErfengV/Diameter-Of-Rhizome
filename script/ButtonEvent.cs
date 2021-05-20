using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    private Button btn;
    [Header("暂停菜单")]
    public GameObject pauseMenu;
    private Animator anim;

    public Transform transformPlayer;
    public Transform transformCundangdian;
    void Awake()
    {
        anim = GetComponent<Animator>();
        print(anim);
        btn = this.GetComponent<Button>();
        btn.animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    void Update()
    {
        //print(pauseMenu.GetComponent<Animator>().GetBool("active"));
        if (m_down && Time.time - _time > 2f)//按下2秒属于长按
        {
            m_down = false;
            Debug.Log("长按中");
        }
    }
    //
    public void ResetAnimator()
    {
        //btn.animator.Play("NULL", 0, 0);
        //btn.animator.Update(0);
        //btn.animator.enabled = false;
    }
    public void BoFanAnimator()
    {
        //btn.animator.Play("Back");
        //btn.animator.enabled = true;
    }
    public void MouseEnter()
    {
        anim.SetBool("mouseOn", true);//将动画状态机设置为动画模式
        //if (btn.animator.enabled == false)
        //    BoFanAnimator();
        //Debug.Log("鼠标进入");
    }
    public void MouseExit()
    {
        anim.SetBool("mouseOn", false);//将动画状态机设置为静止模式
        //    if (btn.animator.enabled == true)

        //ResetAnimator();
        //Debug.Log("鼠标滑出");
    }
   
    //返回按钮点击
    public void  BackMouseClick()
    {
        anim.SetTrigger("click");//触发按钮点击动画
        
        Time.timeScale = 1f;
        pauseMenu.GetComponent<Animator>().SetBool("active",false);
        PlayerController1.canCtrl = true;
        StartCoroutine("pauseMenuUnable");
        //Invoke("pauseMenuUnable", 1f);
        //btn.animator.updateMode=AnimatorUpdateMode.Normal;
        //Debug.Log("返回按钮点击");
    }
    IEnumerator  pauseMenuUnable()
    {
        yield return new WaitForSeconds(1f);
        pauseMenu.SetActive(false);
    }
    //返回上一存档点按钮点击
    public void PointMouseClick()
    {
        anim.SetTrigger("click");//触发按钮点击动画
        Debug.Log("返回上一存档点按钮点击");

        if (Save.flag)//场景可以加载   后期要用堆栈式结构存储场景是否加载，以及加载的位置
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Load.flag = true;//场景已加载
            PlayerController1.canCtrl = true;
        }
     
    }
    //设置按钮点击
    public void SetMouseClick()
    {
        anim.SetTrigger("click");//触发按钮点击动画
        Debug.Log("设置按钮点击");
    }
    //返回主界面按钮点击
    public void HomeMouseClick()
    {
        anim.SetTrigger("click");//触发按钮点击动画
        Debug.Log("返回主界面按钮点击");
    }


    float _time;
    bool m_down = false;
    void MouseDownLong(bool _isdown)
    {
        m_down = _isdown;
        if (_isdown)
        {
            Debug.Log("开始长按");
            _time = Time.time;
        }
        else if (_time != 0)
        {
            _time = 0;
            Debug.Log("停止长按");
        }
    }
}