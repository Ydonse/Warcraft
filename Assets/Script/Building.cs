using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int          lifePoints;
    public int          MaxLifePoints;
    //activate isMain only if it's a hotel
    public bool         isMain;
    public float        spawnTime = 10;
    private float       currentTime = 0;
    public GameObject   troop;
    public GameObject   spawner;
    public End          end;
    void Start()
    {
        lifePoints = MaxLifePoints;
        end = GameObject.Find("End").GetComponent<End>();
    }
    void    Spawn_Unit()
    {
        Vector3 pos = spawner.transform.position;
        pos.z = 1;
        Instantiate(troop, pos, spawner.transform.rotation);
        currentTime = 0;
    }
    void Update()
    {
        if (isMain && end.isEnd == false)
        {
            currentTime += 1 * Time.deltaTime;
            if (currentTime >= spawnTime)
                Spawn_Unit();
        }
        
    }
}
