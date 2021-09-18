using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformSpawner : MonoBehaviour
{
    public GameObject platformX;
    public GameObject player;
    public GameObject jumpPlatform;

    Vector3 LastPos;
    float size;

    
    void Start()
    {
        LastPos = platformX.transform.position;
        size = platformX.transform.localScale.x;

        for (int i = 0; i < 20; i++)
        {
            SpawnPlatform();
        }
        InvokeRepeating("SpawnPlatform",5f,0.4f);
    }


    void SpawnPlatform()
    {
        int rand = Random.Range(0,10);
        if(rand < 4)
        {
            SpawnX();
            SpawnX();

        }
        if(rand > 5)
        {
            SpawnZ();
            SpawnZ();

        }
        if(rand == 4)
        {
            SpawnZ();
            SpawnZ();
            SpawnZ();
            jump();
            SpawnZ();
            SpawnZ();
            SpawnZ();
            SpawnZ();
        }
    }


    void SpawnX()
    {
        Vector3 pos = LastPos;
        pos.x += -size;

        Instantiate(platformX, pos, Quaternion.identity);
        LastPos = pos; 
    }
    void SpawnZ()
    {
        Vector3 pos = LastPos;
        pos.z += size;

        Instantiate(platformX, pos, Quaternion.identity);
        LastPos = pos; 
    }
    void jump()
    {
        Vector3 pos = LastPos;
        pos.z += size;

        Instantiate(jumpPlatform, pos, Quaternion.identity);
        LastPos = pos; 
    }
}
