using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformSpawner : MonoBehaviour
{
    public Vector3 StartPos;
    public Vector3 leftUpPos;
    public Vector3 rightUpPos;

    private Vector3 platformSpawnerPos;
    private int spawnPlatformCount;
    private bool isLeftSpawn=false;
    private ManagerVars vars;
    // Start is called before the first frame update
    void Start()
    {
        //生成平台
        vars = ManagerVars.GetManagerVars();
        platformSpawnerPos=StartPos;
        for (int i = 0; i < 5; i++)
        {
            spawnPlatformCount = 5;
            DecidePath();
        }
        //生成人物
        GameObject.Instantiate(vars.playerPrefeb, StartPos + new Vector3(0, 2, 0),Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void DecidePath()
    {
        if (spawnPlatformCount > 0)
        {
            spawnPlatformCount--;
            SpawnPlatform();
        }
        else {
            isLeftSpawn = !isLeftSpawn;
            spawnPlatformCount = Random.Range(1, 4);
            SpawnPlatform();
        }
    }
    private void SpawnPlatform()
    {
        GameObject go = GameObject.Instantiate(vars.normalPlatform, transform);
        go.transform.position = platformSpawnerPos;
        if (isLeftSpawn)
        {
            platformSpawnerPos = platformSpawnerPos + leftUpPos;
        }
        else
        {
            platformSpawnerPos = platformSpawnerPos + rightUpPos;
        }

    }
}
