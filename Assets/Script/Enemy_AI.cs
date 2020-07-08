
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    private Trooper troop;
    void Start()
    {
        troop = GetComponent<Trooper>();
        troop.target = GameObject.Find(troop.hotelEnemy);
        troop.destination = troop.target.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == troop.tagEnemy)
        {
            troop.target = other.gameObject;
            troop.destination = troop.target.transform.position;
            troop.destination.z = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (troop.target == null && troop.end.isEnd != false)
        {
            troop.target = GameObject.Find(troop.hotelEnemy);
            troop.destination = troop.target.transform.position;
            troop.destination.z = 1;
        }
            
    }
}
