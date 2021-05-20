using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    private bool isMoveLeft = false;
    private ManagerVars vars;
    public Vector3 leftUpPos;
    public Vector3 rightUpPos;
    private Vector3 currentPlatform;
    //private Vector3 nextPlatformLeft, nextPlatformRight;
    // Start is called before the first frame update
    private void Awake()
    {
        vars = ManagerVars.GetManagerVars();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                isMoveLeft = true;
            }
            else
            {
                isMoveLeft = false;
            }
            Jump();
        }
        
    }
    private void Jump()
    {
        if (currentPlatform != Vector3.zero)
        {
            if (isMoveLeft)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Vector3 targetPos = currentPlatform + leftUpPos;
                transform.DOMoveX(targetPos.x, 0.2f);
                transform.DOMoveY(targetPos.y + 0.5f, 0.15f);
            }
            else
            {
                transform.localScale = Vector3.one;
                Vector3 targetPos = currentPlatform + rightUpPos;
                transform.DOMoveX(targetPos.x, 0.2f);
                transform.DOMoveY(targetPos.y + 0.8f, 0.15f);
            }
            currentPlatform = Vector3.zero;
        }
        



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "platform")
        {
            currentPlatform = collision.transform.position;
        }
    }
}
