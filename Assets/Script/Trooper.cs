using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trooper : MonoBehaviour
{
    public Vector3      destination;
    public int          speed;
    bool                facingRight = true;
    private Animator    anim;
    AudioSource         audioData;
    public GameObject   target;
    public int          attack_value;
    //second is used for DPS
    private float       second = 0;
    public bool         attacks = false;
    //goal to destroy
    public GameObject   hotel;
    public  int         MaxLifePoints;
    public  int         lifePoints;
    //name of the enemy hotel
    public string       hotelEnemy;
    public string       tagVillageEnemy;
    public string       tagEnemy;
    private bool        isDead = false;
    public End          end;

    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;
        end = GameObject.Find("End").GetComponent<End>();
        anim = GetComponent<Animator>();
        audioData =  GetComponent<AudioSource>();
        hotel = GameObject.Find(hotelEnemy);
        lifePoints = MaxLifePoints;
        if (hotel == null)
            Debug.Log("Error : Hotel not found");
       
    }
    // change character localX depending of the orientation
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = facingRight ? false : true;
    }
    public void Attack()
    {
        //attack/kill an enemy
        if (target.tag == tagEnemy)
        {
            target.GetComponent<Trooper>().lifePoints -= attack_value ;
            if (target.GetComponent<Trooper>().lifePoints > 0)
                Debug.Log(target.GetComponent<Trooper>().name + " [" +  target.GetComponent<Trooper>().lifePoints + "/" + target.GetComponent<Trooper>().MaxLifePoints + "] has been attacked.");
            else
            {
                 target = null;
                 anim.SetBool("isAttacking", false);
            }
        }
        else if (target.tag == tagVillageEnemy) //attack/destroy enemy village
        {
            target.GetComponent<Building>().lifePoints -= attack_value;
            if (target.GetComponent<Building>().lifePoints > 0)
                Debug.Log(target.GetComponent<Building>().gameObject.name + "[" +  target.GetComponent<Building>().lifePoints + "/" + target.GetComponent<Building>().MaxLifePoints + "] has been attacked.");
            else
            {
                if (target == hotel)
                {
                    end.isEnd = true;
                    Debug.Log("Game Over !!");
                }
                else
                    hotel.GetComponent<Building>().spawnTime += (float)2.5; //when a building is destroyed, enemy's spawntime increases
                Destroy(target);
                target = null;
            }
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == target)
        {
            attacks = false;
            second = 1;
            anim.SetBool("isAttacking", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (target != null && other.gameObject == target)
        {
            destination = transform.position;
            attacks = true;
            anim.SetBool("isAttacking", true);
        }
    }
 
    public void Move()
    {
        anim.SetBool("isMoving", true);
        anim.SetBool("isAttacking", false);
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (destination.x < transform.position.x && facingRight)
            Flip();
        else if (destination.x > transform.position.x && !facingRight)
            Flip();
    }
    void Update()
    {
        if (!isDead && end.isEnd == false)
        {
            if (gameObject.transform.position != destination && !attacks)
                Move();
            else
                anim.SetBool("isMoving", false);
            if (attacks && target != null && !isDead)
            {
                second-= 1 * Time.deltaTime;
                if (second <= 0)
                {
                    Attack();
                    second = 1;
                }
            }
            if (lifePoints <= 0 && !isDead)
            {
                anim.SetBool("isDead", true);
                isDead = true;
            }
            if (attacks && target)
                anim.SetBool("isAttacking", true);
            else
                anim.SetBool("isAttacking", false);
        }
    }
}
