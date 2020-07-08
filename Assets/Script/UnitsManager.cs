using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    AudioSource         audioData;
    public List<GameObject>        troopers = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        audioData = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit && Input.GetKey(KeyCode.LeftControl))
            {
                if (hit.collider.gameObject.tag == "peons")
                {
                    foreach (GameObject item in troopers)
                    {
                        if (item.name == hit.collider.gameObject.name)
                        {
                            troopers.Remove(item);
                            return;
                        }
                    }
                     troopers.Add(hit.collider.gameObject);
                }
               
            }
            else if (hit && !Input.GetKey(KeyCode.LeftControl))
            {
                Debug.Log("true");
                if (hit.collider.gameObject.tag == "peons")
                {
                    troopers.Clear();
                    troopers.Add(hit.collider.gameObject);
                }
                else if(hit.collider.gameObject.tag == "enemy" || hit.collider.gameObject.tag == "Orc_Village")
                {
                    foreach (GameObject item in troopers)
                    {
                        item.GetComponent<Trooper>().target = hit.collider.gameObject;
                        item.GetComponent<Trooper>().destination = hit.collider.gameObject.transform.position;
                        item.GetComponent<Trooper>().destination.z = 1;
                    }
                }
            }
            else
            {
                foreach (GameObject item in troopers)
                {

                    Trooper troop = item.GetComponent<Trooper>();
                    troop.destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    troop.destination.z = item.transform.position.z;
                    item.GetComponent<AudioSource>().Play(0);
                    troop.target = null;
                    troop.attacks = false;
                    // Debug.Log("dest =" + dest);

                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            troopers.Clear();
        }
    }
}
