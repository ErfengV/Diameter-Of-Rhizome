using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    private Vector3 offset;
    private Transform PlayerPos;
    public Transform transformxy;
    // Start is called before the first frame update

    void Awake() {
        if (Load.flag) {

            this.GetComponent<Transform>().position = new Vector3(transformxy.position.x, transformxy.position.y, -10);
        }
    }
    void Start()
    {

        PlayerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        offset = transform.position - PlayerPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, PlayerPos.position + offset, Time.deltaTime * 2);
            // Mathf.Lerp(transform.position.y, PlayerPos.position.y + offset.y, Time.deltaTime * 2,));
    }
}
