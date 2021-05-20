using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgTheme : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;
    private ManagerVars vars;
    private void Awake()
    {
        vars =ManagerVars.GetManagerVars();
        print(vars);
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        int ranValue = Random.Range(0, vars.bgThemeSpriteList.Count);
        m_SpriteRenderer.sprite = vars.bgThemeSpriteList[ranValue];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
