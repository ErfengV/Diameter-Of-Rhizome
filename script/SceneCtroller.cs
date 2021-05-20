using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtroller : MonoBehaviour
{

    public GameObject hero;
    public Vector2 pointOffset;
    public Vector2 size;
    public LayerMask groundLayerMask;
    // Update is called once per frame
    void Update()
    {
        if (is_Ground()) {
            OnTriggerEnter();
        }
      
        
    }
    void OnTriggerEnter() {
        //加载场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public bool is_Ground()
    {
        Collider2D coll = Physics2D.OverlapBox((Vector2)transform.position + pointOffset, size, 0, groundLayerMask);
        if (coll != null)
        {
            Debug.Log("COLL为空");
            return true;
        }
        else
        {
            Debug.Log("COLL不为空");
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + pointOffset, size);
    }

}
